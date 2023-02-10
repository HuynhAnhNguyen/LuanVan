using System;
using System.Collections.Generic;

namespace LuanVan.Models;

public partial class NhanVien
{
    public string MaNhanVien { get; set; } = null!;

    public string HoNhanVien { get; set; } = null!;

    public string TenNhanVien { get; set; } = null!;

    public DateTime NgaySinh { get; set; }

    public string GioiTinh { get; set; } = null!;

    public string SoDienThoai { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public int TrangThai { get; set; }

    public string? MaRole { get; set; }

    public virtual Role? MaRoleNavigation { get; set; }
}
