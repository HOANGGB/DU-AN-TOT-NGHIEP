using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class GioHang
    {
        [Key]
        public int GioHangID { get; set; }

        [ForeignKey("NguoiDung")]
        public int NguoiDungID { get; set; }
        public NguoiDung? NguoiDung { get; set; }

        [ForeignKey("SanPham")]
        public int SanPhamID { get; set; }
        public SanPham? SanPham { get; set; }

        [ForeignKey("KichThuocSanPham")]
        public int? KichThuocID { get; set; }
        public KichThuocSanPham? KichThuocSanPham { get; set; }

        [Required]
        public int SoLuong { get; set; }
    }
}
