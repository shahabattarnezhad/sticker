using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe2.Dtos
{
	public record CustomerDto
	(
		int Id,
		Guid Guid,
		string Firstname,
		string Lastname,
		string Email,
		DateTime RegistrationDate
		//List<VehicleDto> Vehicles,
		//List<StickerDto> Stickers
	);
}

