using GuitarManagement.BL.Domain;
using Microsoft.AspNetCore.Identity;

namespace GuitarManagement.DAL;

public class InMemoryRepository : IRepository, IDisposable
{
    private static readonly ICollection<GuitarModel> _guitars = new List<GuitarModel>();
    private static readonly ICollection<Store> _stores = new List<Store>();

    public static void seed()
    {
        //MANUFACTURER INIT
        Manufacturer manufacturer1 = new Manufacturer()
        {
            Name = "Ibanez",
            DateFounded = new DateTime(1908, 12, 8),
            Location = "Nagoya,Japan",
            GuitarModels = new List<GuitarModel>(),
            Id = 1,
        };
        Manufacturer manufacturer2 = new Manufacturer()
        {
            Name = "Gibson",
            DateFounded = new DateTime(1902, 10, 10),
            Location = "Nashville,USA",
            GuitarModels = new List<GuitarModel>(),
            Id = 2,
        };
        Manufacturer manufacturer3 = new Manufacturer()
        {
            Name = "Fender",
            DateFounded = new DateTime(1946, 01, 01),
            Location = "Fullerton, USA",
            GuitarModels = new List<GuitarModel>(),
            Id = 3,
        };
        //GUITARMODEL INIT
        GuitarModel guitar1 = new GuitarModel()
        {
            Name = "Gio",
            AmountOfStrings = 10,
            Price = 1920.80,
            Type = GuitarType.ELECTRIC,
            Manufacturer = manufacturer1,
            Stock = new List<Stock>(),
            Id = 1,
        };
        GuitarModel guitar2 = new GuitarModel()
        {
            Name = "Stratocaster",
            AmountOfStrings = 6,
            Price = 1999.99,
            Type = GuitarType.ELECTRIC,
            Manufacturer = manufacturer3,
            Stock = new List<Stock>(),
            Id = 2,
        };
        GuitarModel guitar3 = new GuitarModel()
        {
            Name = "Telecaster",
            AmountOfStrings = 6,
            Price = 1949.00,
            Type = GuitarType.ELECTRIC,
            Manufacturer = manufacturer3,
            Stock = new List<Stock>(),
            Id = 3,
        };
        GuitarModel guitar4 = new GuitarModel()
        {
            Name = "SG",
            AmountOfStrings = 6,
            Price = 2500.00,
            Type = GuitarType.ELECTRIC,
            Manufacturer = manufacturer2,
            Stock = new List<Stock>(),
            Id = 4,
        };
        GuitarModel guitar5 = new GuitarModel()
        {
            Name = "Les Paul",
            AmountOfStrings = 6,
            Price = 1499.99,
            Type = GuitarType.ELECTRIC,
            Manufacturer = manufacturer2,
            Stock = new List<Stock>(),
            Id = 5,
        };
        GuitarModel guitar6 = new GuitarModel()
        {
            Name = "TCY10E-BK",
            AmountOfStrings = 6,
            Price = 999.99,
            Type = GuitarType.SEMI,
            Manufacturer = manufacturer1,
            Stock = new List<Stock>(),
            Id = 6,
        };
        GuitarModel guitar7 = new GuitarModel()
        {
            Name = "Hummingbird Standard",
            AmountOfStrings = 6,
            Price = 3600.00,
            Type = GuitarType.ACOUSTIC,
            Manufacturer = manufacturer2,
            Stock = new List<Stock>(),
            Id = 7,
        };
        GuitarModel guitar8 = new GuitarModel()
        {
            Name = "J35",
            AmountOfStrings = 6,
            Price = 2500.00,
            Type = GuitarType.ACOUSTIC,
            Manufacturer = manufacturer2,
            Stock = new List<Stock>(),
            Id = 8
        };
        
        //STORE INIT
        Store store1 = new Store()
        {
            Name = "Bax-Music",
            Address = "Paleisstraat 119",
            Location = "Antwerpen, Belgium",
            Id = 1
        };
        Store store2 = new Store()
        {
            Name = "Musicstore",
            Address = "Online",
            Location = "Online",
            Id = 2
        };
        Store store3 = new Store()
        {
            Name = "Music World",
            Address = "Elewijtsesteenweg 10",
            Location = "Zemst, Belgium",
            Id = 3
        };
        Store store4 = new Store()
        {
            Name = "Music All In",
            Address = "Keyserwey 63",
            Location = "Noordwijk, Netherlands",
            Id = 4
        };
        Store store5 = new Store()
        {
            Name = "Bax-Music",
            Address = "Olympiastraat 4",
            Location = "Goes, Netherlands",
            Id = 5
        };
        //FILLUP STOCK ELEMENTS FOREACH STORE
        fillList(store1.Stock, new List<Stock>()
        {
            new Stock()
            {
                Store = store1,
                GuitarModel = guitar1,
                Amount = 20,
            },
            new Stock()
            {
                Store= store1,
                GuitarModel = guitar3,
                Amount = 2
            },
            new Stock()
            {
                Store= store1,
                GuitarModel = guitar3,
                Amount = 2
            },
            new Stock()
            {
                Store= store1,
                GuitarModel = guitar4,
                Amount = 2
            },new Stock()
            {
                Store= store1,
                GuitarModel = guitar6,
                Amount = 2
            }
        });
        fillList(store2.Stock, new List<Stock>()
        {
            new Stock()
            {
                Store = store2,
                GuitarModel = guitar2,
                Amount = 20,
            },
            new Stock()
            {
                Store = store2,
                GuitarModel = guitar3,
                Amount = 20,
            },
            new Stock()
            {
                Store = store2,
                GuitarModel = guitar4,
                Amount = 20,
            },
            new Stock()
            {
                Store = store2,
                GuitarModel = guitar5,
                Amount = 20,
            },
        });
        fillList(store3.Stock, new List<Stock>()
        {
            new Stock()
            {
                Store = store3,
                GuitarModel = guitar1,
                Amount = 20,
            },
            new Stock()
            {
                Store = store3,
                GuitarModel = guitar2,
                Amount = 20,
            },
            new Stock()
            {
                Store = store3,
                GuitarModel = guitar7,
                Amount = 20,
            },
            new Stock()
            {
                Store = store3,
                GuitarModel = guitar8,
                Amount = 20,
            },
        });
        fillList(store4.Stock, new List<Stock>()
        {
            new Stock()
            {
                Store = store4,
                GuitarModel = guitar3,
                Amount = 20,
            },
            new Stock()
            {
                Store = store4,
                GuitarModel = guitar4,
                Amount = 20,
            },
            new Stock()
            {
                Store = store4,
                GuitarModel = guitar5,
                Amount = 20,
            }
        });
        fillList(store5.Stock, new List<Stock>()
        {
            new Stock()
            {
                Store = store5,
                GuitarModel = guitar1,
                Amount = 20,
            },
            new Stock()
            {
                Store = store5,
                GuitarModel = guitar2,
                Amount = 20,
            },
            new Stock()
            {
                Store = store5,
                GuitarModel = guitar5,
                Amount = 20,
            },
            new Stock()
            {
                Store = store5,
                GuitarModel = guitar6,
                Amount = 20,
            },
            new Stock()
            {
                Store = store5,
                GuitarModel = guitar6,
                Amount = 20,
            }
        });
        //FILL Guitarmodels list of stores.
        /*fillList(store1.GuitarModels,new List<GuitarModel>(){guitar1,guitar3,guitar4,guitar6});
        fillList(store2.GuitarModels,new List<GuitarModel>(){guitar2, guitar3,guitar4, guitar5});
        fillList(store3.GuitarModels,new List<GuitarModel>(){guitar1, guitar2, guitar7,guitar8});
        fillList(store4.GuitarModels,new List<GuitarModel>(){guitar3,guitar4,guitar5});
        fillList(store5.GuitarModels,new List<GuitarModel>(){guitar1,guitar2,guitar5, guitar6, guitar7});
        //FIL STORES LIST OF GUITARS
        fillList(guitar1.Stores,new List<Store>(){store1, store3, store5});
        fillList(guitar2.Stores,new List<Store>(){store2, store3, store5});
        fillList(guitar3.Stores,new List<Store>(){store1, store2,store4});
        fillList(guitar4.Stores,new List<Store>(){store1, store2,store4});
        fillList(guitar5.Stores,new List<Store>(){store2,store4, store5});
        fillList(guitar6.Stores,new List<Store>(){store1, store5});
        fillList(guitar7.Stores,new List<Store>(){store3,store5});
        fillList(guitar8.Stores,new List<Store>(){store3});*/
        //ADD manufacturer to guitarmodels
        fillList(manufacturer1.GuitarModels, new List<GuitarModel>(){guitar1,guitar6});
        fillList(manufacturer2.GuitarModels, new List<GuitarModel>(){guitar4,guitar5,guitar7,guitar8});
        fillList(manufacturer3.GuitarModels, new List<GuitarModel>(){guitar2,guitar3});
        //Fill up list
        fillList(_guitars, new List<GuitarModel>(){guitar1,guitar2,guitar3,guitar4,guitar5,guitar6,guitar7,guitar8});
        fillList(_stores, new List<Store>(){store1,store2,store3,store4,store5});
    }
    private static void fillList<T>(ICollection<T> list, ICollection<T> listOfNewItems)
    {
        if (listOfNewItems.Count != 0)
        {
            foreach (var item in listOfNewItems)
            {
                list.Add(item);
            }
        }
    }

