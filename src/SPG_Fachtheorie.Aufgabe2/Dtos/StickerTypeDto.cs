using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe2.Dtos
{
    public record StickerTypeDto
    (
        string Name,
        VehicleType VehicleType,
        int DaysValid,
        decimal Price
    );
}
