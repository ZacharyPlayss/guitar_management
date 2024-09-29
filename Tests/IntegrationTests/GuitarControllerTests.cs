using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Testing;
using Tests.IntegrationTests.Config;

namespace Tests.IntegrationTests;

public class GuitarControllerTests : IClassFixture<CustomWebApplicationFactoryWithMockAuth<Program>>
{
    private readonly CustomWebApplicationFactoryWithMockAuth<Program> _factory;

    public GuitarControllerTests(CustomWebApplicationFactoryWithMockAuth<Program> factory)
    {
        _factory = factory;
    }


    [Fact]
    public void Add_Guitar_Should_Require_Authenticated_User()
    {
        //Arrange
        var httpClient = _factory.CreateClient(
            new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
        
        //Act
        var response = httpClient.PostAsync("/Guitar/Add", null).Result;
        //Assert
        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        Assert.Equal("/Identity/Account/Login", response.Headers.Location?.AbsolutePath);
    }
    [Fact]
    public void Add_Guitar_Should_Not_Accept_Invalid_Input()
    {
        //Arrange
        var httpClient = _factory
            .AuthenticatedInstance(
                new Claim(ClaimTypes.NameIdentifier, "zachary@kdg.be")
            )
            .CreateClient();
        
        //Act
        var response = httpClient.PostAsync("/Guitar/Add", null).Result;
        var content = response.Content.ReadAsStringAsync().Result;
        
        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains(">The field AmountOfStrings must be between 1 and 50.<",content);
    }
    
    [Fact]
    public void Add_Guitar_Should_Accept_Valid_Input()
    {
        //Arrange
        var httpClient = _factory
            .AuthenticatedInstance(
                new Claim(ClaimTypes.NameIdentifier, "zachary@kdg.be")
            )
            .CreateClient();
        
        //Act
        var response = httpClient.PostAsync("/Guitar/Add", 
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"Name","TestGuitar"},
                {"AmountOfStrings", "6"},
                {"Price","250,95"},
                {"Type","ACOUSTIC"}
            })).Result;
        var content = response.Content.ReadAsStringAsync().Result;
        
        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.DoesNotContain(">The field AmountOfStrings must be between 1 and 50.<",content);
        Assert.DoesNotContain(">The field AmountOfStrings must be between 1 and 50.<",content);
    }
    
    
}