using System;
using System.Collections.Generic;

namespace Project7MAR2023.PGModels;

public partial class VEmployeePosition
{
    public string? Employeeid { get; set; }

    public string? Fullname { get; set; }

    public DateOnly? Birthdate { get; set; }

    public string? Address { get; set; }

    public string? Postitle { get; set; }
}
