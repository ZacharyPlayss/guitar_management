using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using GuitarManagement.DAL.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Tests.IntegrationTests.Config;
using UI_MVC.Models.Dto;

namespace Tests.IntegrationTests;

public class GuitarsControllerTests: IClassFixture<ExtendedWebApplicationFactoryWithMockAuth<Program>>
{
    private readonly ExtendedWebApplicationFactoryWithMockAuth<Program> _factory;
    
    public GuitarsControllerTests(ExtendedWebApplicationFactoryWithMockAuth<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public void Update_Guitar_Should_Require_Authenticated_User()
    {
        //Arrange
        var httpClient = _factory.CreateClient(
            new WebApplicationFactoryClientOptions{ AllowAutoRedirect = false});
        
        //Act
        var response = httpClient.PutAsync("/api/Guitars/1", null).Result;
        //Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public void update_Guitar_Should_Return_Ok_When_User_Is_Guitar_Maintainer()
    {
        //Arrange
        var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GuitarDbContext>();
        var user1 = dbContext.Users.First();
        
        var httpClient = _factory
            .SetAuthenticatedUser( 
                new Claim(ClaimTypes.NameIdentifier, user1.Id),
                new Claim(ClaimTypes.Role,"user") )
            .CreateClient();
        
        var httpBody = new StringContent(JsonSerializer.Serialize(new UpdateGuitarModelDto()
        {
            Price = 50
        }), Encoding.UTF8, "application/json");
        //Act
        var response = httpClient.PutAsync("/api/Guitars/1", httpBody).Result;
        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public void update_Guitar_Should_Return_Ok_When_User_Is_Not_Guitar_Maintainer_But_Has_Admin_Role()
    {
        //Arrange
        var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GuitarDbContext>();
        // Moet user zijn die in seeding ook Admin Role bevat.
        var user4 = dbContext.Users.Single(u => u.UserName =="lars@kdg.be");
        
        var httpClient = _factory
            .SetAuthenticatedUser( 
                new Claim(ClaimTypes.NameIdentifier, user4.Id),
                new Claim(ClaimTypes.Role,"admin"))
            .CreateClient();
        
        var httpBody = new StringContent(JsonSerializer.Serialize(new UpdateGuitarModelDto()
        {
            Price = 50
        }), Encoding.UTF8, "application/json");
        //Act
        var response = httpClient.PutAsync("/api/Guitars/1", httpBody).Result;
        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}