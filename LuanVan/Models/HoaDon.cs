using System;
using System.Collections.Generic;

namespace LuanVan.Models;

public partial class HoaDon
{
    public string MaHoaDon { get; set; } = null!;

    public DateTime NgayXuatHd { get; set; }

    public string? MaKhachHang { get; set; }

    public string? MaKm { get; set; }

    public int TrangThai { get; set; }

    public virtual ICollection<ChiTietHd> ChiTietHds { get; } = new List<ChiTietHd>();

    public virtual KhachHang? MaKhachHangNavigation { get; set; }

    public virtual KhuyenMai? MaKmNavigation { get; set; }
}
