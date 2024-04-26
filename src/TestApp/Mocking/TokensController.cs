using System;

namespace TestApp.Mocking
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }        
    }

    public interface IAuthorizeService
    {
        bool Authorize(string username, string password);
    }

    public interface ITokenService
    {
        string CreateToken();
    }

    public class TokensController : BaseController
    {
        IAuthorizeService authorizeService;
        ITokenService tokenService;

        public TokensController(IAuthorizeService authorizeService, ITokenService tokenService)
        {
            this.authorizeService = authorizeService;
            this.tokenService = tokenService;
        }

        public ActionResult Create(LoginModel model)
        {
            if (authorizeService.Authorize(model.Username, model.Password))
            {
                var token = tokenService.CreateToken();

                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }

    public class BaseController
    {
        public ActionResult Ok(object? value) => new Ok(value);
        public ActionResult Unauthorized() => new Unauthorized();
    }

    public abstract class ActionResult { }
    public class Ok(object? value) : ActionResult { }
    public class Unauthorized : ActionResult { }
    
}
