using System.Collections.Generic;
using System.Security.Claims;
using GuitarManagement.BL;
using GuitarManagement.BL.Domain;
using GuitarManagement.UI.MVC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UI_MVC.Models.Dto;

namespace UI_MVC.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class GuitarsController : ControllerBase
{
    private readonly IManager _manager;
    private readonly UserManager<IdentityUser> _userManager;

    public GuitarsController(IManager manager, UserManager<IdentityUser> userManager)
    {
        _manager = manager;
        _userManager = userManager;
    }
    [HttpGet]
    public IEnumerable<GuitarModel> GetAllGuitars()
    {
        return _manager.GetAllGuitarModels();
    }
    
    [HttpPut("{id}")]
    [Authorize]
    public IActionResult UpdateGuitar(long id, UpdateGuitarModelDto guitarDto)
    {
        string userId = _userManager.GetUserId(User);
        var guitar = _manager.GetGuitarModelWithMaintainer(id);
        if(userId == guitar.ModelOwner.Id || User.IsInRole(CustomIdentityConstants.AdminRole)){
            _manager.UpdateGuitarPrice(id,userId,guitarDto.Price);
            return Ok();
        }
        else
        {
            return Forbid();
        }
    }
}