using System;
using System.Collections.Generic;

namespace vityEmployeeInfoSystem.Models;

public partial class UserTable
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;
}
