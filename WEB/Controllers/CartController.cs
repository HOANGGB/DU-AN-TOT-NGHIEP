using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.Models;
using WEB.Models.Mail;

namespace WEB.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext db;


        public CartController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Cart()
        {
            if (AccountCurrent.Acc != null)
            {
                var cartItems = from gh in db.GioHangs
                                join sp in db.SanPhams on gh.SanPhamID equals sp.SanPhamID
                                join kt in db.KichThuocSanPhams on gh.KichThuocID equals kt.KichThuocID
                                where gh.NguoiDungID == AccountCurrent.Acc.NguoiDungID
                                select new CartView
                                {
                                    GioHangID = gh.GioHangID,
                                    HinhAnh = sp.HinhAnh,
                                    TenSanPham = sp.TenSanPham,
                                    KichThuoc = kt,
                                    SanPhamID = sp.SanPhamID,
                                    SoLuong = gh.SoLuong,
                                    Gia = sp.Gia
                                };

                return View(cartItems.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        public IActionResult AddToCart(int id,int? ktid,int? sl)
        {
            if (AccountCurrent.Acc != null)
            {
                var checkCart = db.GioHangs.FirstOrDefault(c => c.SanPhamID == id 
                                                             && c.NguoiDungID == AccountCurrent.Acc.NguoiDungID);
                int soluongthem = sl.HasValue ? sl.Value : 1;
                int ktidcheck = ktid.HasValue ? ktid.Value
                                             : db.KichThuocSanPhams.FirstOrDefault(kt => kt.SanPhamID == id).KichThuocID;

                if (checkCart == null)
                {
                    Console.WriteLine("NAMEEEEEEEEEE = "+checkCart);
                    var cart = new GioHang
                    {
                        NguoiDungID = AccountCurrent.Acc.NguoiDungID,
                        SanPhamID = id,
                        KichThuocID = ktidcheck,
                        SoLuong = soluongthem
                    };
                    db.Add(cart);
                    db.SaveChanges();
                    return RedirectToAction("Cart", "Cart");
                }
                else
                {
                    Console.WriteLine("OKKKKKKKKKKKKKKKKKKK  + "+ checkCart.NguoiDungID);
                    Console.WriteLine("OKKKKKKKKKKKKKKKKKKK  + "+ checkCart.SanPhamID);

                    var sltoncheck = db.KichThuocSanPhams.FirstOrDefault(k=>k.KichThuocID == ktidcheck);
                    if (checkCart.SoLuong + soluongthem > sltoncheck.SoLuongTon) checkCart.SoLuong = sltoncheck.SoLuongTon;
                    else checkCart.SoLuong += soluongthem;

                    db.SaveChanges();
                    return RedirectToAction("Cart", "Cart");

                }

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult Sub_PlusCart(int id, string SP)
        {
            var gh = db.GioHangs.FirstOrDefault(g => g.GioHangID == id);
            if (SP == "Sub")
            {
                if (gh.SoLuong == 1) db.Remove(gh);
                else gh.SoLuong--;
            }
            else
            {
                if(gh.SoLuong < db.KichThuocSanPhams.FirstOrDefault(k=>k.KichThuocID == gh.KichThuocID).SoLuongTon)
                {
                    gh.SoLuong++;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Cart");
        }


        public IActionResult Buy(List<int> selectedItems)
        {
            var checkDc = db.DiaChis.Any(d => d.NguoiDungID == AccountCurrent.Acc.NguoiDungID);
            if(!checkDc) return RedirectToAction("Address", "Account");
            if (selectedItems == null || selectedItems.Count == 0)
            {
                    return RedirectToAction("Cart", "Cart");
            }

            var selectedProducts = db.GioHangs
                .Where(gh => selectedItems.Contains(gh.GioHangID)) 
                .Select(gioHang => new ProductBuys
                {
                    HinhAnh = gioHang.SanPham.HinhAnh,
                    TenSanPham = gioHang.SanPham.TenSanPham,
                    KickThuocID = gioHang.KichThuocSanPham.KichThuocID,
                    SanPhamID = gioHang.SanPham.SanPhamID,
                    SoLuong = gioHang.SoLuong,
                    Gia = gioHang.SanPham.Gia
                })
                .ToList();

            var diaChis = db.DiaChis
                .Where(dc => dc.NguoiDungID == AccountCurrent.Acc.NguoiDungID)
                .ToList();

            var tongSoLuong = selectedProducts.Sum(sp => sp.SoLuong);
            var tongTien = selectedProducts.Sum(sp => sp.Gia * sp.SoLuong);

            var buyView = new BuyView
            {
                TongSoLuong = tongSoLuong,
                TongTongTien = tongTien,
                ProductBuy = selectedProducts,  
                DiaChis = diaChis

            };

            return View(buyView);
        }
        [HttpPost]
        public IActionResult BuyBuy(List<ProductBuys> buy, string thanhtoan, int diachi)
        {
            if (thanhtoan == "chuyenkhoan")
            {
                return NotFound(); 
            }
            decimal tongGia = 0;

            var donHang = new DonHang
            {
                NguoiDungID = AccountCurrent.Acc.NguoiDungID, 
                ShiperID = null, 
                DiaChiID = diachi,
                NgayDatHang = DateTime.Now,
                TrangThai = TrangThai.NotConfirmed,
                TongGia = 0
            };
            db.DonHangs.Add(donHang);
            db.SaveChanges();

            foreach (var product in buy)
            {
                var chiTiet = new ChiTietDonHang
                {
                    DonHangID = donHang.DonHangID,
                    SanPhamID = product.SanPhamID,
                    KichThuocID = product.KickThuocID,
                    SoLuong = product.SoLuong,
                    DonGia = product.Gia
                };
                tongGia += chiTiet.SoLuong * chiTiet.DonGia;

                db.ChiTietDonHangs.Add(chiTiet);

                var updateDt = db.DoanhThus.FirstOrDefault(dt => dt.Ngay == DateTime.Now.Date && dt.SanPhamID == product.SanPhamID);
                if(updateDt != null)
                {
                    updateDt.SoLuongBan += chiTiet.SoLuong;
                    updateDt.TongDoanhThu += tongGia;
                    db.SaveChanges();
                }
                else
                {
                    DoanhThu doanhThu = new DoanhThu
                    {
                        SanPhamID = product.SanPhamID,
                        Ngay = DateTime.Now,
                        SoLuongBan = chiTiet.SoLuong,
                        TongDoanhThu = tongGia
                    };
                    db.Add(doanhThu);
                    db.SaveChanges();
                }
                db.SaveChanges();
            }

            donHang.TongGia = tongGia;
            db.SaveChanges();
            MailSetup.SendMail("ĐẶT HÀNG THÀNH CÔNG", MailSetup.ConfiOrder, AccountCurrent.Acc.Email);


            return RedirectToAction("Index","Home"); 
        }


        public IActionResult Order()
        {
            if (AccountCurrent.Acc != null)
            {
                var orders = db.DonHangs
                    .Where(o => o.NguoiDungID == AccountCurrent.Acc.NguoiDungID)
                    .Select(o => new OrderViewModel
                    {
                        DonHangID = o.DonHangID,
                        NgayDatHang = o.NgayDatHang,
                        TrangThai = o.TrangThai,
                        TongGia = o.TongGia,
                        DiaChi = o.DiaChi.DiaChiChiTiet,
                        ChiTietDonHang = o.ChiTietDonHangs.Select(ct => new OrderDetailViewModel
                        {
                            HinhAnh = ct.SanPham.HinhAnh,
                            TenSanPham = ct.SanPham.TenSanPham,
                            SanPhamID = ct.SanPhamID,
                            SoLuong = ct.SoLuong,
                            DonGia = ct.DonGia,
                            DaDanhGia = db.DanhGiaSanPhams.Any(d=>d.SanPhamID == ct.SanPhamID && d.NguoiDungID == o.NguoiDungID)
                        }).ToList()
                    })
                    .ToList();

                return View(orders);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }



        public IActionResult Review(int id)
        {
            return View(db.SanPhams.FirstOrDefault(x=>x.SanPhamID == id));
        }
        public IActionResult SubmitReview(int id, int diem, string danhgia)
        {
            var newreview = new DanhGiaSanPham
            {
                NguoiDungID = AccountCurrent.Acc.NguoiDungID,
                SanPhamID = id,
                DiemDanhGia = diem,
                BinhLuan = danhgia,
                NgayDanhGia = DateTime.Now

            };
            db.Add(newreview);
            db.SaveChanges();
            return RedirectToAction("Order");

        }
    }

    public class CartView
    {
        public int GioHangID { get; set; }
        public string HinhAnh { get; set; }
        public string TenSanPham { get; set; }
        public int SanPhamID { get; set; }
        public int SoLuong { get; set; }
        public KichThuocSanPham KichThuoc { get; set; }
        public decimal Gia { get; set; }

    }
    public class OrderViewModel
    {
        public int DonHangID { get; set; }
        public DateTime NgayDatHang { get; set; }
        public string TrangThai { get; set; }
        public decimal TongGia { get; set; }
        public string DiaChi { get; set; }
        public List<OrderDetailViewModel> ChiTietDonHang { get; set; }
    }

    public class OrderDetailViewModel
    {
        public string HinhAnh { get; set; }
        public string TenSanPham { get; set; }
        public int SanPhamID { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public bool DaDanhGia { get; set; }
    }

    public class BuyView{
       public int TongSoLuong { get; set; }
        public decimal TongTongTien { get; set; }

        public List<ProductBuys> ProductBuy { get; set; }
        public List<DiaChi> DiaChis { get; set; }

    }
    public class ProductBuys{
       public string HinhAnh { get; set; }
       public string TenSanPham { get; set; }
       public int SanPhamID { get; set; }
       public int KickThuocID { get; set; }
       public int SoLuong { get; set; }
       public decimal Gia { get; set; }

    }
     
    
}
