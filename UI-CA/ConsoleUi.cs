using System.ComponentModel.DataAnnotations;
using System.Text;
using GuitarManagement.BL;
using GuitarManagement.BL.Domain;
using GuitarManagement.UI.CA.Extensions;

namespace GuitarManagement.UI.CA;

public class ConsoleUi
{
    private readonly IManager _manager;

    public ConsoleUi(IManager manager)
    {
        _manager = manager;
    }

    private void ShowSelectionMenu()
    {
        Console.WriteLine("What would you like to do?\n==========================");
        Console.WriteLine("0) Quit");
        Console.WriteLine("1) Show all guitar models");
        Console.WriteLine("2) Show guitar models by type");
        Console.WriteLine("3) Show all stores");
        Console.WriteLine("4) Show stores with name and/or location");
        Console.WriteLine("5) Add a new guitar model.");
        Console.WriteLine("6) Add a new store.");
        Console.WriteLine("7) Add guitar model to a store.");
        Console.WriteLine("8) Remove guitar model from a store.");
        Console.Write("Choice (0-8):");
    }
    private void ShowEnd()
    {
        Console.WriteLine("Thanks for using our software!");
    }
    
    private void ShowGuitarmodels()
    {
        Console.WriteLine("All guitarmodels\n=============");
        foreach (var model in _manager.GetAllGuitarModelsAndManufacturer())
        {
            Console.WriteLine("- " + model.GetStringRepresentation());
        }
        Console.WriteLine();
    }
    private void ShowGuitarmodelsByType()
    {
        bool isValidated = false;
        do
        {
            string selectedType;
            string typeOptions = ShowTypeOptions();
            Console.Write("select a type {0} :", typeOptions);
            var input = Console.ReadLine();
            var inputAsNumber = Convert.ToByte(input);
            if (Enum.IsDefined(typeof(GuitarType), inputAsNumber))
            {
                isValidated = true;
                GuitarType guitarType = (GuitarType)inputAsNumber;
                IEnumerable<GuitarModel> matchingGuitarModels = _manager.GetAllGuitarModelsByType(guitarType);
                foreach (var guitarModel in matchingGuitarModels)
                {
                    Console.WriteLine("- " + guitarModel);
                }
            }
        } while (!isValidated);
        Console.WriteLine();
        
    }
    private void ShowStores()
    {
        Console.WriteLine("All stores\n=============");
        foreach (Store store in _manager.GetAllStoresWithStockAndGuitarModels())
        {
            Console.WriteLine("- " + store.GetStringRepresentationWithGuitarModel());
        }
        Console.WriteLine();
    }

    
    private void ShowStoresByLocation()
    {
        string inputName;
        string inputCountry;
        Console.Write("Enter (part of) a name or leave blank:");
        inputName = Console.ReadLine();
        Console.Write("Enter a country / city or leave blank:");
        inputCountry = Console.ReadLine();
        Console.WriteLine("Stores using your input:\n=============");
        ICollection<Store> stores = _manager.GetStoresByNameAndLocation(inputName, inputCountry);

        foreach (var store in stores)
        {
            Console.WriteLine("- " + store.GetSimpleStringRepresentation());
        }
        
        Console.WriteLine();
    }

    private void AddGuitarmodel()
    {
        GuitarModel guitar = new GuitarModel();
        bool validationOk = false;
        Console.WriteLine("Add a guitar model\n========");
        do
        {
            Console.Write("Name:");
            string nameInput = Console.ReadLine();
            Console.Write("Amount of strings:");
            string amountOfStringsInput = Console.ReadLine();
            int amountOfStrings = Convert.ToInt32(amountOfStringsInput);
            Console.Write("Price (use , for decimals):");
            string priceInput = Console.ReadLine();
            double price = Convert.ToDouble(priceInput);
            string typeOptions = ShowTypeOptions();
            bool typeValid = false;
            GuitarType guitarType = (GuitarType)0;
            do
            {
                Console.Write("Type {0}:", typeOptions);
                string typeInput = Console.ReadLine();
                var typeInputAsNumber = Convert.ToByte(typeInput);
                if (Enum.IsDefined(typeof(GuitarType), typeInputAsNumber))
                {
                    guitarType = (GuitarType)typeInputAsNumber;
                    typeValid = true;
                }
            } while (!typeValid);
            
            try
            {
                guitar = _manager.AddGuitarModel(nameInput, amountOfStrings, price, guitarType);
                validationOk = true;
            }
            catch (ValidationException e)
            {
                Console.WriteLine(e.Message);
                validationOk = false;
            }
        } while (!validationOk);
    }

