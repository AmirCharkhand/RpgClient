using RPGClient.Models;

namespace RPGClient.Services.Contracts;

public interface ILoginService
{
    Task<string> Login(UserLoginDto userLogin);
}