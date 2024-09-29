using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GuitarManagement.BL.Domain;

public class GuitarModel : IValidatableObject
{
    public long Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Range(1,50)]
    public int AmountOfStrings { get; set; }
    public double Price { get; set; }
    [Required]
    public GuitarType Type { get; set; }
    public Manufacturer Manufacturer { get; set; }
    public ICollection<Stock> Stock { get; set; }
    public IdentityUser ModelOwner { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> result = new List<ValidationResult>();
        if (Price <= 0)
        {
            result.Add(new ValidationResult(
                "A guitars price cannot be negative.", 
                new []{nameof(Price)}));
        }
        return result;
    }
}