using GuitarManagement.BL.Domain;
using Microsoft.AspNetCore.Identity;

namespace GuitarManagement.DAL;

public interface IRepository
{
    //GUITARMODEL
    public GuitarModel readGuitarModel(long id);
    public GuitarModel readGuitarModelWithMaintainer(long id);
    public GuitarModel readGuitarModelWithStores(long id);
    public GuitarModel readGuitarModelWithStoresAndMaintainer(long id);
    public IEnumerable<GuitarModel> readAllGuitarModels();
    public IEnumerable<GuitarModel> readAllGuitarModelsAndManufacturer();
    public IEnumerable<GuitarModel> readGuitarModelsByType(GuitarType type);
    public void createGuitarModel(GuitarModel guitar);

    public GuitarModel updateGuitar(GuitarModel guitar);
    ////STORE
    public Store ReadStore(long id);
    public IEnumerable<Store> ReadAllStores();
    public IEnumerable<Store> ReadAllStoresWithStockAndGuitarModels();
    public Store ReadStoreWithStockAndGuitarModels(long id);
    public ICollection<Store> ReadStoresByNameAndLocation(string name, string location);
    public void CreateStore(Store store);
    public void CreateStoreGuitarModel(Store store,GuitarModel guitarModel);
    public void DeleteStoreGuitarModel(long guitarModelId, long storeId);
    IEnumerable<GuitarModel> ReadGuitarModelsNotInStore(long storeId);
    IEnumerable<GuitarModel> ReadGuitarModelsInStore(long storeId);
    
    //STOCK ELEMENT
    public void CreateStockElement(Stock stock);
    //MANUFACTURER
    public IEnumerable<Manufacturer> ReadManufacturers();
    public Manufacturer ReadManufacturer(long id);
    public void CreateManufacturer(Manufacturer manufacturer);
    //USER
    public IdentityUser ReadUser(string userId);
    public IdentityUser ReadUserByMail(string mail);
    public IdentityRole ReadUserRole(string userId);
}