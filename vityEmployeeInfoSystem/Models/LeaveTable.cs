using System;
using System.Collections.Generic;

namespace vityEmployeeInfoSystem.Models;

public partial class LeaveTable
{
    public int LeaveId { get; set; }

    public int? EmployeeId { get; set; }

    public DateOnly? LeaveDate { get; set; }

    public string? Reason { get; set; }

    public virtual EmployeeTable? Employee { get; set; }
}
