using System;
using System.Collections.Generic;

namespace MscApi.Domain.Entities;

public partial class CatalogueType
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
