using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class DanhMucSanPham
    {
        [Key]
        public int DanhMucID { get; set; }

        [Required, MaxLength(100)]
        public string? TenDanhMuc { get; set; }

        public ICollection<SanPham>? SanPhams { get; set; }
    }
}
