using System;
using System.Collections.Generic;

namespace Project7MAR2023.PGModels;

public partial class Employee
{
    public int Id { get; set; }

    public string Employeeid { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public DateOnly Birthdate { get; set; }

    public string? Address { get; set; }
}
