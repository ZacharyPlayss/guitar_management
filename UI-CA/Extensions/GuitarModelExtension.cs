using System.Text;
using GuitarManagement.BL.Domain;

namespace GuitarManagement.UI.CA.Extensions;

public static class GuitarModelExtension
{
    public static string GetStringRepresentation(this GuitarModel guitarModel)
    {
        var sb = new StringBuilder();
        sb.Append($"{guitarModel.Name} {guitarModel.Price:C} [Manufactured by {guitarModel.Manufacturer.Name}]");
        return sb.ToString();
    }
}