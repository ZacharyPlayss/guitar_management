using System.Collections;
using System.Collections.Generic;
using GuitarManagement.BL;
using GuitarManagement.BL.Domain;
using GuitarManagement.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UI_MVC.Models.Dto;

namespace UI_MVC.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class ManufacturersController : ControllerBase
{
    private readonly IManager _manager;

    public ManufacturersController(IManager manager)
    {
        _manager = manager;
    }
    
    [HttpGet]
    public IEnumerable<Manufacturer> GetAllManufacturers()
    {
        return _manager.GetManufacturers();
    }

    [HttpGet("{id}")]
    public Manufacturer GetOneManfucturer(long id)
    {
        return _manager.GetManufacturer(id);
    }

    [HttpPost]
    [Authorize]
    public IActionResult AddManufacturer(ManufacturerDto ManufacturerDto)
    { 
        var createdManufacturer =
            _manager.AddManufacturer(ManufacturerDto.Name, ManufacturerDto.DateFounded, ManufacturerDto.Location);
        return CreatedAtAction("GetOneManfucturer", new { id = createdManufacturer.Id },
            new ManufacturerDto
            {
                Name = createdManufacturer.Name,
                DateFounded = createdManufacturer.DateFounded,
                Location = createdManufacturer.Location
            });
    }
      
}