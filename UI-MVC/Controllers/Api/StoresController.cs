using System.Collections.Generic;
using GuitarManagement.BL;
using GuitarManagement.BL.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UI_MVC.Models.Dto;

namespace UI_MVC.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class StoresController : ControllerBase
{
    private readonly IManager _manager;

    public StoresController(IManager manager)
    {
        _manager = manager;
    }
    
    [HttpGet]
    public IEnumerable<Store> getAllStores()
    {
        return _manager.GetAllStores();
    }

    [HttpGet("store/{id}")]
    public Store getStoreWithStock(long id)
    {
        return _manager.GetStoreWithStockAndGuitarModels(id);
    }
    [HttpGet("guitarmodel/{id}")]
    public IEnumerable<GuitarModel> GetGuitarmodelsNotInStore(long id)
    {
        return _manager.GetGuitarModelsNotInStore(id);
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult AddStock(StockDto stockDto)
    { 
        var createdStock =
            _manager.AddStock(stockDto.StoreId, stockDto.GuitarModelId, stockDto.Amount);
        return CreatedAtAction("getStoreWithStock", new { id = stockDto.Id },
            new StockDto()
            {
                Id = createdStock.Id,
                StoreId = createdStock.Store.Id,
                GuitarModelId = createdStock.GuitarModel.Id,
                Amount = (int)createdStock.Amount
            });
    }
    
}