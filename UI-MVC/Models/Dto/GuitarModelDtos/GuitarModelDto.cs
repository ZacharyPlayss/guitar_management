using System.ComponentModel.DataAnnotations;
using GuitarManagement.BL.Domain;

namespace UI_MVC.Models.Dto;

public class GuitarModelDto
{
    [Required]
    public string Name { get; set; }
    [Range(1,50)]
    public int AmountOfStrings { get; set; }
    public double Price { get; set; }
    [Required]
    public GuitarType Type { get; set; }
}