    public GuitarModel readGuitarModel(long id)
    {
        foreach(GuitarModel guitar in _guitars)
        {
            if (guitar.Id == id)
            {
                return guitar;
            }
        }

        return null;
    }

    public GuitarModel readGuitarModelWithMaintainer(long id)
    {
        throw new NotImplementedException();
    }

    public GuitarModel readGuitarModelWithStores(long id)
    {
        throw new NotImplementedException();
    }

    public GuitarModel readGuitarModelWithStoresAndMaintainer(long id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<GuitarModel> readAllGuitarModels()
    {
        return _guitars;
    }

    public IEnumerable<GuitarModel> readAllGuitarModelsAndManufacturer()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<GuitarModel> readGuitarModelsByType(GuitarType type)
    {
        List<GuitarModel> result = new List<GuitarModel>();
        foreach (var guitar in _guitars)
        {
            if (guitar.Type == type)
            {
                result.Add(guitar);
            }
        }
        return result;
    }

    public void createGuitarModel(GuitarModel guitar)
    {
        if (_guitars.Count == 0)
        {
            guitar.Id = 1;
        }
        else
        {
            guitar.Id = _stores.Max(t => t.Id) + 1;
        }
        
        _guitars.Add(guitar);
    }

    public GuitarModel updateGuitar(GuitarModel guitar)
    {
        throw new NotImplementedException();
    }

    public Store ReadStore(long id)
    {
        foreach (Store store in _stores)
        {
            if (store.Id == id)
            {
                return store;
            }
        }

        return null;
    }

    public IEnumerable<Store> ReadAllStores()
    {
        return _stores;
    }

    public IEnumerable<Store> ReadAllStoresWithStockAndGuitarModels()
    {
        throw new NotImplementedException();
    }

    public Store ReadStoreWithStockAndGuitarModels(long id)
    {
        throw new NotImplementedException();
    }

    public ICollection<Store> ReadStoresByNameAndLocation(string name, string location)
    {
        List<Store> result = new List<Store>();
        foreach (Store store in _stores)
        {
            if (name.Length != 0)
            {
                if (location.Length != 0)
                {
                    //NAAM NIET LEEG EN COUNTRY NIET LEEG
                    if (store.Name.ToLower().Contains(name.ToLower())
                        && store.Location.ToLower().Contains(location.ToLower()))
                    {
                        result.Add(store);
                    }
                }
                else
                {
                    //NAAM NIET LEEG MAAR COUNTRY IS WEL LEEG
                    if (store.Name.ToLower().Contains(name.ToLower()))
                    {
                        result.Add(store);
                    }
                }
            }
            else
            {
                if (location.Length != 0)
                {
                    //NAAM LEEG EN COUNTRY NIET LEEG
                    if (store.Location.ToLower().Contains(location.ToLower()))
                    {
                        result.Add(store);
                    }
                }
                else
                {
                    //NAAM LEEG EN COUNTRY LEEG
                    result.Add(store);
                }
            }
        }

        return result;
    }

    public void CreateStore(Store store)
    {
        if (_stores.Count == 0)
        {
            store.Id = 1;
        }
        else
        {
            store.Id = _stores.Max(t => t.Id) + 1;
        }
        
        _stores.Add(store);
    }

    public void CreateStockElement(Stock stock)
    {
        throw new NotImplementedException();
    }

    public void CreateStoreGuitarModel(Store store,GuitarModel guitarModel)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<GuitarModel> ReadAllGuitarModelsAndStores()
    {
        throw new NotImplementedException();
    }

    public void DeleteStoreGuitarModel(long guitarModelId, long storeId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<GuitarModel> ReadGuitarModelsNotInStore(long storeId)
    {
        throw new NotImplementedException();
    }
    
    public IEnumerable<GuitarModel> ReadGuitarModelsInStore(long storeId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Manufacturer> ReadManufacturers()
    {
        throw new NotImplementedException();
    }

    public Manufacturer ReadManufacturer(long id)
    {
        throw new NotImplementedException();
    }

    public void CreateManufacturer(Manufacturer manufacturer)
    {
        throw new NotImplementedException();
    }

    public IdentityUser ReadUser(string userId)
    {
        throw new NotImplementedException();
    }

    public IdentityUser ReadUserByMail(string mail)
    {
        throw new NotImplementedException();
    }

    public IdentityRole ReadUserRole(string userId)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
    }
}