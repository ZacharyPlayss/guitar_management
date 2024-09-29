using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using GuitarManagement.BL;
using GuitarManagement.BL.Domain;
using GuitarManagement.DAL;
using GuitarManagement.UI.MVC;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Tests.UnitTests;

public class ManagerTests
{
    private readonly IManager _manager;
    private readonly Mock<IRepository> _repositoryMock;

    public ManagerTests()
    {
        _repositoryMock = TestsHelper.GetRepositoryMock();
        _manager = new Manager(_repositoryMock.Object);
    }

    [Fact]
    public void AddGuitarModel_Should_Reject_Invalid_GuitarModel_Data()
    {
        //Arrange

        //Act
        var action = () => _manager.AddGuitarModel("NewGuitar", 51, -200, GuitarType.ELECTRIC);

        //Assert
        Assert.Throws<ValidationException>(action);
        _repositoryMock.Verify(repo => repo.createGuitarModel(It.IsAny<GuitarModel>()),
            Times.Never);
    }

    [Fact]
    public void AddGuitarModel_Should_Accept_Valid_GuitarModel_Data()
    {
        //Arrange

        //Act
        var createdGuitarModel = _manager.AddGuitarModel("NewGuitar", 6, 200, GuitarType.ELECTRIC);

        //Assert
        _repositoryMock.Verify(repo => repo.createGuitarModel(It.IsAny<GuitarModel>()),
            Times.Once);
        Assert.NotNull(createdGuitarModel);
        Assert.Equal("NewGuitar", createdGuitarModel.Name);
        Assert.Equal(6, createdGuitarModel.AmountOfStrings);
        Assert.Equal(200, createdGuitarModel.Price);
        Assert.Equal(GuitarType.ELECTRIC, createdGuitarModel.Type);
    }

    [Fact]
    public void AddGuitarModelWithMaintainer_should_reject_invalid_guitarModel_Data()
    {
        //Arrange
        _repositoryMock.Setup(rep => rep.ReadUser("5")).Returns(new IdentityUser
        {
            Id = "5",
            Email = "test@user.com"
        });
        //Act
        var action = () => _manager.AddGuitarModelWithMaintainer("newGuitar", 51, -200, GuitarType.SEMI, "5");

        //Assert
        Assert.Throws<ValidationException>(action);
        _repositoryMock.Verify(repo => repo.ReadUser(It.IsAny<string>()),
            Times.Once);
        _repositoryMock.Verify(repo => repo.createGuitarModel(It.IsAny<GuitarModel>()), Times.Never);
    }

    [Fact]
    public void AddGuitarModelWithMaintainer_should_accept_valid_guitarmodel_data()
    {
        //Arrange
        _repositoryMock.Setup(rep => rep.ReadUser("5")).Returns(new IdentityUser
        {
            Id = "5",
            Email = "test@user.com"
        });

        //Act
        var createdGuitarModel = _manager.AddGuitarModelWithMaintainer("newGuitar", 6, 200, GuitarType.SEMI, "5");

        //Assert
        _repositoryMock.Verify(repo => repo.ReadUser(It.IsAny<string>()),
            Times.Once);
        _repositoryMock.Verify(repo => repo.createGuitarModel(It.IsAny<GuitarModel>()), Times.Once);
        Assert.Equal("5", createdGuitarModel.ModelOwner.Id);
        Assert.Equal("test@user.com", createdGuitarModel.ModelOwner.Email);
        Assert.Equal("newGuitar", createdGuitarModel.Name);
        Assert.Equal(6, createdGuitarModel.AmountOfStrings);
        Assert.Equal(200, createdGuitarModel.Price);
        Assert.Equal(GuitarType.SEMI, createdGuitarModel.Type);
    }

