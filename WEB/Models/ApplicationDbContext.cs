using Microsoft.EntityFrameworkCore;
using WEB.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }

    public DbSet<NguoiDung> NguoiDungs { get; set; }
    public DbSet<DanhMucSanPham> DanhMucSanPhams { get; set; }
    public DbSet<DonHang> DonHangs { get; set; }
    public DbSet<SanPham> SanPhams { get; set; }
    public DbSet<KichThuocSanPham> KichThuocSanPhams { get; set; }
    public DbSet<DiaChi> DiaChis { get; set; }
    public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
    public DbSet<DanhGiaSanPham> DanhGiaSanPhams { get; set; }
    public DbSet<ThongBao> ThongBaos { get; set; }
    public DbSet<GioHang> GioHangs { get; set; }
    public DbSet<DoanhThu> DoanhThus { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Định nghĩa khóa ngoại từ DonHang đến NguoiDung (NguoiDungID)
        modelBuilder.Entity<DonHang>()
            .HasOne(dh => dh.NguoiDung)
            .WithMany(nd => nd.DonHangs)
            .HasForeignKey(dh => dh.NguoiDungID)
            .OnDelete(DeleteBehavior.NoAction);  // Ngăn việc xóa cascade

        // Định nghĩa khóa ngoại từ DonHang đến DiaChi (DiaChiID)
        modelBuilder.Entity<DonHang>()
            .HasOne(dh => dh.DiaChi)
            .WithMany()
            .HasForeignKey(dh => dh.DiaChiID)
            .OnDelete(DeleteBehavior.NoAction);  // Ngăn việc xóa cascade

        base.OnModelCreating(modelBuilder);
    }
}
