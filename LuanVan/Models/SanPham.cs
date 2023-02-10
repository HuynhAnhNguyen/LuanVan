using System;
using System.Collections.Generic;

namespace LuanVan.Models;

public partial class SanPham
{
    public string MaSanPham { get; set; } = null!;

    public string TenSanPham { get; set; } = null!;

    public string TenDvt { get; set; } = null!;

    public string? MaNsx { get; set; }

    public string? MaLoaiSp { get; set; }

    public string? MaMau { get; set; }

    public string HinhAnh { get; set; } = null!;

    public long GiaBan { get; set; }

    public int SoLuongTon { get; set; }

    public virtual ICollection<GioHang> GioHangs { get; } = new List<GioHang>();

    public virtual LoaiSanPham? MaLoaiSpNavigation { get; set; }

    public virtual MauSac? MaMauNavigation { get; set; }

    public virtual NhaSanXuat? MaNsxNavigation { get; set; }
}
