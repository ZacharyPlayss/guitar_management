using System.Security.Claims;
using GuitarManagement.BL;
using GuitarManagement.BL.Domain;
using GuitarManagement.UI.MVC.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Tests.Helpers;
using UI_MVC.Models.Dto;

namespace Tests.UnitTests;

public class GuitarControllerTests
{
    private readonly GuitarController _guitarController;
    private readonly Mock<IManager> _managerMock;
    private readonly Mock<UserManager<IdentityUser>> _userManagerMock;

    public GuitarControllerTests()
    {
        _managerMock = TestsHelper.GetMockManager();
        _userManagerMock = TestsHelper.GetMockUserManager<IdentityUser>();
        _guitarController = new GuitarController(_managerMock.Object, _userManagerMock.Object);
    }
    
    [Fact]
    public void AddGuitar_Should_Return_Same_Page_ViewResult_When_Invalid_ModelState()
    {
        //Arrange
        _guitarController.ModelState.AddModelError("Name","Name");
        //Act
        var result = _guitarController.Add(new GuitarModelDto());
        //Assert
        Assert.IsType<ViewResult>(result);
        var viewResult = (ViewResult)result;
        Assert.Null(viewResult.ViewName);
        _managerMock.Verify(mgr => mgr.AddGuitarModelWithMaintainer(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<double>(), It.IsAny<GuitarType>(),It.IsAny<string>()),
            Times.Never);
    }
    
    [Fact]
    public void AddGuitar_Should_Return_Same_Page_ViewResult_When_Valid_Input()
    {
        //Arrange
        _userManagerMock.Setup(uMgr => uMgr.GetUserId(It.IsAny<ClaimsPrincipal>()))
            .Returns("Mock_id");
        //Act
        var result = _guitarController.Add(new GuitarModelDto
        {
            Name = "Test",
            AmountOfStrings = 6,
            Price = 200,
            Type = GuitarType.ELECTRIC
        });
        //Assert
        Assert.IsType<RedirectToActionResult>(result);
        var redirectResult = (RedirectToActionResult)result;
        Assert.Equal("Index",redirectResult.ActionName);
        _managerMock.Verify(mgr => mgr.AddGuitarModelWithMaintainer("Test", 6,200, GuitarType.ELECTRIC,"Mock_id"),
            Times.Once);
    }
    
}