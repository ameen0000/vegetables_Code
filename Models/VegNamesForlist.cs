using System;
using System.Collections.Generic;

namespace LoginAndVegitable.Models;

public partial class VegNamesForlist
{
    public int Id { get; set; }

    public string? VegetableName { get; set; }

    public virtual ICollection<Price> Prices { get; set; } = new List<Price>();
}
