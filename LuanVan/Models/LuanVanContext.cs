using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LuanVan.Models;

public partial class LuanVanContext : DbContext
{
    public LuanVanContext()
    {
    }

    public LuanVanContext(DbContextOptions<LuanVanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietHd> ChiTietHds { get; set; }

    public virtual DbSet<GioHang> GioHangs { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }

    public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }

    public virtual DbSet<MauSac> MauSacs { get; set; }

    public virtual DbSet<NhaSanXuat> NhaSanXuats { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<Quyen> Quyens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-VCL1NL6;Initial Catalog=LuanVan;TrustServerCertificate=True; Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietHd>(entity =>
        {
            entity.HasKey(e => e.MaChiTietHd).HasName("PK__ChiTietH__651E49EBA37A5A57");

            entity.ToTable("ChiTietHD");

            entity.Property(e => e.MaChiTietHd)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MaChiTietHD");
            entity.Property(e => e.MaGioHang)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MaHoaDon)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.MaGioHangNavigation).WithMany(p => p.ChiTietHds)
                .HasForeignKey(d => d.MaGioHang)
                .HasConstraintName("FK__ChiTietHD__MaGio__59063A47");

            entity.HasOne(d => d.MaHoaDonNavigation).WithMany(p => p.ChiTietHds)
                .HasForeignKey(d => d.MaHoaDon)
                .HasConstraintName("FK__ChiTietHD__MaHoa__5812160E");
        });

        modelBuilder.Entity<GioHang>(entity =>
        {
            entity.HasKey(e => e.MaGioHang).HasName("PK__GioHang__F5001DA3BF44EDAB");

            entity.ToTable("GioHang");

            entity.Property(e => e.MaGioHang)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MaSanPham)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.GioHangs)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK__GioHang__MaKhach__5441852A");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.GioHangs)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__GioHang__MaSanPh__5535A963");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHoaDon).HasName("PK__HoaDon__835ED13B534E3A97");

            entity.ToTable("HoaDon");

            entity.Property(e => e.MaHoaDon)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MaKm)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MaKM");
            entity.Property(e => e.NgayXuatHd)
                .HasColumnType("date")
                .HasColumnName("NgayXuatHD");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK__HoaDon__MaKhachH__4BAC3F29");

            entity.HasOne(d => d.MaKmNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaKm)
                .HasConstraintName("FK__HoaDon__MaKM__4CA06362");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKhachHang).HasName("PK__KhachHan__88D2F0E5147936CB");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.GioiTinh).HasMaxLength(3);
            entity.Property(e => e.HoKhachHang).HasMaxLength(30);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenKhachHang).HasMaxLength(50);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MaKm).HasName("PK__KhuyenMa__2725CF15C7F51782");

            entity.ToTable("KhuyenMai");

            entity.Property(e => e.MaKm)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MaKM");
            entity.Property(e => e.GiaTriKm).HasColumnName("GiaTriKM");
            entity.Property(e => e.NgayBatDau).HasColumnType("date");
            entity.Property(e => e.NgayKetThuc).HasColumnType("date");
            entity.Property(e => e.TenKhuyenMai).HasMaxLength(200);
        });

        modelBuilder.Entity<LoaiSanPham>(entity =>
        {
            entity.HasKey(e => e.MaLoaiSp).HasName("PK__LoaiSanP__1224CA7C28C28A8E");

            entity.ToTable("LoaiSanPham");

            entity.Property(e => e.MaLoaiSp)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MaLoaiSP");
            entity.Property(e => e.TenLoaiSp)
                .HasMaxLength(40)
                .HasColumnName("TenLoaiSP");
        });

        modelBuilder.Entity<MauSac>(entity =>
        {
            entity.HasKey(e => e.MaMau).HasName("PK__MauSac__3A5BBB7DBBB4C681");

            entity.ToTable("MauSac");

            entity.Property(e => e.MaMau)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenMau).HasMaxLength(40);
        });

        modelBuilder.Entity<NhaSanXuat>(entity =>
        {
            entity.HasKey(e => e.MaNsx).HasName("PK__NhaSanXu__3A1BDBD2F4E7F01A");

            entity.ToTable("NhaSanXuat");

            entity.Property(e => e.MaNsx)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MaNSX");
            entity.Property(e => e.TenNsx)
                .HasMaxLength(40)
                .HasColumnName("TenNSX");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien).HasName("PK__NhanVien__77B2CA4783B18CF0");

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNhanVien)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.GioiTinh).HasMaxLength(3);
            entity.Property(e => e.HoNhanVien).HasMaxLength(30);
            entity.Property(e => e.MaRole)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenNhanVien).HasMaxLength(50);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.MaRoleNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.MaRole)
                .HasConstraintName("FK__NhanVien__MaRole__398D8EEE");
        });

        modelBuilder.Entity<Quyen>(entity =>
        {
            entity.HasKey(e => e.MaQuyen).HasName("PK__Quyen__1D4B7ED4C1BC3E41");

            entity.ToTable("Quyen");

            entity.Property(e => e.MaQuyen)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenQuyen).HasMaxLength(40);

            entity.HasMany(d => d.MaRoles).WithMany(p => p.MaQuyens)
                .UsingEntity<Dictionary<string, object>>(
                    "QuyenRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("MaRole")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Quyen_Rol__MaRol__3F466844"),
                    l => l.HasOne<Quyen>().WithMany()
                        .HasForeignKey("MaQuyen")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Quyen_Rol__MaQuy__3E52440B"),
                    j =>
                    {
                        j.HasKey("MaQuyen", "MaRole").HasName("PK_NQ");
                        j.ToTable("Quyen_Role");
                    });
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.MaRole).HasName("PK__Role__0639A0FDE6DE0CDA");

            entity.ToTable("Role");

            entity.Property(e => e.MaRole)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenRole).HasMaxLength(40);
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSanPham).HasName("PK__SanPham__FAC7442DD9127B41");

            entity.ToTable("SanPham");

            entity.Property(e => e.MaSanPham)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.HinhAnh).HasMaxLength(200);
            entity.Property(e => e.MaLoaiSp)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MaLoaiSP");
            entity.Property(e => e.MaMau)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MaNsx)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MaNSX");
            entity.Property(e => e.TenDvt)
                .HasMaxLength(20)
                .HasColumnName("TenDVT");
            entity.Property(e => e.TenSanPham).HasMaxLength(100);

            entity.HasOne(d => d.MaLoaiSpNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaLoaiSp)
                .HasConstraintName("FK__SanPham__MaLoaiS__5070F446");

            entity.HasOne(d => d.MaMauNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaMau)
                .HasConstraintName("FK__SanPham__MaMau__5165187F");

            entity.HasOne(d => d.MaNsxNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaNsx)
                .HasConstraintName("FK__SanPham__MaNSX__4F7CD00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
