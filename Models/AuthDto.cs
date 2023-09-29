namespace RPGClient.Models;

public class AuthDto
{
    public string UserName { get; set; } = null!;
    public string Jwt { get; set; } = null!;
}