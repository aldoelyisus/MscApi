using System;
using System.Collections.Generic;

namespace MscApi.Domain.Entities;

public partial class Product
{
    public uint Id { get; set; }

    public string Category { get; set; } = null!;

    public string Catalogue { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Description { get; set; } = null!;

    public uint Stock { get; set; }

    public uint Price { get; set; }

    public uint Cost { get; set; }

    public uint Discount { get; set; }

    public bool Featured { get; set; }

    public string ImgPath { get; set; } = null!;

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual CatalogueType CatalogueNavigation { get; set; } = null!;

    public virtual CategoryType CategoryNavigation { get; set; } = null!;
}
