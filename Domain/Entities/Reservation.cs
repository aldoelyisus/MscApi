using System;
using System.Collections.Generic;

namespace MscApi.Domain.Entities;

public partial class Reservation
{
    public uint Id { get; set; }

    public uint StatusHistory { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ReservationStatus StatusHistoryNavigation { get; set; } = null!;
}
