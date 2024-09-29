using System;
using GuitarManagement.BL.Domain;

namespace UI_MVC.Models.Dto;

public class StockDto
{
    public long Id { get; set; }
    public long StoreId { get; set; }
    public long GuitarModelId { get; set; } 
    public int Amount { get; set; }
}