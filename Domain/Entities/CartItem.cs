using System;
using System.Collections.Generic;

namespace MscApi.Domain.Entities;

public partial class CartItem
{
    public uint Id { get; set; }

    public uint Product { get; set; }

    public uint Reservation { get; set; }

    public uint Quantity { get; set; }

    public virtual Product ProductNavigation { get; set; } = null!;

    public virtual Reservation ReservationNavigation { get; set; } = null!;
}
