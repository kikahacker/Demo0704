using System;
using System.Collections.Generic;

namespace Demo0704.Models;

public partial class Concelarium
{
    public string Article { get; set; } = null!;

    public int? NamingId { get; set; }

    public int? UnitId { get; set; }

    public double? Price { get; set; }

    public double? MaxDiscount { get; set; }

    public int? ManufacturerId { get; set; }

    public int? PostavshikId { get; set; }

    public int? CategoryId { get; set; }

    public double? CurrentDiscount { get; set; }

    public int? QuantityInStorage { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Manufacturer? Manufacturer { get; set; }

    public virtual Naming? Naming { get; set; }

    public virtual Postavshiki? Postavshik { get; set; }

    public virtual Unit? Unit { get; set; }
}
