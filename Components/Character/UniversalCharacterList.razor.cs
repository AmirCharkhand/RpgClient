using Microsoft.AspNetCore.Components;
using MudBlazor;
using RPGClient.Components.Skills;
using RPGClient.Components.Weapon;
using RPGClient.Extensions;
using RPGClient.Models;
using RPGClient.Models.Character;
using RPGClient.Services.Contracts;

namespace RPGClient.Components.Character;

public partial class UniversalCharacterList
{
        private string _searchText = string.Empty;
    private MudTable<GetUniversalCharacterDto> _table = null!;
    private readonly DialogOptions _options = new ()
    {
        CloseOnEscapeKey = true,
        FullWidth = true,
        MaxWidth = MaxWidth.Small
    };

    [Inject] private ICharacterService Service { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Parameter] public EventCallback<GetUniversalCharacterDto> OnSelectedRowChange { get; set; }

    private async Task<TableData<GetUniversalCharacterDto>> GetServerData(TableState state)
    {
        var pagedListParameters = new PagedListParameters()
        {
            SortingPropName = state.SortLabel,
            SearchText = _searchText,
            Ascending = state.SortDirection != SortDirection.Descending,
            PageIndex = state.Page + 1,
            PageSize = state.PageSize
        };
        
        TableData<GetUniversalCharacterDto> tableData;
        try
        {
            tableData = await Service.GetUniversalCharacters(pagedListParameters);
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Warning);
            tableData = new TableData<GetUniversalCharacterDto>() { TotalItems = 0 };
        }

        return tableData;
    }

    private async Task OnSearch(string searchText)
    {
        _searchText = searchText;
        await _table.ReloadServerData();
    }

    private async void OnRowClick(GetUniversalCharacterDto selectedItem) => await OnSelectedRowChange.InvokeAsync(selectedItem);
    
    private async Task ShowWeaponDetails(GetUniversalCharacterDto ownedCharacter)
    {
        var parameters = new DialogParameters()
        {
            { "CharacterName", ownedCharacter.Name },
            { "Weapon", ownedCharacter.Weapon }
        };

        await DialogService.ShowAsync<ShowWeapon>("Weapon", parameters, _options);
    }

    private async Task AddNewWeapon(GetUniversalCharacterDto ownedCharacter)
    {
        var parameters = new DialogParameters() { { "CharacterId", ownedCharacter.Id } };
        await DialogService.ShowAsync<AddWeapon>("Add new Weapon", parameters, _options, () => _table.ReloadServerData());
    }

    private async Task ShowSkills(GetUniversalCharacterDto ownedCharacter)
    {
        var parameters = new DialogParameters()
        {
            { "CharacterName", ownedCharacter.Name },
            { "CharacterId", ownedCharacter.Id }
        };

        await DialogService.ShowAsync<CharacterSkills>("Skills", parameters, _options);
    }
}