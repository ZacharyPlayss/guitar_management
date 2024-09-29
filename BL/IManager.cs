using System.Collections;
using GuitarManagement.BL.Domain;
using Microsoft.AspNetCore.Identity;

namespace GuitarManagement.BL;

public interface IManager
{
    //GUITARMODEL
    public GuitarModel GetGuitarModel(long id);
    public GuitarModel GetGuitarModelWithMaintainer(long id);
    public GuitarModel GetGuitarModelWithStoresAndMaintainer(long id);
    public IEnumerable<GuitarModel> GetAllGuitarModels();
    public IEnumerable<GuitarModel> GetAllGuitarModelsByType(GuitarType type);
    public IEnumerable<GuitarModel> GetAllGuitarModelsAndManufacturer();
    public GuitarModel AddGuitarModel(string name, int stringAmount,double price, GuitarType type);
    public GuitarModel AddGuitarModelWithMaintainer(string name, int stringAmount,double price, GuitarType type, string userId);

    public GuitarModel UpdateGuitarPrice(long guitarModelId, string userId,double price);
    //STORE
    public void AddStoreGuitarModel(Store store,GuitarModel guitarModel);
    public void RemoveStoreGuitarModel(long guitarModelId, long storeId);
    public Store GetStore(long id);
    public IEnumerable<Store> GetAllStores();
    public IEnumerable<Store> GetAllStoresWithStockAndGuitarModels();
    public Store GetStoreWithStockAndGuitarModels(long id);
    public ICollection<Store> GetStoresByNameAndLocation(string name, string location);
    public Store AddStore(string name, string address, string location);
    IEnumerable<GuitarModel> GetGuitarModelsNotInStore(long storeId);
    IEnumerable<GuitarModel> GetGuitarModelsInStore(long storeId);
    //STOCK
    public Stock AddStock(long storeId, long guitarModelId,int amount);
    //MANUFACTURER
    public IEnumerable<Manufacturer> GetManufacturers();
    public Manufacturer GetManufacturer(long id);
    public Manufacturer AddManufacturer(string name, DateTime dateFonded, string location);
    public IdentityUser ReadUserByMail(string mail);
} 