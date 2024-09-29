using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GuitarManagement.BL.Domain;

namespace UI_MVC.Models.Dto;

public class ManufacturerDto
{
    public long Id { get; set; }
    [Required]
    public string Name { get; set; }
    public DateTime DateFounded{ get; set; }
    public string Location { get; set; }
}  
