using RPGClient.Models.Skill;

namespace RPGClient.Services.Contracts;

public interface ISkillService
{
    Task<List<GetSkillDto>> GetAll();
}