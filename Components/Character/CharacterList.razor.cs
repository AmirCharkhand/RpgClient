using Microsoft.AspNetCore.Components;
using MudBlazor;
using RPGClient.Models;
using RPGClient.Models.Character;
using RPGClient.Models.Weapon;
using RPGClient.Services.Contracts;

namespace RPGClient.Components.Character;

public partial class CharacterList
{
    private string _searchText = string.Empty;
    private MudTable<GetCharacterDto> _table;

    [Inject]
    private ICharacterService Service { get; set; } = null!;
    
    private async Task<TableData<GetCharacterDto>> GetServerData(TableState state)
    {
        var tableMeta = new TableMetaData()
        {
            SortingPropName = string.IsNullOrEmpty(state.SortLabel) ? "Id" : state.SortLabel,
            Ascending = state.SortDirection != SortDirection.Descending,
            PageIndex = state.Page,
            PageSize = state.PageSize
        };
        return await Service.GetCharacters(tableMeta);
    }

    private async Task OnSearch(string searchText)
    {
        _searchText = searchText;
        await _table.ReloadServerData();
    }
}