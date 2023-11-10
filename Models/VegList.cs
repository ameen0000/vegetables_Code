using System;
using System.Collections.Generic;

namespace LoginAndVegitable.Models;

public partial class VegList
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Price> Prices { get; set; } = new List<Price>();
}
