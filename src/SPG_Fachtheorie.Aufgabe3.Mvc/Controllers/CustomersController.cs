using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Dtos;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Model;
using SPG_Fachtheorie.Aufgabe2.MyDto;

namespace SPG_Fachtheorie.Aufgabe3.Mvc.Controllers
{
    public class CustomersController : Controller
    {
        private readonly StickerContext _db;

        public CustomersController(StickerContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            // fetch
            List<Customer> data = _db.Customers
                                       .Include(c => c.Stickers)
                                       .Include(c => c.Vehicles)
                                       .ToList();

            // Maping
            var dataDto = data.Select(x => new CustomerToDisplayDto 
            { 
                Id = x.Id,
                Guid = x.Guid,
                FirstName = x.Firstname,
                LastName = x.Lastname,
                NumberOfStickers = x.Stickers.Count,
                NumberOfVehicles = x.Vehicles.Count,
            }).ToList();

            // Data Transfer Object -> DTO

            //var data = _db
            //    .Customers
            //    .Include(c => c.Stickers)
            //     .ThenInclude(c => c.StickerType)
            //    .Include(c => c.Vehicles)
            //    .ToList();

            //var dataDto = 
            //    data.Select(x => new CustomerToDisplayDto
            //    {
            //        FirstName = x.Firstname,
            //        LastName = x.Lastname,
            //        Id = x.Id,
            //        NumberOfVehicles = x.Vehicles.Count,
            //        NumberOfStickers = x.Stickers.Count,
            //    })
            //        .ToList();

            return View(dataDto);
        }
    }
}
