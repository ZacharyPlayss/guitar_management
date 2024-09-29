using GuitarManagement.BL.Domain;

namespace GuitarManagement.DAL.EF;

public static class DataSeeder
{
    public static void Seed(GuitarDbContext context)
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
            ModelOwner = context.Users.Single(user => user.UserName =="admin@guitarmanagement.be")
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
            ModelOwner = context.Users.Single(user => user.UserName =="admin@guitarmanagement.be")
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
            ModelOwner = context.Users.Single(user => user.UserName =="admin@guitarmanagement.be")
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
            ModelOwner = context.Users.Single(user => user.UserName =="user@guitarmanagement.be")
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
            ModelOwner = context.Users.Single(user => user.UserName =="user@guitarmanagement.be")
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
            ModelOwner = context.Users.Single(user => user.UserName =="user@guitarmanagement.be")
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
            ModelOwner = context.Users.Single(user => user.UserName =="user@guitarmanagement.be")
        };
        GuitarModel guitar8 = new GuitarModel()
        {
            Name = "J35",
            AmountOfStrings = 6,
            Price = 2500.00,
            Type = GuitarType.ACOUSTIC,
            Manufacturer = manufacturer2,
            Stock = new List<Stock>(),
            Id = 8,
            ModelOwner = context.Users.Single(user => user.UserName =="admin@guitarmanagement.be")
        };
        //STORE INIT
        Store store1 = new Store()
        {
            Name = "Bax-Music",
            Address = "Paleisstraat 119",
            Location = "Antwerpen, Belgium",
            Stock = new List<Stock>(),
            Id = 1
        };
        Store store2 = new Store()
        {
            Name = "Musicstore",
            Address = "Online",
            Location = "Online",
            Stock = new List<Stock>(),
            Id = 2
        };
        Store store3 = new Store()
        {
            Name = "Music World",
            Address = "Elewijtsesteenweg 10",
            Location = "Zemst, Belgium",
            Stock = new List<Stock>(),
            Id = 3
        };
        Store store4 = new Store()
        {
            Name = "Music All In",
            Address = "Keyserwey 63",
            Location = "Noordwijk, Netherlands",
            Stock = new List<Stock>(),
            Id = 4
        };
        Store store5 = new Store()
        {
            Name = "Bax-Music",
            Address = "Olympiastraat 4",
            Location = "Goes, Netherlands",
            Stock = new List<Stock>(),
            Id = 5
        };
        //CREATE STOCK ELEMENTS
        Stock stock1 = new Stock()
        {
            Store = store1,
            GuitarModel = guitar1,
            Amount = 20,
        };
        Stock stock2 = new Stock()
        {
            Store = store1,
            GuitarModel = guitar3,
            Amount = 2,
        };
        Stock stock3 = new Stock()
        {
            Store = store1,
            GuitarModel = guitar4,
            Amount = 2,
        };
        Stock stock4 = new Stock()
        {
            Store = store1,
            GuitarModel = guitar6,
            Amount = 2
        };
        Stock stock5 = new Stock()
        {
            Store = store2,
            GuitarModel = guitar2,
            Amount = 20,
        };
        Stock stock6 = new Stock()
        {
            Store = store2,
            GuitarModel = guitar3,
            Amount = 20,
        };
        Stock stock7 =new Stock()
        {
            Store = store2,
            GuitarModel = guitar4,
            Amount = 20,
        };
        Stock stock8 =new Stock()
        {
            Store = store2,
            GuitarModel = guitar5,
            Amount = 20,
        };
        Stock stock9 = new Stock()
        {
            Store = store3,
            GuitarModel = guitar1,
            Amount = 20,
        };
        Stock stock10 = new Stock()
        {
            Store = store3,
            GuitarModel = guitar2,
            Amount = 20,
        };
        Stock stock11 = new Stock()
        {
            Store = store3,
            GuitarModel = guitar7,
            Amount = 20,
        };
        Stock stock12 = new Stock()
        {
            Store = store3,
            GuitarModel = guitar8,
            Amount = 20,
        };
        Stock stock13 = new Stock()
        {
            Store = store4,
            GuitarModel = guitar3,
            Amount = 20,
        };
       Stock stock14 =  new Stock()
        {
            Store = store4,
            GuitarModel = guitar4,
            Amount = 20,
        };
       Stock stock15 = new Stock()
       {
           Store = store4,
           GuitarModel = guitar5,
           Amount = 20,
       };
       Stock stock16 = new Stock()
       {
           Store = store5,
           GuitarModel = guitar1,
           Amount = 20,
       };
       Stock stock17 = new Stock()
       {
           Store = store5,
           GuitarModel = guitar2,
           Amount = 20,
       };
       Stock stock18 = new Stock()
       {
           Store = store5,
           GuitarModel = guitar5,
           Amount = 20,
       };
       Stock stock19 = new Stock()
       {
           Store = store5,
           GuitarModel = guitar6,
           Amount = 20,
       };
       //FILL GUITAR STOCK
        FillList(guitar1.Stock, new List<Stock>()
        {
            stock1,
            stock9
        });
        FillList(guitar2.Stock, new List<Stock>()
        {
            stock5, 
            stock10,
            stock16
        });
        FillList(guitar3.Stock, new List<Stock>()
        {
           stock2,
           stock6,
           stock13,
           stock17
        });
        FillList(guitar4.Stock, new List<Stock>()
        {
          stock3,
          stock7,
          stock14
        });
        FillList(guitar5.Stock, new List<Stock>()
        {
           stock8,
           stock15,
           stock18
        });
        FillList(guitar6.Stock, new List<Stock>()
        {
            stock4,
            stock19
        });
        FillList(guitar7.Stock, new List<Stock>()
        {
           stock11
        });
        FillList(guitar8.Stock, new List<Stock>()
        {
            stock12
        });
    //FILLUP AND CREATE STOCK ELEMENTS FOR STORES AND GUIT.MODELS
        FillList(store1.Stock, new List<Stock>()
        {
            stock1,
            stock2,
            stock3,
            stock4
        });
        FillList(store2.Stock, new List<Stock>()
        {
            stock5,
            stock6,
            stock7,
            stock8
        });
        FillList(store3.Stock, new List<Stock>()
        {
            stock9,
            stock10,
            stock11,
            stock12
        });
        FillList(store4.Stock, new List<Stock>()
        {
            stock13,
            stock14,
            stock15,
        });
        FillList(store5.Stock, new List<Stock>()
        {
            stock16,
            stock17,
            stock18,
            stock19
        });
        //ADD GUITARMODELS
        context.GuitarModels.Add(guitar1);
        context.GuitarModels.Add(guitar2);
        context.GuitarModels.Add(guitar3);
        context.GuitarModels.Add(guitar4);
        context.GuitarModels.Add(guitar5);
        context.GuitarModels.Add(guitar6);
        context.GuitarModels.Add(guitar7);
        context.GuitarModels.Add(guitar8);
        //ADD MANUFACTURERRS
        context.Manufacturers.Add(manufacturer1);
        context.Manufacturers.Add(manufacturer2);
        context.Manufacturers.Add(manufacturer3);
        //ADD STORES
        context.Stores.Add(store1);
        context.Stores.Add(store2);
        context.Stores.Add(store3);
        context.Stores.Add(store4);
        context.Stores.Add(store5);

        context.SaveChanges();
        
        context.ChangeTracker.Clear();
    }
    
    private static void FillList<T>(ICollection<T> list, ICollection<T> listOfNewItems)
    {
        if (listOfNewItems.Count != 0)
        {
            foreach (var item in listOfNewItems)
            {
                list.Add(item);
            }
        }
    }
}