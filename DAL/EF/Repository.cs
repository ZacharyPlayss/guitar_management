using GuitarManagement.BL.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GuitarManagement.DAL.EF;

public class Repository : IRepository
{
    private readonly GuitarDbContext _context;
    public Repository(GuitarDbContext context)
    {
        this._context = context;
    }

    public GuitarModel readGuitarModel(long id)
    {
        return _context.GuitarModels.First(g => g.Id == id);
    }

    public GuitarModel readGuitarModelWithMaintainer(long id)
    {
        return _context.GuitarModels
            .Include(g => g.Stock)
            .ThenInclude(s => s.Store)
            .Include(g => g.ModelOwner)
            .First(g => g.Id == id);
    }

    public GuitarModel readGuitarModelWithStores(long id)
    {
        return _context.GuitarModels
            .Include(g => g.Stock)
            .ThenInclude(s => s.Store)
            .First(g => g.Id == id);
    }

    public GuitarModel readGuitarModelWithStoresAndMaintainer(long id)
    {
        return _context.GuitarModels
            .Include(g => g.Stock)
            .ThenInclude(s => s.Store)
            .Include(g => g.ModelOwner)
            .First(g => g.Id == id);
    }
    public IEnumerable<GuitarModel> readAllGuitarModels()
    {
        return _context.GuitarModels;
    }

    public IEnumerable<GuitarModel> readAllGuitarModelsAndManufacturer()
    {
        return _context.GuitarModels.Include(gm => gm.Manufacturer);
    }
    
    public IEnumerable<GuitarModel> readGuitarModelsByType(GuitarType type)
    {
        return _context.GuitarModels.Where(gm => gm.Type == type);
    }

    public IEnumerable<Store> ReadAllStoresWithStockAndGuitarModels()
    {
        return _context.Stores.Include(s => s.Stock)
            .ThenInclude(stock => stock.GuitarModel);
    }

    public Store ReadStoreWithStockAndGuitarModels(long id)
    {
        return _context.Stores
            .Include(s => s.Stock)
            .ThenInclude(stock => stock.GuitarModel)
            .First(s => s.Id == id);
    }
    public void createGuitarModel(GuitarModel guitar)
    {
        _context.GuitarModels.Add(guitar);
        _context.SaveChanges();
    }

    public GuitarModel updateGuitar(GuitarModel guitar)
    {
        var existingGuitar = _context.GuitarModels.Find(guitar.Id);
        if (existingGuitar == null)
        {
            return null;
        }

        existingGuitar.Name = guitar.Name;
        existingGuitar.Manufacturer = guitar.Manufacturer;
        existingGuitar.ModelOwner = guitar.ModelOwner;
        existingGuitar.Stock = guitar.Stock;
        existingGuitar.Price = guitar.Price;
        existingGuitar.AmountOfStrings = guitar.AmountOfStrings;
        existingGuitar.Type = guitar.Type;
        _context.SaveChanges();

        return _context.GuitarModels.Find(guitar.Id);
    }

    public Store ReadStore(long id)
    {
        return _context.Stores.Find(id);
    }

    public IEnumerable<Store> ReadAllStores()
    {
        return _context.Stores;
    }

    public ICollection<Store> ReadStoresByNameAndLocation(string name, string location)
    {
        IQueryable<Store> query = _context.Stores;
        
        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(location))
        {
            // Option 1: Both name and location are provided, filter on both
            query = query.Where(store => store.Name.Contains(name) && store.Location.Contains(location));
        }else if (!string.IsNullOrEmpty(name))
        {
            // Option 2: Only name is provided, filter on name
            query = query.Where(store => store.Name.Contains(name));
        }else if (!string.IsNullOrEmpty(location))
        {
            // Option 3: Only location is provided, filter on location
            query = query.Where(store => store.Location.Contains(location));
        }

        return query.ToList();
    }

    public void CreateStore(Store store)
    {
        _context.Stores.Add(store);    
        _context.SaveChanges();               
    }

    public void CreateStoreGuitarModel(Store store,GuitarModel guitarModel)
    {
        store.Stock.Add(new Stock()
        {
            Store = store,
            GuitarModel = guitarModel,
            Amount = null
        });
    }
    
    public void DeleteStoreGuitarModel(long guitarModelId, long storeId)
    {
        var store = _context.Stores.Include(s => s.Stock)
            .ThenInclude(stock => stock.GuitarModel)
            .Single(s => s.Id == storeId);
        var stockToRemove = store.Stock.Single(s => s.GuitarModel.Id == guitarModelId);
        
        store.Stock.Remove(stockToRemove);
    }

    public IEnumerable<GuitarModel> ReadGuitarModelsNotInStore(long storeId)
    {
        var store = _context.Stores.Include(s => s.Stock)
            .ThenInclude(stock => stock.GuitarModel)
            .Single(s => s.Id == storeId);
        if (store != null)
        {
            var guitarModelsInStock = store.Stock.Select(s => s.GuitarModel.Id);
            return _context.GuitarModels
                .Where(g => !guitarModelsInStock.Contains(g.Id));
        }
        return Enumerable.Empty<GuitarModel>();
    }
    public IEnumerable<GuitarModel> ReadGuitarModelsInStore(long storeId)
    {
        var store = _context.Stores.Include(s => s.Stock)
            .ThenInclude(stock => stock.GuitarModel)
            .Single(s => s.Id == storeId);
        if (store != null)
        {
            var guitarModelsInStock = store.Stock.Select(s => s.GuitarModel.Id);
            return _context.GuitarModels
                .Where(g => guitarModelsInStock.Contains(g.Id));
        }
        return Enumerable.Empty<GuitarModel>();
    }

    public void CreateStockElement(Stock stock)
    {

        _context.Stocks.Add(stock);
        _context.SaveChanges();
    }

    public IEnumerable<Manufacturer> ReadManufacturers()
    {
        return _context.Manufacturers;
    }

    public Manufacturer ReadManufacturer(long id)
    {
        return _context.Manufacturers.Find(id);
    }

    public void CreateManufacturer(Manufacturer manufacturer)
    {
        _context.Manufacturers.Add(manufacturer);
        _context.SaveChanges();
    }
    
    //USER
    public IdentityUser ReadUser(string userId)
    {
        return _context.Users.Find(userId);
    }
    public IdentityUser ReadUserByMail(string mail)
    {
            return _context.Users.First(u => u.Email == mail);
    }
    public IdentityRole ReadUserRole(string userId)
    {
        var user = _context.Users.Find(userId);
        if (user != null)
        {
            var userRole = _context.UserRoles.FirstOrDefault(ur => ur.UserId == user.Id);
            if (userRole != null)
            {
                var role = _context.Roles.Find(userRole.RoleId);
                return role;
            }
        }

        return null;
    }
}