using System;
using System.Collections.Generic;

namespace LuanVan.Models;

public partial class Quyen
{
    public string MaQuyen { get; set; } = null!;

    public string TenQuyen { get; set; } = null!;

    public virtual ICollection<Role> MaRoles { get; } = new List<Role>();
}
