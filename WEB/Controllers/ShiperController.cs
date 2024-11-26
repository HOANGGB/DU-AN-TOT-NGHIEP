using Microsoft.AspNetCore.Mvc;
using WEB.Models;

namespace WEB.Controllers
{
    public class ShiperController : Controller
    {
        private readonly ApplicationDbContext db;


        public ShiperController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Pending()
        {
            var dh = db.DonHangs.Where(dh => dh.TrangThai == TrangThai.Pending).ToList();

            return View(dh);
        }
        public IActionResult InProgress()
        {
            var dh = db.DonHangs.Where(dh => dh.TrangThai == TrangThai.Processing 
                                          && dh.ShiperID == AccountCurrent.Acc.NguoiDungID).ToList();
            return View(dh);
        }
        public IActionResult Delivered()
        {
            var dh = db.DonHangs.Where(dh => dh.TrangThai == TrangThai.Completed
                                          && dh.ShiperID == AccountCurrent.Acc.NguoiDungID).ToList();
            return View(dh);
        }
        public IActionResult Cancelled()
        {
            var dh = db.DonHangs.Where(dh => dh.TrangThai == TrangThai.Cancelled
                                          && dh.NguoiDungID == AccountCurrent.Acc.NguoiDungID).ToList();
            return View(dh);
        }
        public IActionResult ConfirmDelivery(int id)
        {
            var dh = db.DonHangs.FirstOrDefault(d => d.DonHangID == id);
            dh.TrangThai = TrangThai.Processing;
            dh.ShiperID = AccountCurrent.Acc.NguoiDungID;
            db.SaveChanges();
            return RedirectToAction("InProgress");
        }
        public IActionResult ConfirmComplete(int id)
        {
            var dh = db.DonHangs.FirstOrDefault(d => d.DonHangID == id);
            dh.TrangThai = TrangThai.Completed;
            db.SaveChanges();
            return RedirectToAction("Delivered");
        }
        public IActionResult Cancel(int id)
        {
            var dh = db.DonHangs.FirstOrDefault(d => d.DonHangID == id);
            dh.TrangThai = TrangThai.Cancelled;
            db.SaveChanges();
            return RedirectToAction("Cancelled");
        }



    }

    public class DHCT
    {
        public DonHang Donhang { get; set; }
        public ChiTietDonHang Donhangchitiet { get; set; }
        public NguoiDung Nguoidung { get; set; }
        public SanPham Sanpham { get; set; }
        public DiaChi Diachi { get; set; }



    }
}
