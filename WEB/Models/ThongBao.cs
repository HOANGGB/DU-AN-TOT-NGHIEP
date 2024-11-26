using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class ThongBao
    {
        [Key]
        public int ThongBaoID { get; set; }

        [ForeignKey("NguoiDung")]
        public int NguoiDungID { get; set; }
        public NguoiDung? NguoiDung { get; set; }

        [Required, MaxLength(255)]
        public string? NoiDung { get; set; }

        public bool DaXem { get; set; } = false;

        public DateTime NgayTao { get; set; } = DateTime.Now;
    }
}
