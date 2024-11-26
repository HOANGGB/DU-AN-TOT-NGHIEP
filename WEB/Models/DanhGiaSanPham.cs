using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class DanhGiaSanPham
    {
        [Key]
        public int DanhGiaID { get; set; }

        [ForeignKey("NguoiDung")]
        public int NguoiDungID { get; set; }
        public NguoiDung? NguoiDung { get; set; }

        [ForeignKey("SanPham")]
        public int SanPhamID { get; set; }
        public SanPham? SanPham { get; set; }

        [Range(1, 5)]
        public int DiemDanhGia { get; set; }

        public string? BinhLuan { get; set; }

        public DateTime NgayDanhGia { get; set; } = DateTime.Now;
    }
}
