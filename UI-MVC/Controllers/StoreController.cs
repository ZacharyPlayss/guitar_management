using GuitarManagement.BL;
using GuitarManagement.BL.Domain;
using Microsoft.AspNetCore.Mvc;

namespace UI_MVC.Controllers;

public class StoreController : Controller
{
    private readonly IManager _manager;

    public StoreController(IManager manager)
    {
        _manager = manager;
    }

    public IActionResult Details(int id)
    {
        var author = _manager.GetStoreWithStockAndGuitarModels(id);
        return View(author);
    }
}