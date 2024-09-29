using System.ComponentModel.DataAnnotations;
using GuitarManagement.BL;
using GuitarManagement.BL.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.IntegrationTests;

public class ManagerTests: IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public ManagerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    //STORE
    [Fact]
    public void AddStore_With_Valid_Data_ReturnsStore()
    {
        //Arrange
        using var scope = _factory.Services.CreateScope();
        var manager = scope.ServiceProvider.GetRequiredService<IManager>();
        
        //Act
        var store = manager.AddStore("TestStore","teststreet 20","Belgium");
        
        //Assert
        Assert.NotNull(store);
        Assert.InRange(store.Id, 1, Int64.MaxValue);
        Assert.Equal("TestStore", store.Name);
        Assert.Equal("teststreet 20", store.Address);
        Assert.Equal("Belgium", store.Location);
        Assert.True(store.Stock.Count == 0);
    }
    [Fact]
    public void AddStore_With_Invalid_Data_Throws_ValidationError()
    {
        //Arrange
        using var scope = _factory.Services.CreateScope();
        var manager = scope.ServiceProvider.GetRequiredService<IManager>();
        
        //Act
        var action = () => manager.AddStore("te",null,null);
        
        //Assert
        Assert.Throws<ValidationException>(action);
    }
    
    //GUITARMODEL
    [Fact]
    public void AddGuitarModel_With_Valid_Data_Returns_GuitarModel()
    {
        //Arrange
        using var scope = _factory.Services.CreateScope();
        var manager = scope.ServiceProvider.GetRequiredService<IManager>();
        //Act
        var guitarModel = manager.AddGuitarModel("Les paul",6,2999.99,GuitarType.ELECTRIC);
        //Assert
        Assert.NotNull(guitarModel);
        Assert.InRange(guitarModel.Id, 1, Int64.MaxValue);
        Assert.Equal("Les paul", guitarModel.Name);
        Assert.Equal(6, guitarModel.AmountOfStrings);
        Assert.Equal(2999.99, guitarModel.Price);
        Assert.Equal(GuitarType.ELECTRIC, guitarModel.Type);
        Assert.Null(guitarModel.ModelOwner);
        Assert.True(guitarModel.Stock.Count == 0);
    }
    
    [Fact]
    public void AddGuitarModel_With_Invalid_Data_Throws_ValidationError()
    {
        //Arrange
        using var scope = _factory.Services.CreateScope();
        var manager = scope.ServiceProvider.GetRequiredService<IManager>();
        //Act
        var action = () => manager.AddGuitarModel("Les paul",0,-50,GuitarType.ELECTRIC);
        //Assert
        Assert.Throws<ValidationException>(action);
    }
}