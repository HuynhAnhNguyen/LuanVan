using System;
using System.Collections.Generic;

namespace LuanVan.Models;

public partial class GioHang
{
    public string MaGioHang { get; set; } = null!;

    public int SoLuongDat { get; set; }

    public int TrangThai { get; set; }

    public string? MaKhachHang { get; set; }

    public string? MaSanPham { get; set; }

    public virtual ICollection<ChiTietHd> ChiTietHds { get; } = new List<ChiTietHd>();

    public virtual KhachHang? MaKhachHangNavigation { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
