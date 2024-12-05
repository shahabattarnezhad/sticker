using System;

namespace SPG_Fachtheorie.Aufgabe2.MyDto;

public class CustomerToDisplayDto
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int NumberOfStickers { get; set; }

    public int NumberOfVehicles { get; set; }
}
