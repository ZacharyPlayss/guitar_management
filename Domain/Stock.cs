using System.ComponentModel.DataAnnotations;

namespace GuitarManagement.BL.Domain;

public class Stock
{
    public long Id { get; set; }
    [Required] 
    public Store Store { get; set; }
    [Required]
    public GuitarModel GuitarModel { get; set; }
    
    public int? Amount { get; set; }
}