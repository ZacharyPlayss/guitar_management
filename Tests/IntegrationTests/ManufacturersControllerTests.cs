using System.Net;
using System.Security.Claims;
using System.Text;
using Tests.IntegrationTests.Config;

namespace Tests.IntegrationTests;

public class ManufacturersControllerTests : IClassFixture<CustomWebApplicationFactoryWithMockAuth<Program>>
{
    private readonly CustomWebApplicationFactoryWithMockAuth<Program> _factory;

    public ManufacturersControllerTests(CustomWebApplicationFactoryWithMockAuth<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void addManufacturer_Should_require_Authentication_if_not_signed_in()
    {
        //Arrange
        var httpClient = _factory
            .CreateClient();
        //Act
        var response = httpClient.PostAsync("/api/Manufacturers", null).Result;
        //Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public void addManufacturer_Should_return_badRequest_With_Invalid_Data()
    {
        //Arrange
        var httpClient = _factory
            .AuthenticatedInstance(new Claim(ClaimTypes.NameIdentifier,"zachary@kdg.be")) 
            //Deze gebruiker moet niet bestaan. Omdat er geen user controles plaatsvinden in de controller.
            .CreateClient();
        var httpBody = new StringContent("{\"Name\": \"Test Manufacturer\",\"DateFounded\": \"2023-01-01\",\"Location\": \"Test Location\"}", Encoding.UTF8, "application/json");
        //Act
        var response = httpClient.PostAsync("/api/Manufacturers", httpBody).Result;
        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
    [Fact]
    public void addManufacturer_Should_return_Created_Manufacturer_with_valid_data()
    {
        //Arrange
        var httpClient = _factory
            .AuthenticatedInstance(new Claim(ClaimTypes.NameIdentifier,"zachary@kdg.be")) 
            .CreateClient();
        var httpBody = new StringContent("{\"Name\": \"testMan\"}", Encoding.UTF8, "application/json");
        //Act
        var response = httpClient.PostAsync("/api/Manufacturers", httpBody).Result;
        //Assert
        var responseBody = response.Content.ReadAsStringAsync().Result;
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Matches("{\"id\":\\d+,\"name\":\"testMan\",\"dateFounded\":\"0001-01-01T00:00:00\",\"location\":null}",responseBody);
        Assert.Matches("/api/Manufacturers/\\d+", response.Headers.Location?.PathAndQuery);
    }
}