using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuitarManagement.BL.Domain;

public class Manufacturer
{
    public long Id { get; set; }
    public string Name { get; set; }
    public DateTime DateFounded { get; set; }
    public string Location { get; set; }
    public ICollection<GuitarModel> GuitarModels { get; set; }
    
}