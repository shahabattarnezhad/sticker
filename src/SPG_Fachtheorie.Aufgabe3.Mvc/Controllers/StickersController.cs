using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Dtos;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Model;
using SPG_Fachtheorie.Aufgabe2.MyDto;

namespace SPG_Fachtheorie.Aufgabe3.Mvc.Controllers
{
    public class StickersController : Controller
    {
        private readonly StickerContext _db;

        public StickersController(StickerContext db)
        {
            _db = db;
        }

        public IActionResult Index(Guid customerGuid)
        {
            var result = 
                _db.Stickers
                   .Where(s => s.Customer.Guid == customerGuid)
                   .Include(s => s.StickerType)
                   .Include(s => s.Customer)
                   .ToList();

            var resultDto =
                result
                .GroupBy(s => new { s.Customer.Firstname, s.Customer.Lastname, s.Customer.Email })
                .Select(x => new StickersOfCustomerDto
                {
                    FullName = x.Key.Firstname + " " + x.Key.Lastname,
                    Email = x.Key.Email,
                    StickerDetails = x.Select(sticker => new StickerDetailsDto
                    {
                        Plate = sticker.Numberplate,
                        StickerName = sticker.StickerType.Name,
                        PurchaseDate = sticker.PurchaseDate,
                        ValidFrom = sticker.ValidFrom,
                        ValidTo = sticker.ValidFrom.AddDays(sticker.StickerType.DaysValid),
                        Price = sticker.Price
                    }).ToList()
                }).FirstOrDefault();

            //var resultDto =
            //    result.Select(x => new StickersOfCustomerDto
            //    {
            //        FullName = x.Customer.Firstname + " " + x.Customer.Lastname,
            //        Email = x.Customer.Email,
            //        Plate = x.Numberplate,
            //        StickerName = x.StickerType.Name,
            //        PurchaseDate = x.PurchaseDate,
            //        ValidFrom = x.ValidFrom,
            //        ValidTo = x.ValidFrom.AddDays(x.StickerType.DaysValid),
            //        Price = x.Price,
            //    }).ToList();

            return View(resultDto);
        }

        //public IActionResult Index(Guid customerGuid)
        //{
        //    var customer = _db.Customers
        //        .Include(c => c.Vehicles)
        //        .Include(c => c.Stickers)
        //        .ThenInclude(s => s.StickerType)
        //        .FirstOrDefault(c => c.Guid == customerGuid);

        //    if (customer == null)
        //    {
        //        return RedirectToAction("Index", "Customers");
        //    }

        //    var customerDto = customer.ToDto();

        //    ViewBag.Kunde = $"{customerDto.Firstname} {customerDto.Lastname}";
        //    ViewBag.Email = customerDto.Email;

        //    List<StickerDto> data = _db.Stickers
        //        .Where(s => s.Customer.Guid == customerGuid)
        //        .Include(s => s.Customer)
        //        .Include(s => s.StickerType)
        //        .Select(s => s.ToDto())
        //        .ToList();

        //    return View(data);
        //}

        [HttpGet]
        public IActionResult Add(Guid customerGuid)
        {
            ViewBag.CustomerGuid = customerGuid;

            // Ensure customer exists
            var customer = _db.Customers.FirstOrDefault(c => c.Guid == customerGuid);

            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

            // Fetch the vehicles registered to the customer
            var vehicles = _db.Vehicles
                .Where(v => v.Customer.Guid == customerGuid)
                .ToList(); // Load data into memory to work with VehicleInfo

            // Check if vehicles exist for the customer
            if (!vehicles.Any())
            {
                ModelState.AddModelError("", "No vehicles found for this customer.");
            }

            // Fetch available sticker types
            var stickerTypes = _db.StickerTypes.ToList();

            // Check if sticker types are available
            if (!stickerTypes.Any())
            {
                ModelState.AddModelError("", "No sticker types available.");
            }

            // Populate the dropdowns with the correct data
            ViewBag.Vehicles = new SelectList(vehicles, "Numberplate", "VehicleInfo"); // Use Numberplate for unique ID
            ViewBag.StickerTypes = new SelectList(stickerTypes, "Id", "Name");

            // Return the form with an empty AddStickerCommand
            return View();
        }

        [HttpPost]
        public IActionResult Add(Guid customerGuid, AddStickerCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);


            var vehicles = _db.Vehicles
                .Where(v => v.Customer.Guid == customerGuid)
                .ToList();

            if (!vehicles.Any())
            {
                ModelState.AddModelError("", "No vehicles found for this customer.");
            }

            var stickerTypes = _db.StickerTypes.ToList();

            if (!stickerTypes.Any())
            {
                ModelState.AddModelError("", "No sticker types available.");
            }

            ViewBag.Vehicles = new SelectList(vehicles, "Numberplate", "VehicleInfo"); // Use Numberplate for unique ID
            ViewBag.StickerTypes = new SelectList(stickerTypes, "Id", "Name");

            var customer = _db.Customers
                .Include(c => c.Vehicles)
                .FirstOrDefault(c => c.Guid == customerGuid);

            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

            // Now fetch vehicles in memory (client-side) and check for the selected VehicleInfo
            //var vehicles = _db.Vehicles
            //    .Where(v => v.Customer.Guid == customerGuid)
            //    .AsEnumerable()  // Switch to client-side evaluation
            //    .ToList();
            //var vehicles = _db.Vehicles
            //   .Include(v => v.Customer)
            //   .Where(c => c.Customer.Guid == customerGuid);

            // Ensure the selected vehicle exists by matching VehicleInfo

            var vehicle = vehicles
                .FirstOrDefault(v => v.Numberplate == command.VehicleInfo);



            if (vehicle == null)
            {
                ModelState.AddModelError("", "Selected vehicle not found.");
                return View(command);
            }

            // Ensure the selected sticker type exists
            var stickerType = _db.StickerTypes
                .FirstOrDefault(st => st.Id == command.StickerTypeId);

            if (stickerType == null)
            {
                ModelState.AddModelError("", "Sticker type not found.");
                return View(command);
            }

            // Ensure the vehicle type matches the sticker type
            if (vehicle.VehicleType != stickerType.VehicleType)
            {
                ModelState.AddModelError("", "The selected sticker type does not match the vehicle type.");
                return View(command);
            }

            // Ensure the "ValidFrom" date is not in the past
            if (command.ValidFrom < DateTime.Now.Date)
            {
                ModelState.AddModelError("", "The validity date cannot be in the past.");
                return View(command);
            }

            // Create the new sticker
            var newSticker = new Sticker(
                    vehicle.Numberplate,
                    vehicle.Customer,
                    stickerType,
                    DateTime.Now,
                    command.ValidFrom,
                    stickerType.Price
                );

            // Add and save the sticker
            _db.Stickers.Add(newSticker);
            _db.SaveChanges();

            // Redirect to the customer's stickers overview page
            return RedirectToAction("Index", "Stickers", new { customerGuid });
        }

    }
}

