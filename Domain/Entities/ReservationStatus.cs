using System;
using System.Collections.Generic;

namespace MscApi.Domain.Entities;

public partial class ReservationStatus
{
    public uint Id { get; set; }

    public string Code { get; set; } = null!;

    public DateTime StatusDate { get; set; }

    public virtual ReservationType CodeNavigation { get; set; } = null!;

    public virtual Reservation? Reservation { get; set; }
}
