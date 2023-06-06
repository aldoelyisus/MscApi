using System;
using System.Collections.Generic;

namespace MscApi.Domain.Entities;

public partial class CategoryType
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public string Label { get; set; } = null!;

    public uint ParentId { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
