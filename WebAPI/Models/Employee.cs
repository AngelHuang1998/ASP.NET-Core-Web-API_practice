using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string Name { get; set; } = null!;
}
