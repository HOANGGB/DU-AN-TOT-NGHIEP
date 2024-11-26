using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class NguoiDung
    {
        [Key]
        public int NguoiDungID { get; set; }

        [Required, MaxLength(100)]
        public string? HoTen { get; set; }

        [Required, MaxLength(255)]
        public string? HinhAnh { get; set; }


        [Required, MaxLength(100)]
        public string? Email { get; set; }

        [Required, MaxLength(255)]
        public string? MatKhauHash { get; set; }

        [Required, MaxLength(20)]
        public string VaiTro { get; set; } 

        [MaxLength(20)]
        public string? SoDienThoai { get; set; }


        public ICollection<DonHang>? DonHangs { get; set; }
        public ICollection<ThongBao>? ThongBaos { get; set; }
        public ICollection<DanhGiaSanPham>? DanhGiaSanPhams { get; set; }
        public ICollection<GioHang>? GioHangs { get; set; }
    }
    public static class Vaitro
    {
        public const string Customer = "Khách Hàng";
        public const string Shiper = "Shiper";
        public const string Admin = "Admin";

        public static List<string> ListVaiTro = new List<string>()
        {
            Admin,
            Customer,
            Shiper
        };
    }
}
