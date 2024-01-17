using System;
using System.Collections.Generic;

namespace vityEmployeeInfoSystem.Models;

public partial class SalaryTable
{
    public int SalaryId { get; set; }

    public int? EmployeeId { get; set; }

    public decimal? ReleasedAmount { get; set; }

    public DateOnly? ReleasedDate { get; set; }

    public string? ReleasedForMonth { get; set; }

    public virtual EmployeeTable? Employee { get; set; }
}
