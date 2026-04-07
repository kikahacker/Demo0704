using System;
using System.Collections.Generic;

namespace Demo0704.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Concelarium> Concelaria { get; set; } = new List<Concelarium>();
}
