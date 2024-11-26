using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class DoanhThu
    {
        [Key]
        public int DoanhThuID { get; set; } 

        [Required]
        public int SanPhamID { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Ngay { get; set; } 

        [Required]
        public int SoLuongBan { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal TongDoanhThu { get; set; }

        [ForeignKey("SanPhamID")]
        public virtual SanPham SanPham { get; set; }
    }
}
