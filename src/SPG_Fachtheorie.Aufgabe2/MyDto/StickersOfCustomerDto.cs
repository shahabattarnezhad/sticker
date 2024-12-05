using System;
using System.Collections.Generic;

namespace SPG_Fachtheorie.Aufgabe2.MyDto;

public class StickersOfCustomerDto
{
    public string? FullName { get; set; }

    public string? Email { get; set; }

    public List<StickerDetailsDto>? StickerDetails { get; set; }
}
