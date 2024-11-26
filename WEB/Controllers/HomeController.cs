using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using WEB.Models;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;


        public HomeController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var products = db.SanPhams.ToList();
            var categories = db.DanhMucSanPhams.ToList();
            var tb = db.ThongBaos.Where(tb => tb.NguoiDungID == 1).ToList();

            var viewModel = new HomeViewModel
            {
                SanPhams = products,
                DanhMucs = categories,
                ThongBaos = tb

            };

            return View(viewModel);
        }
        public IActionResult Seach(string TenSanPham)
        {
            List<SanPham> sps = null;
            if (!string.IsNullOrEmpty(TenSanPham))
            {
                sps = db.SanPhams.Where(nd => nd.TenSanPham.ToLower().Contains(TenSanPham.ToLower())).ToList();
                return View(sps);
            }
            else
            {
                return View("Index");
            }
        }
        public IActionResult Detail(int? ktid,int id,int sl=1)
        {
            // Lấy giá trị từ ViewBag.sl hoặc đặt giá trị mặc định là 1 nếu ViewBag.sl chưa tồn tại
            if (ViewBag.sl == null) ViewBag.sl = 1;

            var sp = db.SanPhams.FirstOrDefault(sp => sp.SanPhamID == id);
            var danhGia = db.DanhGiaSanPhams.FirstOrDefault(d => d.SanPhamID == id);
            float ddd = danhGia != null ? danhGia.DiemDanhGia : 5;
            int ldd = db.DanhGiaSanPhams.Count(l => l.SanPhamID == id);
            var ktsp = db.KichThuocSanPhams.Where(k => k.SanPhamID == id).ToList();
            var dgsp = (from dg in db.DanhGiaSanPhams
                        join nd in db.NguoiDungs
                        on dg.NguoiDungID equals nd.NguoiDungID
                        where dg.SanPhamID == id
                        select new ReviewView
                        {
                            Avata = nd.HinhAnh, 
                            TenNguoiDung = nd.HoTen, 
                            DiemDG = dg.DiemDanhGia,
                            NgayDG = dg.NgayDanhGia,
                            NoiDung = dg.BinhLuan 
                        }).ToList();
            var end = new HomeDetail
            {
                SanPhams = sp,
                KTSP = ktsp,
                diemDanhGia = ddd,
                luotDanhGia = ldd,
                SL= sl,
                DanhGias = dgsp
            };

            if (ktid.HasValue)
            {
                end.KTSPHT = db.KichThuocSanPhams.FirstOrDefault(k => k.KichThuocID == ktid);
            }
            else
            {
                end.KTSPHT = db.KichThuocSanPhams.FirstOrDefault(k => k.SanPhamID == id);
            }
            if (end.SL > end.KTSPHT.SoLuongTon) end.SL = end.KTSPHT.SoLuongTon;

            return View(end);
        }

        public IActionResult CapnhatSLKT(int id, string cong_tru,int sl,int ktid)
        {
            
            if (cong_tru == "cong")
            {
                var checksl = db.KichThuocSanPhams.FirstOrDefault(k => k.KichThuocID == ktid).SoLuongTon;
                if(sl < checksl) sl++; 
            }
            else if(cong_tru =="tru")
            {
                if(sl > 1) sl--;
            }


            return RedirectToAction("Detail", new { id = id,sl = sl ,ktid = ktid});
        }


        public IActionResult Info()
        {
            return View(AccountCurrent.Acc);
        }
         public IActionResult ChangeInfo(NguoiDung nd, IFormFile HinhAnh)
        {
            bool hasError = false;
            var ndht = db.NguoiDungs.FirstOrDefault(n=>n.NguoiDungID== nd.NguoiDungID);
            if (string.IsNullOrWhiteSpace(nd.HoTen) || nd.HoTen.Length < 3)
            {
                ViewBag.MessHoten = "Họ và tên phải có ít nhất 3 ký tự!";
                hasError = true;
            }


            string phonePattern = @"^0\d{9,10}$";
            if (string.IsNullOrWhiteSpace(nd.SoDienThoai) || !Regex.IsMatch(nd.SoDienThoai, phonePattern))
            {
                ViewBag.MessSdt = "Định dạng sđt 10-11 chữ số bắt đầu bằng 0!";
                hasError = true;
            }

            /*if (string.IsNullOrWhiteSpace(nd.MatKhauHash) || nd.MatKhauHash.Length < 5)
            {
                ViewBag.MessMatkhau = "Mật khẩu phải có ít nhất 5 ký tự!";
                hasError = true;
            }*/
            if (string.IsNullOrWhiteSpace(nd.MatKhauHash) || !Regex.IsMatch(nd.MatKhauHash, @"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$"))
            {
                ViewBag.MessMatkhau = "Mật khẩu phải có ít nhất 6 ký tự, bao gồm chữ in hoa, số và ký tự đặc biệt!";
                hasError = true;
            }

            if (HinhAnh != null && HinhAnh.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", HinhAnh.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    HinhAnh.CopyTo(stream);
                }

            }

            if (hasError)
            {
                return View("Info");
            }
            ndht.HoTen = nd.HoTen;
            ndht.SoDienThoai = nd.SoDienThoai;
            ndht.MatKhauHash = nd.MatKhauHash;
            if (HinhAnh != null && HinhAnh.Length > 0)
            {
                ndht.HinhAnh = HinhAnh.FileName;
            }
            AccountCurrent.SetUser(ndht);
            db.SaveChanges();

            return RedirectToAction("Info");
        }


    }

    public class HomeViewModel
    {
        public List<SanPham> SanPhams { get; set; }
        public List<DanhMucSanPham> DanhMucs { get; set; }
        public List<ThongBao> ThongBaos { get; set; }


    }

    public class HomeDetail
    {
        public SanPham SanPhams { get; set; }
        public List<KichThuocSanPham> KTSP { get; set; }
        public KichThuocSanPham KTSPHT { get; set; }
        public int SL { get; set; }
        public float diemDanhGia { get; set; }
        public int luotDanhGia { get; set; }
        public List<ReviewView> DanhGias { get; set; }

    }

    public class ReviewView
    {
        public string Avata { get; set; }
        public string TenNguoiDung { get; set; }
        public int DiemDG { get; set; }
        public DateTime NgayDG { get; set; }
        public string NoiDung { get; set; }

    }
}