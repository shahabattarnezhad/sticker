﻿using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe2.Dtos
{
    public record StickerDto
    (
        string Numberplate,
        //Customer Customer,
        StickerTypeDto StickerType,
        DateTime PurchaseDate,
        DateTime ValidFrom,
        decimal Price
    );
}
