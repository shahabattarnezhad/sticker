using System;

namespace SPG_Fachtheorie.Aufgabe2.MyDto;

public class StickerDetailsDto
{
    public string? Plate { get; set; }

    public string? StickerName { get; set; }

    public DateTime PurchaseDate { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime ValidTo { get; set; }

    public decimal Price { get; set; }
}
