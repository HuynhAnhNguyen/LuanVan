using System;
using System.Collections.Generic;

namespace LuanVan.Models;

public partial class Role
{
    public string MaRole { get; set; } = null!;

    public string TenRole { get; set; } = null!;

    public virtual ICollection<NhanVien> NhanViens { get; } = new List<NhanVien>();

    public virtual ICollection<Quyen> MaQuyens { get; } = new List<Quyen>();
}
