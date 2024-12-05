using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe2.Dtos
{
    public record AddStickerCommand
    (
        
        [Required(ErrorMessage = "Vehicle info is required.")]
         string VehicleInfo,
    
        [Required(ErrorMessage = "Sticker type is required.")]
         int StickerTypeId,
    
        [DataType(DataType.Date)]
         [Required(ErrorMessage = "Valid from date is required.")]
         DateTime ValidFrom
    
        //[Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        // decimal Price
    );
}
