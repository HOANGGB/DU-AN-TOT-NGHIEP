using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class SanPham
    {
        [Key]
        public int SanPhamID { get; set; }

        [Required, MaxLength(100)]
        public string? TenSanPham { get; set; }

        [Required, MaxLength(255)]
        public string? HinhAnh { get; set; } 

        public string? MoTa { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Gia { get; set; }


        [ForeignKey("DanhMucSanPham")]
        public int DanhMucID { get; set; }
        public DanhMucSanPham? DanhMucSanPham { get; set; }

        public ICollection<KichThuocSanPham>? KichThuocSanPhams { get; set; }
        public ICollection<ChiTietDonHang>? ChiTietDonHangs { get; set; }
        public ICollection<DanhGiaSanPham>? DanhGiaSanPhams { get; set; }
        public ICollection<GioHang>? GioHangs { get; set; }
    }
}