    [Fact]
    public void UpdateGuitarPrice_Should_Require_A_Valid_UserId_Which_Matches_The_Models_ModelOwner_Field()
    {
        //Arrange
        _repositoryMock.Setup(rep => rep.ReadUser("5")).Returns(new IdentityUser
        {
            Id = "5",
            Email = "test@user.com"
        });
        _repositoryMock.Setup(rep => rep.ReadUserRole("5"))
            .Returns(new IdentityRole()
            {
                Id = "1",
                Name = "user",
            });
        _repositoryMock.Setup(rep => rep.readGuitarModel(1))
            .Returns(new GuitarModel()
            {
                Id = 1,
                AmountOfStrings = 6,
                Name = "TestGuitar",
                Price = 500,
                ModelOwner = new IdentityUser()
                {
                    Id = "5",
                    Email = "modelOwner@user.com"
                }
            });
        //Act
        var action = () => _manager.UpdateGuitarPrice(1, "5", 200);
        //Assert
        Assert.Throws<AuthenticationException>(action);
        _repositoryMock.Verify(repo => repo.updateGuitar(It.IsAny<GuitarModel>()),
            Times.Never);
    }
    [Fact]
    public void UpdateGuitarPrice_Should_Update_The_Guitar_Price_When_Provided_User_Is_Owner()
    {
        //Arrange
        var testUser = new IdentityUser()
        {
            Id = "2",
            Email = "test@user.com"
        };
        var testGuitarModel = new GuitarModel()
        {
            Id = 1,
            AmountOfStrings = 6,
            Name = "TestGuitar",
            Price = 500,
            ModelOwner = testUser
        };
        _repositoryMock.Setup(rep => rep.ReadUser("2")).Returns(testUser);
        _repositoryMock.Setup(rep => rep.ReadUserRole("2"))
            .Returns(new IdentityRole()
            {
                Id = "1",
                Name = "user",
            });
        _repositoryMock.Setup(rep => rep.readGuitarModel(1))
            .Returns(testGuitarModel);
        _repositoryMock.Setup(rep => rep.updateGuitar(testGuitarModel))
            .Returns(new GuitarModel()
            {
                Id = 1,
                AmountOfStrings = 6,
                Name = "TestGuitar",
                Price = 200,
                ModelOwner = testUser
            });
        //Act
        var guitarModel = _manager.UpdateGuitarPrice(1, "2", 200);
        //Assert
        Assert.Equal(1, guitarModel.Id);
        Assert.Equal(6, guitarModel.AmountOfStrings);
        Assert.Equal("TestGuitar", guitarModel.Name);
        Assert.Equal(200, guitarModel.Price);
        Assert.Equal(testUser, guitarModel.ModelOwner);
        _repositoryMock.Verify(repo => repo.updateGuitar(It.IsAny<GuitarModel>()),
            Times.Once);
    }
    [Fact]
    public void UpdateGuitarPrice_Should_Update_The_Guitar_Price_When_Provided_User_Is_Not_Owner_But_Is_In_Admin_Role()
    {
        //Arrange
        var testUser = new IdentityUser()
        {
            Id = "2",
            Email = "test@user.com"
        };
        var testGuitarModel = new GuitarModel()
        {
            Id = 1,
            AmountOfStrings = 6,
            Name = "TestGuitar",
            Price = 500,
            ModelOwner = new IdentityUser()
            {
                Id = "3",
                Email = "modelowner@email.com"
            }
        };
        _repositoryMock.Setup(rep => rep.ReadUser("2")).Returns(testUser);
        _repositoryMock.Setup(rep => rep.ReadUserRole("2"))
            .Returns(new IdentityRole()
            {
                Id = "1",
                Name = "admin",
            });
        _repositoryMock.Setup(rep => rep.readGuitarModel(1))
            .Returns(testGuitarModel);
        _repositoryMock.Setup(rep => rep.updateGuitar(testGuitarModel))
            .Returns(new GuitarModel()
            {
                Id = 1,
                AmountOfStrings = 6,
                Name = "TestGuitar",
                Price = 200,
                ModelOwner = testUser
            });
        //Act
        var guitarModel = _manager.UpdateGuitarPrice(1, "2", 200);
        //Assert
        Assert.Equal(1, guitarModel.Id);
        Assert.Equal(6, guitarModel.AmountOfStrings);
        Assert.Equal("TestGuitar", guitarModel.Name);
        Assert.Equal(200, guitarModel.Price);
        Assert.Equal(testUser, guitarModel.ModelOwner);
        _repositoryMock.Verify(repo => repo.updateGuitar(It.IsAny<GuitarModel>()),
            Times.Once);
    }

    [Fact]
    public void AddStock_Should_Require_A_Valid_Store_Id()
    {
        //Arrange
        var testStore = new Store()
        {
            Id = 1
        };
        var testGuitar = new GuitarModel()
        {
            Id = 1
        };
        _repositoryMock.Setup(rep => rep.ReadStore(1)).Returns(testStore);
        _repositoryMock.Setup(rep => rep.readGuitarModel(1)).Returns(testGuitar);
        //Act
        var action = () => _manager.AddStock(-1, 1, 5);
        //Assert
        Assert.Throws<ValidationException>(action);
        _repositoryMock.Verify(repo => repo.ReadStore(It.IsAny<long>()),
            Times.Once);
        _repositoryMock.Verify(repo => repo.readGuitarModel(It.IsAny<long>()),
            Times.Once);
        _repositoryMock.Verify(repo => repo.CreateStockElement(It.IsAny<Stock>()),
            Times.Never);
    }
    
    [Fact]
    public void AddStock_Should_Require_A_Valid_GuitarModelId()
    {
        //Arrange
        var testStore = new Store()
        {
            Id = 1
        };
        var testGuitar = new GuitarModel()
        {
            Id = 1
        };
        _repositoryMock.Setup(rep => rep.ReadStore(1)).Returns(testStore);
        _repositoryMock.Setup(rep => rep.readGuitarModel(1)).Returns(testGuitar);
        //Act
        var action = () => _manager.AddStock(1, -1, 5);
        //Assert
        Assert.Throws<ValidationException>(action);
        _repositoryMock.Verify(repo => repo.ReadStore(It.IsAny<long>()),
            Times.Once);
        _repositoryMock.Verify(repo => repo.readGuitarModel(It.IsAny<long>()),
            Times.Once);
        _repositoryMock.Verify(repo => repo.CreateStockElement(It.IsAny<Stock>()),
            Times.Never);
    }
    
    [Fact]
    public void AddStock_Should_Create_Add_Stock_To_Given_Store_And_GuitarModel()
    {
        //Arrange
        var testStore = new Store()
        {
            Id = 1
        };
        var testGuitar = new GuitarModel()
        {
            Id = 1
        };
        _repositoryMock.Setup(rep => rep.ReadStore(1)).Returns(testStore);
        _repositoryMock.Setup(rep => rep.readGuitarModel(1)).Returns(testGuitar);
        //Act
        var stock = _manager.AddStock(1, 1, 5);
        //Assert
        Assert.Equal(1,stock.GuitarModel.Id);
        Assert.Equal(1,stock.Store.Id);
        Assert.Equal(5,stock.Amount);
        _repositoryMock.Verify(repo => repo.ReadStore(It.IsAny<long>()),
            Times.Once);
        _repositoryMock.Verify(repo => repo.readGuitarModel(It.IsAny<long>()),
            Times.Once);
        _repositoryMock.Verify(repo => repo.CreateStockElement(It.IsAny<Stock>()),
            Times.Once);
    }
    
}