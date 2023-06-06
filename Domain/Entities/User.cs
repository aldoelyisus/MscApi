using System;
using System.Collections.Generic;

namespace MscApi.Domain.Entities;

public partial class User
{
    public uint Id { get; set; }

    public string Role { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string FirstSurname { get; set; } = null!;

    public string LastSurname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime RegistrationDate { get; set; }

    public virtual RoleType RoleNavigation { get; set; } = null!;
}
