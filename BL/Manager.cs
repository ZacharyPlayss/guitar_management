using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using System.Text;
using GuitarManagement.BL.Domain;
using GuitarManagement.DAL;
using Microsoft.AspNetCore.Identity;

namespace GuitarManagement.BL;

public class Manager : IManager
{
    private readonly IRepository _repository;

    public Manager(IRepository repository)
    {
        _repository = repository;
    }

    public GuitarModel GetGuitarModel(long id)
    {
        return _repository.readGuitarModel(id);
    }

    public GuitarModel GetGuitarModelWithMaintainer(long id)
    {
        return _repository.readGuitarModelWithMaintainer(id);
    }

    public GuitarModel GetGuitarModelWithStores(long id)
    {
        return _repository.readGuitarModelWithStores(id);
    }

    public GuitarModel GetGuitarModelWithStoresAndMaintainer(long id)
    {
        return _repository.readGuitarModelWithStoresAndMaintainer(id);
    }

    public IEnumerable<GuitarModel> GetAllGuitarModels()
    {
        return _repository.readAllGuitarModels();
    }

    public IEnumerable<GuitarModel> GetAllGuitarModelsAndManufacturer()
    {
        return _repository.readAllGuitarModelsAndManufacturer();
    }
    
    public IEnumerable<Store> GetAllStoresWithStockAndGuitarModels()
    {
        return _repository.ReadAllStoresWithStockAndGuitarModels();
    }

    public Store GetStoreWithStockAndGuitarModels(long id)
    {
        return _repository.ReadStoreWithStockAndGuitarModels(id);
    }

    public IEnumerable<GuitarModel> GetAllGuitarModelsByType(GuitarType type)
    {
        return _repository.readGuitarModelsByType(type);
    }
    
    public GuitarModel AddGuitarModel(string name, int stringAmount,double price, GuitarType type)
    {
        GuitarModel guitar = new GuitarModel()
        {
            Name = name,
            AmountOfStrings = stringAmount,
            Price = price,
            Type = type,
            Manufacturer = new Manufacturer(),
            Stock = new List<Stock>(),
            ModelOwner = null
        };
        ValidationContext vc = new ValidationContext(guitar);
        List<ValidationResult> result = new List<ValidationResult>();
        bool isOk = Validator.TryValidateObject(guitar, vc, result, validateAllProperties: true);

        if (!isOk)
        {
            string errorMessage = "\nSomething went wrong. Please try again... \n";
            foreach (ValidationResult vr in result)
            {
                errorMessage += vr.ErrorMessage + "\n";
            }
            throw new ValidationException(errorMessage.ToString());
        }
        else
        {
            _repository.createGuitarModel(guitar);
        }

        return guitar;
    }

    public GuitarModel AddGuitarModelWithMaintainer(string name, int stringAmount,double price, GuitarType type, string userId)
    {
        var user = _repository.ReadUser(userId);
        GuitarModel guitar = new GuitarModel()
        {
            Name = name,
            AmountOfStrings = stringAmount,
            Price = price,
            Type = type,
            Manufacturer = new Manufacturer(),
            Stock = new List<Stock>(),
            ModelOwner = user
        };
        ValidationContext vc = new ValidationContext(guitar);
        List<ValidationResult> result = new List<ValidationResult>();
        bool isOk = Validator.TryValidateObject(guitar, vc, result, validateAllProperties: true);

        if (!isOk)
        {
            string errorMessage = "\nSomething went wrong. Please try again... \n";
            foreach (ValidationResult vr in result)
            {
                errorMessage += vr.ErrorMessage + "\n";
            }
            throw new ValidationException(errorMessage.ToString());
        }
        else
        {
            _repository.createGuitarModel(guitar);
        }

        return guitar;
    }

    public GuitarModel UpdateGuitarPrice(long guitarModelId, string userId,double price)
    {
        var user = _repository.ReadUser(userId);
        GuitarModel existingGuitar = _repository.readGuitarModel(guitarModelId);
        var userRole = _repository.ReadUserRole(userId);
        if (existingGuitar.ModelOwner == user || userRole.Name == "admin")
        {
            existingGuitar.Price = price;
            return _repository.updateGuitar(existingGuitar);
        }
        
        throw new AuthenticationException("You are not authenticated to update the guitars price.");
    }

    public Store GetStore(long id)
    {
        return _repository.ReadStore(id);
    }

    public IEnumerable<Store> GetAllStores()
    {
        return _repository.ReadAllStores();
    }

    public ICollection<Store> GetStoresByNameAndLocation(string name, string location)
    {
        return _repository.ReadStoresByNameAndLocation(name, location);
    }
    public Store AddStore(string name, string address, string location)
    {
        Store store = new Store()
        {
            Name = name,
            Address = address,
            Location = location,
            Stock = new List<Stock>()
        };
        ValidationContext vc = new ValidationContext(store);
        List<ValidationResult> result = new List<ValidationResult>();
        bool isOk = Validator.TryValidateObject(store, vc, result, validateAllProperties: true);
        
        if (!isOk)
        {
            StringBuilder errorMessage = new StringBuilder();
            foreach (ValidationResult vr in result)
            {
                errorMessage.Append(vr.ErrorMessage + "\n");
            }

            throw new ValidationException(errorMessage.ToString());
        }else
        {
            _repository.CreateStore(store);
        }
        
        return store;
    }

    public void AddStoreGuitarModel(Store store,GuitarModel guitarModel)
    {
        _repository.CreateStoreGuitarModel(store, guitarModel);
    }

    public void RemoveStoreGuitarModel(long guitarModelId, long storeId)
    {
        _repository.DeleteStoreGuitarModel(guitarModelId, storeId);
    }

    public IEnumerable<GuitarModel> GetGuitarModelsNotInStore(long storeId)
    {
        return _repository.ReadGuitarModelsNotInStore(storeId);
    }
    
    public IEnumerable<GuitarModel> GetGuitarModelsInStore(long storeId)
    {
        return _repository.ReadGuitarModelsInStore(storeId);
    }

    public Stock AddStock(long storeId, long guitarModelId, int amount)
    {
        Store store = _repository.ReadStore(storeId);
        GuitarModel guitarModel = _repository.readGuitarModel(guitarModelId);
        Stock stock = new Stock()
        {
            Amount = amount,
            GuitarModel = guitarModel,
            Store = store
        };
        ValidationContext vc = new ValidationContext(stock);
        List<ValidationResult> result = new List<ValidationResult>();
        bool isOk = Validator.TryValidateObject(stock, vc, result, validateAllProperties: true);
        if (!isOk)
        {
            string errorMessage = "\nSomething went wrong. Please try again... \n";
            foreach (ValidationResult vr in result)
            {
                errorMessage += vr.ErrorMessage + "\n";
            }
            throw new ValidationException(errorMessage.ToString());
        }
        else
        {
            _repository.CreateStockElement(stock);
        }

        return stock;
    }

    public IEnumerable<Manufacturer> GetManufacturers()
    {
        return _repository.ReadManufacturers();
    }

    public Manufacturer GetManufacturer(long id)
    {
        return _repository.ReadManufacturer(id);
    }

    public Manufacturer AddManufacturer(string name, DateTime dateFounded, string location)
    {
        var manufacturer = new Manufacturer
        {
            Name = name,
            DateFounded = dateFounded,
            Location = location
        };
        Validator.ValidateObject(manufacturer, new ValidationContext(manufacturer),true);
        _repository.CreateManufacturer(manufacturer);
        return manufacturer;
    }
    //
    public IdentityUser ReadUserByMail(string mail)
    {
        return _repository.ReadUserByMail(mail);
    }
}