using System;
using System.Collections.Generic;

namespace TrainSystem.Data.Models;

public partial class Employee
{
    public int EmployeesId { get; set; }

    public string? Name { get; set; }

    public string? Position { get; set; }

    public int? TrainId { get; set; }

    public virtual Train? Train { get; set; }
}
