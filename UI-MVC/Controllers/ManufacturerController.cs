using GuitarManagement.BL;
using Microsoft.AspNetCore.Mvc;

namespace UI_MVC.Controllers;

public class ManufacturerController: Controller
{
    private readonly IManager _manager;

    public ManufacturerController(IManager manager)
    {
        _manager = manager;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var allManufacturers = _manager.GetAllGuitarModels();
        return View(allManufacturers);
    }
}