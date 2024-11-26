using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class DonHang
    {
        [Key]
        public int DonHangID { get; set; }

        [ForeignKey("NguoiDung")]
        public int NguoiDungID { get; set; }
        public NguoiDung? NguoiDung { get; set; }


        [ForeignKey("DiaChi")]
        public int DiaChiID { get; set; }
        public DiaChi? DiaChi { get; set; }

        [ForeignKey("Shiper")]
        public int? ShiperID { get; set; }
        public NguoiDung? Shiper { get; set; }

        public DateTime NgayDatHang { get; set; } = DateTime.Now;

        [Required, MaxLength(50)]
        public string TrangThai { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal TongGia { get; set; }

        public ICollection<ChiTietDonHang>? ChiTietDonHangs { get; set; }
    }
    public static class TrangThai
    {
        public const string NotConfirmed = "Chưa Xác Nhận";
        public const string Pending = "Chờ Giao";
        public const string Processing = "Đang Giao";
        public const string Completed = "Hoàn Thành";
        public const string Cancelled = "Từ Chối";

        public static List<string> ListTrangThai = new List<string>()
        {
            NotConfirmed,
            Pending,
            Processing,
            Completed,
            Cancelled
        };
    }

}
