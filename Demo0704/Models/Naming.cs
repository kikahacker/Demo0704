using System;
using System.Collections.Generic;

namespace Demo0704.Models;

public partial class Naming
{
    public int NamingId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Concelarium> Concelaria { get; set; } = new List<Concelarium>();
}
