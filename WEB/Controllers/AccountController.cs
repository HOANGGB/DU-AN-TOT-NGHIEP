using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WEB.Models;
using System.Net.Mail;
using System.Net;
using WEB.Models.Mail;

namespace WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext db;


        public AccountController(ApplicationDbContext context)
        {
            db = context;
        }
        public ActionResult Login()
        {

            return View();


        }
        public IActionResult Register()
        {
            return View();
        }
         public IActionResult ForgotPassword()
        {
            return View();
        }
        public IActionResult ResetPass(string email)
        {
            var checkMail = db.NguoiDungs.FirstOrDefault(x => x.Email == email);
            if (checkMail == null)
            {
                ViewBag.MessER = "Email không tồn tại!";
                return View("ForgotPassword");
            }
            else
            {
                MailSetup.SendMail("CẤP LẠI MẬT KHẨU",MailSetup.BodyResetPassword, email);
                
                checkMail.MatKhauHash = "12345";
                db.SaveChanges();


                ViewBag.MessSucc = "Mật khẩu mới đã được cấp lại trong email.\n Vui lòng kiểm tra để đăng nhập!";
                return View("ForgotPassword");
            }
        }
        
        [HttpPost]
        public IActionResult CheckRegister(NguoiDung nd)
        {
            bool hasError = false;

            if (string.IsNullOrWhiteSpace(nd.HoTen) || nd.HoTen.Length < 3)
            {
                ViewBag.MessHoten = "Họ và tên phải có ít nhất 3 ký tự!";
                hasError = true;
            }

            var emailExists = db.NguoiDungs.Any(x => x.Email == nd.Email);
            if (emailExists)
            {
                ViewBag.MessEmail = "Email đã tồn tại!";
                hasError = true;
            }

            string phonePattern = @"^0\d{9,10}$"; 
            if (string.IsNullOrWhiteSpace(nd.SoDienThoai) || !Regex.IsMatch(nd.SoDienThoai, phonePattern))
            {
                ViewBag.MessSdt = "Định dạng sđt 10-11 chữ số bắt đầu bằng 0!";
                hasError = true;
            }

            if (hasError)
            {
                return View("Register");
            }

            MailSetup.SendMail("MẬT KHẨU ĐĂNG NHẬP", "MẬT KHẨU CỦA BẠN LÀ : Nguoidung@123", nd.Email);
            nd.MatKhauHash = "Nguoidung@123";
            nd.HinhAnh = "avateeee.jpg";
            nd.VaiTro = Vaitro.Customer;
            db.NguoiDungs.Add(nd);
            db.SaveChanges();

            
            ViewBag.MessSendMail = "Mật khẩu đã được cấp trong Email.\n Vui lòng đăng nhập và đổi mật khẩu";
            return View("Register");
        }

        public IActionResult CheckLogin(string? email, string? pass)
        {
            var checkNd = db.NguoiDungs.FirstOrDefault(x => x.Email == email);
            if (checkNd == null)
            {
                ViewBag.MessER = "Email không tồn tại!";
                return View("Login");
            }
            else
            {
                if(checkNd.MatKhauHash != pass)
                {
                    ViewBag.MessER = "Sai mật khẩu!";
                    return View("Login");
                }
                else
                {
                    AccountCurrent.SetUser(checkNd);
                    return RedirectToAction("Index", "Home");

                }

            }
        }

        public IActionResult Address()
        {
            var dc = db.DiaChis.Where(dc => dc.NguoiDungID == AccountCurrent.Acc.NguoiDungID).ToList();
            return View(dc);
        }
        public IActionResult AddAddress(DiaChi newDc)
        {
            if (!string.IsNullOrWhiteSpace(newDc.DiaChiChiTiet)){
                db.Add(newDc);
                db.SaveChanges();
            }
            return RedirectToAction("Address");
        }


        public IActionResult Logout()
        {
            AccountCurrent.Logout(); 
            return RedirectToAction("Login");

        }
    }
}
