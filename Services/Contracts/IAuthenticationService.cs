using RPGClient.Models;

namespace RPGClient.Services.Contracts;

public interface IAuthenticationService
{
    Task Login(UserLoginDto userLogin);
    Task Authenticate();
}