using System;
using System.Collections.Generic;

namespace vityEmployeeInfoSystem.Models;

public partial class EmployeeTable
{
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public int? DesignationId { get; set; }

    public int? DepartmentId { get; set; }

    public string? Address { get; set; }

    public string? Contact { get; set; }

    public string? Email { get; set; }

    public DateOnly? Dob { get; set; }

    public decimal? Salary { get; set; }

    public virtual DepartmentTable? Department { get; set; }

    public virtual DesignationTable? Designation { get; set; }

    public virtual ICollection<LeaveTable> LeaveTables { get; set; } = new List<LeaveTable>();

    public virtual ICollection<SalaryTable> SalaryTables { get; set; } = new List<SalaryTable>();
}
