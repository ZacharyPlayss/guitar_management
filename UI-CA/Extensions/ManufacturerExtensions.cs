using GuitarManagement.BL.Domain;

namespace GuitarManagement.UI.CA.Extensions;

public static class ManufacturerExtensions
{
    public static string GetStringRepresentation(this Manufacturer manufacturer)
    {
        return $"{manufacturer.Name} {manufacturer.Location} {manufacturer.DateFounded}";
    }
}