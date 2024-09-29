using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Tests.Helpers;

public class MockHelper
{
    public static Mock<UserManager<TUser>> GetMockUserManager<TUser>()
        where TUser : class
    {
        var userManagerMock = new Mock<UserManager<TUser>>(
            new Mock<IUserStore<TUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<TUser>>().Object,
            new IUserValidator<TUser>[0],
            new IPasswordValidator<TUser>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<TUser>>>().Object);
        return userManagerMock;
    }
}