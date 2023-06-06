using System;
using System.Collections.Generic;

namespace MscApi.Domain.Entities;

public partial class CompanyInformation
{
    public string Description { get; set; } = null!;

    public string Mission { get; set; } = null!;

    public string Vision { get; set; } = null!;

    public string LogoPath { get; set; } = null!;
}
