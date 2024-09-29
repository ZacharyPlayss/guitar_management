using System;
using System.Security.Claims;
using GuitarManagement.BL;
using GuitarManagement.BL.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using UI_MVC.Models.Dto;

namespace GuitarManagement.UI.MVC.Controllers;

public class GuitarController : Controller
{
    private readonly IManager _manager;
    private readonly UserManager<IdentityUser> _userManager;


    public GuitarController(IManager manager, UserManager<IdentityUser> userManager)
    {
        _manager = manager;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Details(long id)
    {
        var guitarModel = _manager.GetGuitarModelWithStoresAndMaintainer(id);
        return View(guitarModel);
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var allGuitars = _manager.GetAllGuitarModels();
        return View(allGuitars);
    }
    
    [HttpGet]
    [Authorize]
    public IActionResult Add()
    {
        return View();
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult Add(GuitarModelDto gm)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        var userId = _userManager.GetUserId(User);
        _manager.AddGuitarModelWithMaintainer(gm.Name,gm.AmountOfStrings,gm.Price,gm.Type, userId);
        return RedirectToAction("Index", "Guitar");
    }
    
}