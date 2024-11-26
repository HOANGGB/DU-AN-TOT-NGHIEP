using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WEB.Models;

namespace WEB.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext db;


        public ManagerController(ApplicationDbContext context)
        {
            db = context;
        }


        ///USER MANAGER START

        public IActionResult User(int? id, string? HoTen)
        {
            NguoiDung ndht = null;
            var nds = db.NguoiDungs.ToList();

            if (!string.IsNullOrEmpty(HoTen))
            {
                nds = db.NguoiDungs.Where(nd => nd.HoTen.ToLower().Contains(HoTen.ToLower())).ToList();
            }
            else if (id.HasValue)
            {
                ndht = db.NguoiDungs.FirstOrDefault(nd => nd.NguoiDungID == id.Value);
            }
            var model = new UserView
            {
                Nguoidungs = nds,
                NguoidungHT = ndht
            };

            return View(model);
        }


        public IActionResult EditUser(int id)
        {
            return RedirectToAction("User", new { id = id });

        }
        public IActionResult UpdateUser(NguoiDung nd, string VaiTro)
        {

            var ndcheck = db.NguoiDungs.FirstOrDefault(x => x.NguoiDungID == nd.NguoiDungID);
/*            if(db.NguoiDungs.Any(n=>n.Email == ndcheck.Email || n.SoDienThoai == ndcheck.SoDienThoai)) return RedirectToAction("User");
*/            if (ndcheck != null)
            {
                ndcheck.HoTen= nd.HoTen;
                ndcheck.Email= nd.Email;
                ndcheck.SoDienThoai = nd.SoDienThoai;
                ndcheck.VaiTro = VaiTro;
                ndcheck.MatKhauHash = nd.MatKhauHash;



                db.SaveChanges();
              
            }
            else
            {

                NguoiDung newNd = new NguoiDung
            {
                    HinhAnh = "avateeee.jpg",
                    HoTen = nd.HoTen,
                    Email = nd.Email,
                    SoDienThoai = nd.SoDienThoai,
                    VaiTro = VaiTro,
                    MatKhauHash = nd.MatKhauHash,

                };

                db.Add(newNd);
                db.SaveChanges();
            }
            return RedirectToAction("User");
        }
        public IActionResult DeleteUser(int id)
        {
            var nd = db.NguoiDungs.FirstOrDefault(x=>x.NguoiDungID==id);
            db.Remove(nd);
            db.SaveChanges();
            return RedirectToAction("User");
        }
        ///USER MANAGER END



        ///PRODUCT MANAGER START
        public IActionResult Product(int? id,string? TenSanPham)
        {
            SanPham spht = null;
            var sps = db.SanPhams.ToList();

            if (id.HasValue)
                spht = db.SanPhams.FirstOrDefault(nd => nd.SanPhamID == id.Value);
            else if (!string.IsNullOrEmpty(TenSanPham))
                sps = db.SanPhams.Where(nd => nd.TenSanPham.ToLower().Contains(TenSanPham.ToLower())).ToList();
            var model = new ProductView
            {
                Sanphams = sps,
                DanhMucSanPhams = db.DanhMucSanPhams.ToList(),
                SanphamHT = spht,

            };


            return View(model);
        }
        public IActionResult EditProduct(int id)
        {
            return RedirectToAction("Product", new { id = id });

        }
        
        public IActionResult DetailKT(int id,int? idkt)
        {
            var listKT = db.KichThuocSanPhams.Where(k => k.SanPhamID == id).ToList();
            var model = new KichThuocView
            {
                KTSP = listKT,
                SanPhamID = id,
            };
            if (idkt.HasValue) model.KichThuocHT = db.KichThuocSanPhams.FirstOrDefault(k => k.KichThuocID == idkt);
            return View(model);

        } 
        public IActionResult EditKTSP(int id, int? idkt)
        {
            return RedirectToAction("DetailKT", new { id = id ,idkt = idkt});

        }


        public IActionResult AddKichThuoc(KichThuocSanPham kt,  int id)
        {
            Console.WriteLine("KIID  =  " + kt.KichThuocID);

            var checkKT = db.KichThuocSanPhams.FirstOrDefault(k => k.KichThuocID == kt.KichThuocID);
            if (checkKT != null)
            {
                checkKT.KichThuoc = kt.KichThuoc;
                checkKT.SoLuongTon = kt.SoLuongTon;
            }
            else
            {
                var newkt = new KichThuocSanPham
                {
                    KichThuoc = kt.KichThuoc,
                    SanPhamID = id,
                    SoLuongTon = kt.SoLuongTon
                };
                if (newkt.SoLuongTon < 1) newkt.SoLuongTon = 1;

                db.Add(newkt);
            }
            db.SaveChanges();

            return RedirectToAction("DetailKT", new { id = id });
        }

        public IActionResult UpdateProduct(SanPham sp,KichThuocSanPham kt, int DMID, IFormFile HinhAnh)
        {

            var spcheck = db.SanPhams.FirstOrDefault(x => x.SanPhamID == sp.SanPhamID);
            if (HinhAnh != null && HinhAnh.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", HinhAnh.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    HinhAnh.CopyTo(stream);
                }

            }
            if (spcheck != null)
            {
                spcheck.TenSanPham = sp.TenSanPham;
                spcheck.MoTa = sp.MoTa;
                spcheck.Gia = sp.Gia;
                spcheck.DanhMucID = DMID;
                if (HinhAnh != null && HinhAnh.Length > 0)
                {
                    spcheck.HinhAnh = HinhAnh.FileName;
                }

                db.SaveChanges();

            }
            else
            {
                SanPham newSP = new SanPham
                {
                    TenSanPham = sp.TenSanPham,
                    MoTa = sp.MoTa,
                    Gia = sp.Gia,
                    DanhMucID = DMID
                };
                
                if (HinhAnh != null && HinhAnh.Length > 0)
                {
                    newSP.HinhAnh = HinhAnh.FileName;
                }
                else
                {
                    newSP.HinhAnh = "wwwroot/img/images.png";
                }
                db.Add(newSP);
                db.SaveChanges();
                KichThuocSanPham newKT = new KichThuocSanPham
                {
                    KichThuoc = kt.KichThuoc,
                    SanPhamID = newSP.SanPhamID,
                    SoLuongTon = kt.SoLuongTon
                };
                if(newKT.SoLuongTon <1) newKT.SoLuongTon =1;
                db.Add(newKT); 
                db.SaveChanges();

            }
            return RedirectToAction("Product");
        }




        ///PRODUCT MANAGER END


        public IActionResult Order(string? status)
        {
            var o = db.DonHangs.ToList();
            if (!string.IsNullOrEmpty(status))
            {
                if(status == "moi")  o = db.DonHangs.OrderByDescending(nd => nd.DonHangID).ToList();
                else if(status == "cu") o = db.DonHangs.OrderBy(nd => nd.DonHangID).ToList();
                else o = db.DonHangs.Where(nd => nd.TrangThai == status).ToList();
            }
            return View(o);
        }
         public IActionResult DetailOrder(int id)
        {
            var dh = db.DonHangs.FirstOrDefault(dh => dh.DonHangID == id);
            var ctDonhang = (from ct in db.ChiTietDonHangs
                             join sp in db.SanPhams on ct.SanPhamID equals sp.SanPhamID
                             join kt in db.KichThuocSanPhams on ct.KichThuocID equals kt.KichThuocID
                             where ct.DonHangID == dh.DonHangID
                             select new ChiTietDonHangView
                             {
                                 SanPhamID = sp.SanPhamID,
                                 TenSanPham = sp.TenSanPham,
                                 KichThuoc = kt.KichThuoc,
                                 SoLuong = ct.SoLuong,
                                 DonGia = ct.DonGia
                             }).ToList();

            var dt = new OrderDetailView
            {
                Donhang = dh,
                CtDonhang = ctDonhang,
                Nguoidung = db.NguoiDungs.FirstOrDefault(nd =>nd.NguoiDungID == dh.NguoiDungID),
                Shiper = db.NguoiDungs.FirstOrDefault(s => s.NguoiDungID == dh.ShiperID),
                Diachi = db.DiaChis.FirstOrDefault(d =>d.DiaChiID == dh.DiaChiID)
            };
            return View(dt);
        }
        public IActionResult ConfirmOrder(int id)
        {
            var dh = db.DonHangs.FirstOrDefault(dh =>dh.DonHangID == id);
            dh.TrangThai = TrangThai.Pending;
            db.SaveChanges();
            return RedirectToAction("Order");
        }
        public IActionResult ConfirmedAll()
        {
            var dhhh = db.DonHangs.ToList();
            for(int i= 0; i < dhhh.Count; i++)
            {
                if (dhhh[i].TrangThai == TrangThai.NotConfirmed) dhhh[i].TrangThai = TrangThai.Pending;
            }
            db.SaveChanges();
            return RedirectToAction("Order");
        }

        public IActionResult CancelOrder(int id)
        {
            var dh = db.DonHangs.FirstOrDefault(dh => dh.DonHangID == id);
            dh.TrangThai = TrangThai.Cancelled;
            db.SaveChanges();
            return RedirectToAction("Order");
        }

        ///CATEGORY MANAGER START

        public IActionResult Category(int? id)
        {
            DanhMucSanPham dmspht = null;
            var dms = db.DanhMucSanPhams.ToList();

            if (id.HasValue) dmspht = db.DanhMucSanPhams.FirstOrDefault(dm => dm.DanhMucID == id.Value);
            var model = new CategoryView
            {
                DanhMucSanPhams = dms,
                DMSP = dmspht
            };

            return View(model);
        }

         public IActionResult EditCategory(int id)
        {
            return RedirectToAction("Category", new { id = id });
        }
        public IActionResult UpdateCategory(DanhMucSanPham dm)
        {
            var dmcheck = db.DanhMucSanPhams.FirstOrDefault(x => x.DanhMucID == dm.DanhMucID);
            if (dmcheck != null)
            {
                dmcheck.TenDanhMuc = dm.TenDanhMuc;
                db.SaveChanges();

            }
            else
            {
                DanhMucSanPham newDM = new DanhMucSanPham
                {
                    TenDanhMuc = dm.TenDanhMuc
                };
                db.Add(newDM);
                db.SaveChanges();
            }
            return RedirectToAction("Category");
        }
        public IActionResult DeleteCategory(int id)
        {
            var cdel = db.DanhMucSanPhams.FirstOrDefault(x=>x.DanhMucID==id);
            db.DanhMucSanPhams.Remove(cdel);
            db.SaveChanges();
            return RedirectToAction("Category");

        }




        ///CATEGORY MANAGER END


        ///SHIPER MANAGER START

        public IActionResult Shiper(int? id)
        {
            List<DonHang> dhht = null;
            var sps = db.NguoiDungs.Where(x=>x.VaiTro== Vaitro.Shiper).ToList();

            if (id.HasValue) dhht = db.DonHangs.Where(dh => dh.ShiperID == id.Value).ToList();
            var model = new ShiperView
            {
                Shipers = sps,
                DonHangs = dhht
            };

            return View(model);
        }

        public IActionResult ShiperViewOrder(int id)
        {
            return RedirectToAction("Shiper", new {id = id});
        }

        public IActionResult ShiperDetailOrder(int id) 
        {
            return View();

        }


        ///SHIPER MANAGER END


        ///REVIEW MANAGER START

        public IActionResult Review(string? fill)
        {
            var dg = db.DanhGiaSanPhams.ToList();
            if (!string.IsNullOrEmpty(fill))
            {
                if(fill == "diemsocao")
                {
                    dg = db.DanhGiaSanPhams.OrderByDescending(d=>d.DiemDanhGia).ToList();
                }else if(fill == "diemsothap")
                {
                    dg = db.DanhGiaSanPhams.OrderBy(d=>d.DiemDanhGia).ToList();
                }
                else if(fill == "moi")
                {
                    dg = db.DanhGiaSanPhams.OrderByDescending(d=>d.DanhGiaID).ToList();
                }
                else if(fill == "cu")
                {
                    dg = db.DanhGiaSanPhams.OrderBy(d=>d.DanhGiaID).ToList();
                }

            }
            return View(dg);
        }

        public IActionResult DeleteReview(int id)
        {
            var dgDel = db.DanhGiaSanPhams.FirstOrDefault(x=>x.DanhGiaID == id);
            db.Remove(dgDel);
            db.SaveChanges();
            return RedirectToAction("Review");
        }


        ///REVIEW MANAGER END
        
        
        ///REVENUE MANAGER START
        public IActionResult Revenue(int? Ngay,int? Thang,int? Nam) 
        { 

        
            var model = new RevenueView
            {
               /* Ngays = db.DoanhThus.Select(n=>n.Ngay).Distinct().ToList(),
                Thangs = db.DoanhThus.Select(n=> new DateTime(n.Ngay.Year, n.Ngay.Month, 1)).Distinct().ToList(),
                Nams = db.DoanhThus.Select(n=>n.Ngay.Year).Distinct().Select(year => new DateTime(year, 1, 1)).ToList()*/
            };

                // Lưu doanh thu theo từng ngày
                model.DoanhthuNgays = db.DoanhThus
                    .GroupBy(dt => new { dt.Ngay, dt.SanPhamID }) // Nhóm theo ngày và SanPhamID
                    .Select(g => new DoanhThu
                    {
                        SanPhamID = g.Key.SanPhamID,
                        Ngay = g.Key.Ngay,
                        SoLuongBan = g.Sum(x => x.SoLuongBan), // Tính tổng số lượng bán ra
                        TongDoanhThu = g.Sum(x => x.TongDoanhThu) // Tính tổng tiền
                    })
                    .ToList();
           
                // Lưu doanh thu theo từng tháng
                model.DoanhthuThangs = db.DoanhThus
                    .GroupBy(dt => new { Year = dt.Ngay.Year, Month = dt.Ngay.Month, dt.SanPhamID }) // Nhóm theo năm, tháng và SanPhamID
                    .Select(g => new DoanhThu
                    {
                        SanPhamID = g.Key.SanPhamID,
                        Ngay = new DateTime(g.Key.Year, g.Key.Month, 1), // Tạo đối tượng DateTime với ngày là 1
                        SoLuongBan = g.Sum(x => x.SoLuongBan), // Tính tổng số lượng bán ra
                        TongDoanhThu = g.Sum(x => x.TongDoanhThu) // Tính tổng tiền
                    })
                    .ToList();

                // Lưu doanh thu theo từng năm
                model.DoanhthuNams = db.DoanhThus
                    .GroupBy(dt => new { Year = dt.Ngay.Year, dt.SanPhamID }) // Nhóm theo năm và SanPhamID
                    .Select(g => new DoanhThu
                    {
                        SanPhamID = g.Key.SanPhamID,
                        Ngay = new DateTime(g.Key.Year, 1, 1), // Tạo đối tượng DateTime cho năm
                        SoLuongBan = g.Sum(x => x.SoLuongBan), // Tính tổng số lượng bán ra
                        TongDoanhThu = g.Sum(x => x.TongDoanhThu) // Tính tổng tiền
                    })
                    .ToList();
            



             return View(model);



        }



    }
    public class UserView
    {
        public List<NguoiDung> Nguoidungs { get; set; }
        public NguoiDung NguoidungHT { get; set; }
        public string TK { get; set; }
    }
    public class ProductView
    {
        public List<SanPham> Sanphams { get; set; }
        public List<DanhMucSanPham> DanhMucSanPhams { get; set; }
        public SanPham SanphamHT { get; set; }
    }
    public class KichThuocView
    {
        public List<KichThuocSanPham> KTSP { get; set; }
        public KichThuocSanPham KichThuocHT { get; set; }
        public int SanPhamID { get; set; }
    }
    public class OrderDetailView
    {
        public DonHang Donhang { get; set; }
        public List<ChiTietDonHangView> CtDonhang { get; set; }
        public NguoiDung Nguoidung { get; set; }
        public NguoiDung Shiper { get; set; }
        public DiaChi Diachi { get; set; }

    }
    public class ChiTietDonHangView
    {
        public int SanPhamID { get; set; }
        public string TenSanPham { get; set; }
        public string KichThuoc { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
    }

    public class CategoryView
    {
        public List<DanhMucSanPham> DanhMucSanPhams { get; set; }
        public DanhMucSanPham DMSP { get; set; }
    }
    public class ShiperView 
    {
        public List<NguoiDung> Shipers { get; set; }
        public List<DonHang> DonHangs { get; set;}
    }
     public class RevenueView
    {
        public List<DoanhThu> DoanhthuNgays { get; set; }
        public List<DoanhThu> DoanhthuThangs { get; set;}
        public List<DoanhThu> DoanhthuNams { get; set; }  

        public DoanhThu DTN { get; set; }
        public DoanhThu DTT { get; set;}
        public DoanhThu STN { get; set; }
        public List<DateTime> Ngays { get; set; }
        public List<DateTime> Thangs { get; set; }
        public List<DateTime> Nams { get; set; }


        public int? Nam { get; set; }

    }

}