    private void AddStore()
    {
        Store store = new Store();
        bool validationOk = false;
        Console.WriteLine("Add a store\n========");
        do
        {
            Console.Write("Name:");
            string nameInput = Console.ReadLine();
            Console.Write("Location ((optional city), country bv. \"Antwerp, Belgium\"):");
            string locationInput = Console.ReadLine();
            Console.Write("Address (street nr, ZIP code):");
            string addressInput = Console.ReadLine();
            
            try
            {
                store = _manager.AddStore(nameInput,addressInput,locationInput);
                validationOk = true;
            }
            catch (ValidationException e)
            {
                Console.WriteLine(e.Message);
                validationOk = false;
            }
        } while (!validationOk);
    }

    private void AddStoreGuitarModel()
    {
        Console.WriteLine();
        Console.WriteLine("Which store would u like to add a guitarmodel to ?");
        int count = 1;
        foreach (Store store in _manager.GetAllStores())
        {
            Console.WriteLine($"[{count}] {store.GetSimpleStringRepresentation()}");
            count++;
        }

        Console.Write("Please enter a store ID:");
        string selection = Console.ReadLine();
        if (int.TryParse(selection, out int storeId))
        {
            int modelCount = 1;
            Console.WriteLine($"Which guitarmodel would you like to assing to store {storeId}?");
            IEnumerable<GuitarModel> guitarsNotInStore = _manager.GetGuitarModelsNotInStore(storeId);
            foreach (var guitarModel in guitarsNotInStore)
            {
                Console.WriteLine($"[{modelCount}] {guitarModel.Name}");
                modelCount++;
            };
            Console.Write("Please enter a guitarmodel ID:");
            string guitarSelection = Console.ReadLine();
            if (int.TryParse(guitarSelection, out int selectedGuitarModelId))
            {
                _manager.AddStoreGuitarModel(_manager.GetStore(storeId), guitarsNotInStore.ElementAt(selectedGuitarModelId - 1));
            }
        }
    }
    private string ShowTypeOptions()
    {
        var enums = Enum.GetValues<GuitarType>();
        int amountOfEnums = enums.Length;
        StringBuilder sb = new StringBuilder();
        sb.Append("(");
        foreach (var type in enums)
        {
            sb.Append($"{((int)type)}={type}");
            if (--amountOfEnums > 0)
            {
                sb.Append(", "); // Add a comma and space after each item, except the last one
            }
        }

        sb.Append(')');

        return sb.ToString();
    }
    public void Run()
    {
        string selection;
        do
        {
            ShowSelectionMenu();
            selection = Console.ReadLine();
            if (int.TryParse(selection, out int selectionNumber))
            {
                switch (selectionNumber)
                {
                    case 0:
                        ShowEnd();
                        break;
                    case 1:
                        ShowGuitarmodels();
                        break;
                    case 2:
                        ShowGuitarmodelsByType();
                        break;
                    case 3:
                        ShowStores();
                        break;
                    case 4:
                        ShowStoresByLocation();
                        break;
                    case 5:
                        AddGuitarmodel();
                        break;
                    case 6:
                        AddStore();
                        break;
                    case 7:
                        AddStoreGuitarModel();
                        break;
                    case 8:
                        RemoveStoreGuitarModel();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number!");
            }
        } while (selection != "0");
        Console.WriteLine("Program run succesfully!");
    }

    private void RemoveStoreGuitarModel()
    {
        Console.WriteLine("Which store would u like to remove a guitarmodel from?");
        int count = 1;
        foreach (Store store in _manager.GetAllStores())
        {
            Console.WriteLine($"[{count}] {store.GetSimpleStringRepresentation()}");
            count++;
        }

        Console.Write("Please enter a store ID:");
        string selection = Console.ReadLine();
        if (int.TryParse(selection, out int storeId))
        {
            int modelCount = 1;
            var guitarModelsInStore = _manager.GetGuitarModelsInStore(storeId);
            Console.WriteLine($"Which guitarmodel would you like to remove from this store ?");
            foreach (var guitarModel in guitarModelsInStore)
            {
                Console.WriteLine($"[{modelCount}] {guitarModel.Name}");
                modelCount++;
            } ;
            
            Console.Write("Please enter a guitarmodel ID:");
            string guitarSelection = Console.ReadLine();
            if (int.TryParse(guitarSelection, out int guitarModelId))
            {
                Console.Write("\n" + guitarModelId  +"\n" );
                if (guitarModelId > guitarModelsInStore.Count()) {
                    Console.WriteLine("Your selection was not valid...");
                }else{
                    GuitarModel selectedGuitarModel = guitarModelsInStore.ElementAt((guitarModelId - 1));
                    _manager.RemoveStoreGuitarModel(selectedGuitarModel.Id, storeId);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Guitarmodel removed from store!");
            Console.WriteLine();
        }
    }
}