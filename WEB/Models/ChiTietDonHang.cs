using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class ChiTietDonHang
    {
        [Key]
        public int ChiTietDonHangID { get; set; }

        [ForeignKey("DonHang")]
        public int DonHangID { get; set; }
        public DonHang? DonHang { get; set; }

        [ForeignKey("SanPham")]
        public int SanPhamID { get; set; }
        public SanPham? SanPham { get; set; }

        [ForeignKey("KichThuocSanPham")]
        public int? KichThuocID { get; set; }
        public KichThuocSanPham? KichThuocSanPham { get; set; }

        [Required]
        public int SoLuong { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal DonGia { get; set; }
    }
}
