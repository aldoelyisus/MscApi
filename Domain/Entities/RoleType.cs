using System;
using System.Collections.Generic;

namespace MscApi.Domain.Entities;

public partial class RoleType
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public string Label { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
