using System;
using System.Collections.Generic;

namespace LoginAndVegitable.Models;

public partial class Price
{
    public int Id { get; set; }

    public int? Price1 { get; set; }

    public int? VegetableId { get; set; }

    public int? UserId { get; set; }

    public virtual VegList? Vegetable { get; set; }

    public virtual VegNamesForlist? VegetableNavigation { get; set; }
}
