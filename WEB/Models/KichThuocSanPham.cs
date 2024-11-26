using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class KichThuocSanPham
    {
        [Key]
        public int KichThuocID { get; set; }

        [ForeignKey("SanPham")]
        public int SanPhamID { get; set; }
        public SanPham? SanPham { get; set; }

        [Required, MaxLength(50)]
        public string? KichThuoc { get; set; }

        [Required]
        public int SoLuongTon { get; set; }

    }
}
