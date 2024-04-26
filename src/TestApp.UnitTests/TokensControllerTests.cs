using TestApp.Mocking;
using Xunit;
using Assert = Xunit.Assert;

namespace TestApp.UnitTests;

public class AuthorizeServiceTests
{
    [Fact]
    public void Create_ValidUsernameAndPassword_ShouldReturnsOk()
    {
        IAuthorizeService sut = new FakeAuthorizeService();

        var result = sut.Authorize("a", "123");

        Assert.True(result);
    }

    [Fact]
    public void Create_InvalidUsernameAndPassword_ShouldReturnsUnauthorized()
    {
        IAuthorizeService sut = new FakeAuthorizeService();

        var result = sut.Authorize("a", "1234");

        Assert.False(result);
    }
}

public class FakeAuthorizeService : IAuthorizeService
{
    public bool Authorize(string username, string password)
    {
        return username == "a" & password == "123";
    }
}

public class FakeTokenService : ITokenService
{
    public string CreateToken() => "abc";
}

public class TokensControllerTests
{
    [Fact]
    public void Create_ValidUsernameAndPassword_ShouldReturnsOk()
    {
        var sut = new TokensController(new FakeAuthorizeService(), new FakeTokenService());
        var model = new LoginModel { Username = "a", Password = "123" };

        var result = sut.Create(model);

        Assert.IsType<Ok>(result);
    }

    [Fact]
    public void Create_InvalidUsernameAndPassword_ShouldReturnsUnauthorized()
    {
        var sut = new TokensController(new FakeAuthorizeService(), new FakeTokenService());
        var model = new LoginModel { Username = "a", Password = "1234" };

        var result = sut.Create(model);

        Assert.IsType<Unauthorized>(result);
    }
}
