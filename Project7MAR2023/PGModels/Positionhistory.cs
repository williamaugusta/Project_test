using System;
using System.Collections.Generic;

namespace Project7MAR2023.PGModels;

public partial class Positionhistory
{
    public int Id { get; set; }

    public string Posid { get; set; } = null!;

    public string Postitle { get; set; } = null!;

    public string Employeeid { get; set; } = null!;

    public DateOnly Startdate { get; set; }

    public DateOnly Enddate { get; set; }
}
