using System.Text;
using GuitarManagement.BL.Domain;

namespace GuitarManagement.UI.CA.Extensions;

public static class StoreExtensions
{
    public static string GetStringRepresentationWithGuitarModel(this Store store)
    {
        var sb = new StringBuilder();
        sb.Append($"{store.Name} {store.Address} {store.Location}");
        foreach (var stock in store.Stock)
        {
            sb.Append($"\n \tGUITAR: {stock.GuitarModel.Name}");
        }

        return sb.ToString();
    }
    public static string GetSimpleStringRepresentation(this Store store)
    {
        var sb = new StringBuilder();
        sb.Append($"{store.Name} {store.Address} {store.Location}");
        return sb.ToString();
    }
}