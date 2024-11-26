﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace WEB.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WEB.Models.ChiTietDonHang", b =>
                {
                    b.Property<int>("ChiTietDonHangID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChiTietDonHangID"));

                    b.Property<decimal>("DonGia")
                        .HasColumnType("decimal(18, 0)");

                    b.Property<int>("DonHangID")
                        .HasColumnType("int");

                    b.Property<int?>("KichThuocID")
                        .HasColumnType("int");

                    b.Property<int>("SanPhamID")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("ChiTietDonHangID");

                    b.HasIndex("DonHangID");

                    b.HasIndex("KichThuocID");

                    b.HasIndex("SanPhamID");

                    b.ToTable("ChiTietDonHangs");
                });

            modelBuilder.Entity("WEB.Models.DanhGiaSanPham", b =>
                {
                    b.Property<int>("DanhGiaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DanhGiaID"));

                    b.Property<string>("BinhLuan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiemDanhGia")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayDanhGia")
                        .HasColumnType("datetime2");

                    b.Property<int>("NguoiDungID")
                        .HasColumnType("int");

                    b.Property<int>("SanPhamID")
                        .HasColumnType("int");

                    b.HasKey("DanhGiaID");

                    b.HasIndex("NguoiDungID");

                    b.HasIndex("SanPhamID");

                    b.ToTable("DanhGiaSanPhams");
                });

            modelBuilder.Entity("WEB.Models.DanhMucSanPham", b =>
                {
                    b.Property<int>("DanhMucID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DanhMucID"));

                    b.Property<string>("TenDanhMuc")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("DanhMucID");

                    b.ToTable("DanhMucSanPhams");
                });

            modelBuilder.Entity("WEB.Models.DiaChi", b =>
                {
                    b.Property<int>("DiaChiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiaChiID"));

                    b.Property<string>("DiaChiChiTiet")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("NguoiDungID")
                        .HasColumnType("int");

                    b.HasKey("DiaChiID");

                    b.HasIndex("NguoiDungID");

                    b.ToTable("DiaChis");
                });

            modelBuilder.Entity("WEB.Models.DoanhThu", b =>
                {
                    b.Property<int>("DoanhThuID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoanhThuID"));

                    b.Property<DateTime>("Ngay")
                        .HasColumnType("date");

                    b.Property<int>("SanPhamID")
                        .HasColumnType("int");

                    b.Property<int>("SoLuongBan")
                        .HasColumnType("int");

                    b.Property<decimal>("TongDoanhThu")
                        .HasColumnType("decimal(18, 0)");

                    b.HasKey("DoanhThuID");

                    b.HasIndex("SanPhamID");

                    b.ToTable("DoanhThus");
                });

            modelBuilder.Entity("WEB.Models.DonHang", b =>
                {
                    b.Property<int>("DonHangID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DonHangID"));

                    b.Property<int>("DiaChiID")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayDatHang")
                        .HasColumnType("datetime2");

                    b.Property<int>("NguoiDungID")
                        .HasColumnType("int");

                    b.Property<int?>("ShiperID")
                        .HasColumnType("int");

                    b.Property<decimal>("TongGia")
                        .HasColumnType("decimal(18, 0)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("DonHangID");

                    b.HasIndex("DiaChiID");

                    b.HasIndex("NguoiDungID");

                    b.HasIndex("ShiperID");

                    b.ToTable("DonHangs");
                });

            modelBuilder.Entity("WEB.Models.GioHang", b =>
                {
                    b.Property<int>("GioHangID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GioHangID"));

                    b.Property<int?>("KichThuocID")
                        .HasColumnType("int");

                    b.Property<int>("NguoiDungID")
                        .HasColumnType("int");

                    b.Property<int>("SanPhamID")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("GioHangID");

                    b.HasIndex("KichThuocID");

                    b.HasIndex("NguoiDungID");

                    b.HasIndex("SanPhamID");

                    b.ToTable("GioHangs");
                });

            modelBuilder.Entity("WEB.Models.KichThuocSanPham", b =>
                {
                    b.Property<int>("KichThuocID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KichThuocID"));

                    b.Property<string>("KichThuoc")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("SanPhamID")
                        .HasColumnType("int");

                    b.Property<int>("SoLuongTon")
                        .HasColumnType("int");

                    b.HasKey("KichThuocID");

                    b.HasIndex("SanPhamID");

                    b.ToTable("KichThuocSanPhams");
                });

            modelBuilder.Entity("WEB.Models.NguoiDung", b =>
                {
                    b.Property<int>("NguoiDungID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NguoiDungID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("HinhAnh")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MatKhauHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SoDienThoai")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("VaiTro")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("NguoiDungID");

                    b.ToTable("NguoiDungs");
                });

            modelBuilder.Entity("WEB.Models.SanPham", b =>
                {
                    b.Property<int>("SanPhamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SanPhamID"));

                    b.Property<int>("DanhMucID")
                        .HasColumnType("int");

                    b.Property<decimal>("Gia")
                        .HasColumnType("decimal(18, 0)");

                    b.Property<string>("HinhAnh")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenSanPham")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("SanPhamID");

                    b.HasIndex("DanhMucID");

                    b.ToTable("SanPhams");
                });

            modelBuilder.Entity("WEB.Models.ThongBao", b =>
                {
                    b.Property<int>("ThongBaoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ThongBaoID"));

                    b.Property<bool>("DaXem")
                        .HasColumnType("bit");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<int>("NguoiDungID")
                        .HasColumnType("int");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("ThongBaoID");

                    b.HasIndex("NguoiDungID");

                    b.ToTable("ThongBaos");
                });

            modelBuilder.Entity("WEB.Models.ChiTietDonHang", b =>
                {
                    b.HasOne("WEB.Models.DonHang", "DonHang")
                        .WithMany("ChiTietDonHangs")
                        .HasForeignKey("DonHangID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WEB.Models.KichThuocSanPham", "KichThuocSanPham")
                        .WithMany()
                        .HasForeignKey("KichThuocID");

                    b.HasOne("WEB.Models.SanPham", "SanPham")
                        .WithMany("ChiTietDonHangs")
                        .HasForeignKey("SanPhamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DonHang");

                    b.Navigation("KichThuocSanPham");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("WEB.Models.DanhGiaSanPham", b =>
                {
                    b.HasOne("WEB.Models.NguoiDung", "NguoiDung")
                        .WithMany("DanhGiaSanPhams")
                        .HasForeignKey("NguoiDungID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WEB.Models.SanPham", "SanPham")
                        .WithMany("DanhGiaSanPhams")
                        .HasForeignKey("SanPhamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NguoiDung");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("WEB.Models.DiaChi", b =>
                {
                    b.HasOne("WEB.Models.NguoiDung", "NguoiDung")
                        .WithMany()
                        .HasForeignKey("NguoiDungID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("WEB.Models.DoanhThu", b =>
                {
                    b.HasOne("WEB.Models.SanPham", "SanPham")
                        .WithMany()
                        .HasForeignKey("SanPhamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("WEB.Models.DonHang", b =>
                {
                    b.HasOne("WEB.Models.DiaChi", "DiaChi")
                        .WithMany()
                        .HasForeignKey("DiaChiID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WEB.Models.NguoiDung", "NguoiDung")
                        .WithMany("DonHangs")
                        .HasForeignKey("NguoiDungID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WEB.Models.NguoiDung", "Shiper")
                        .WithMany()
                        .HasForeignKey("ShiperID");

                    b.Navigation("DiaChi");

                    b.Navigation("NguoiDung");

                    b.Navigation("Shiper");
                });

            modelBuilder.Entity("WEB.Models.GioHang", b =>
                {
                    b.HasOne("WEB.Models.KichThuocSanPham", "KichThuocSanPham")
                        .WithMany()
                        .HasForeignKey("KichThuocID");

                    b.HasOne("WEB.Models.NguoiDung", "NguoiDung")
                        .WithMany("GioHangs")
                        .HasForeignKey("NguoiDungID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WEB.Models.SanPham", "SanPham")
                        .WithMany("GioHangs")
                        .HasForeignKey("SanPhamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KichThuocSanPham");

                    b.Navigation("NguoiDung");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("WEB.Models.KichThuocSanPham", b =>
                {
                    b.HasOne("WEB.Models.SanPham", "SanPham")
                        .WithMany("KichThuocSanPhams")
                        .HasForeignKey("SanPhamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("WEB.Models.SanPham", b =>
                {
                    b.HasOne("WEB.Models.DanhMucSanPham", "DanhMucSanPham")
                        .WithMany("SanPhams")
                        .HasForeignKey("DanhMucID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DanhMucSanPham");
                });

            modelBuilder.Entity("WEB.Models.ThongBao", b =>
                {
                    b.HasOne("WEB.Models.NguoiDung", "NguoiDung")
                        .WithMany("ThongBaos")
                        .HasForeignKey("NguoiDungID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("WEB.Models.DanhMucSanPham", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("WEB.Models.DonHang", b =>
                {
                    b.Navigation("ChiTietDonHangs");
                });

            modelBuilder.Entity("WEB.Models.NguoiDung", b =>
                {
                    b.Navigation("DanhGiaSanPhams");

                    b.Navigation("DonHangs");

                    b.Navigation("GioHangs");

                    b.Navigation("ThongBaos");
                });

            modelBuilder.Entity("WEB.Models.SanPham", b =>
                {
                    b.Navigation("ChiTietDonHangs");

                    b.Navigation("DanhGiaSanPhams");

                    b.Navigation("GioHangs");

                    b.Navigation("KichThuocSanPhams");
                });
#pragma warning restore 612, 618
        }
    }
}
