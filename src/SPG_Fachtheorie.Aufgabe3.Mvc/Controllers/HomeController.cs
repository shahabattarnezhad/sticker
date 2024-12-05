﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SPG_Fachtheorie.Aufgabe2.Dtos;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe3.Mvc.Models;

namespace SPG_Fachtheorie.Aufgabe3.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        StickerContext db = new StickerContext();
        var data = db.Customers.ToList();

        var dataDto = data.Select(x => 
        new CustomerDto
        (
            x.Id,
            x.Guid,
            x.Firstname,
            x.Lastname,
            x.Email,
            x.RegistrationDate
        )).ToList();

        return View(dataDto);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
