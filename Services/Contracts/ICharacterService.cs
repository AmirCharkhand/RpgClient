using MudBlazor;
using RPGClient.Models;
using RPGClient.Models.Character;

namespace RPGClient.Services.Contracts;

public interface ICharacterService
{
    Task<TableData<GetCharacterDto>> GetCharacters(TableMetaData tableMetaData);
    Task<TableData<GetCharacterDto>> SearchCharacters(TableMetaData tableMetaData, string searchText);
    Task AddCharacter(AddCharacterDto characterDto);
    Task UpdateCharacter(UpdateCharacterDto characterDto, int characterId);
    Task DeleteCharacters(List<int> ids);
}