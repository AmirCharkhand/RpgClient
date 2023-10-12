namespace RPGClient.Services.Contracts;

public interface IFightService
{
    public Task<List<string>> Fight(List<int> ids);
}