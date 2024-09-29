using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuitarManagement.BL.Domain;

public class Store
{
    public long Id { get; set; }
    [Required] 
    [MinLength(5)]
    [MaxLength(25)]
    public string Name { get; set; }
    [StringLength(25)]
    public string Location { get; set; }
    public string Address { get; set; }
    [Required]
    public ICollection<Stock> Stock { get; set; }
    
}