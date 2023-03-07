using System;
using System.Collections.Generic;

namespace Project7MAR2023.Datawarehouse;

public partial class EmployeeTrainingHistory
{
    public int Id { get; set; }

    public string Employeeid { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public DateOnly Birthdate { get; set; }

    public string? Address { get; set; }

    public string? Postitle { get; set; }

    public string? Traningtype { get; set; }

    public DateOnly? Completeddate { get; set; }
}
