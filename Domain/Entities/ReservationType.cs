using System;
using System.Collections.Generic;

namespace MscApi.Domain.Entities;

public partial class ReservationType
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public string Label { get; set; } = null!;

    public virtual ICollection<ReservationStatus> ReservationStatuses { get; set; } = new List<ReservationStatus>();
}
