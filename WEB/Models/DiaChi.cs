using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class DiaChi
    {
        [Key]
        public int DiaChiID { get; set; }

        [ForeignKey("NguoiDung")]
        public int NguoiDungID { get; set; }
        public NguoiDung? NguoiDung { get; set; }

        [Required, MaxLength(255)]
        public string? DiaChiChiTiet { get; set; }
    }
}
