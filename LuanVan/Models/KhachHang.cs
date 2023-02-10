using System;
using System.Collections.Generic;

namespace LuanVan.Models;

public partial class KhachHang
{
    public string MaKhachHang { get; set; } = null!;

    public string HoKhachHang { get; set; } = null!;

    public string TenKhachHang { get; set; } = null!;

    public DateTime NgaySinh { get; set; }

    public string GioiTinh { get; set; } = null!;

    public string SoDienThoai { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public int TrangThai { get; set; }

    public virtual ICollection<GioHang> GioHangs { get; } = new List<GioHang>();

    public virtual ICollection<HoaDon> HoaDons { get; } = new List<HoaDon>();
}
