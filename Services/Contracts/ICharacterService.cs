using MudBlazor;
using RPGClient.Models;
using RPGClient.Models.Character;
using RPGClient.Models.Skill;

namespace RPGClient.Services.Contracts;

public interface ICharacterService
{
    Task<TableData<GetCharacterDto>> GetCharacters(PagedListParameters parameters);
    Task AddCharacter(AddCharacterDto characterDto);
    Task UpdateCharacter(UpdateCharacterDto characterDto, int characterId);
    Task DeleteCharacters(List<int> ids);
    Task<List<GetSkillDto>> AddCharacterSkill(CharacterSkillDto characterSkill);
    Task<List<GetSkillDto>> RemoveCharacterSkill(CharacterSkillDto characterSkill);
}