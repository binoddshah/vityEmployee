using System;
using System.Collections.Generic;

namespace vityEmployeeInfoSystem.Models;

public partial class DesignationTable
{
    public int DesignationId { get; set; }

    public string DesignationName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<EmployeeTable> EmployeeTables { get; set; } = new List<EmployeeTable>();
}
