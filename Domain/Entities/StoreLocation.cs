using System;
using System.Collections.Generic;

namespace MscApi.Domain.Entities;

public partial class StoreLocation
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public string FacebookUser { get; set; } = null!;

    public string TwitterUser { get; set; } = null!;

    public string WhatsAppPhone { get; set; } = null!;
}
