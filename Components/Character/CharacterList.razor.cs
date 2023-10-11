using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Extensions;
using RPGClient.Components.Skills;
using RPGClient.Components.Weapon;
using RPGClient.Extensions;
using RPGClient.Models;
using RPGClient.Models.Character;
using RPGClient.Models.Weapon;
using RPGClient.Services.Contracts;

namespace RPGClient.Components.Character;

public partial class CharacterList
{
    private string _searchText = string.Empty;
    private MudTable<GetOwnedCharacterDto> _table = null!;
    private DialogOptions _options = new ()
    {
        CloseOnEscapeKey = true,
        FullWidth = true,
        MaxWidth = MaxWidth.Small
    };

    [Inject] private ICharacterService Service { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Parameter] public EventCallback<GetOwnedCharacterDto[]> OnSelectedRowsChanged { get; set; }

    private async Task<TableData<GetOwnedCharacterDto>> GetServerData(TableState state)
    {
        var pagedListParameters = new PagedListParameters()
        {
            SortingPropName = state.SortLabel,
            SearchText = _searchText,
            Ascending = state.SortDirection != SortDirection.Descending,
            PageIndex = state.Page + 1,
            PageSize = state.PageSize
        };
        
        TableData<GetOwnedCharacterDto> tableData;
        try
        {
            tableData = await Service.GetOwnedCharacters(pagedListParameters);
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Warning);
            tableData = new TableData<GetOwnedCharacterDto>() { TotalItems = 0 };
        }

        return tableData;
    }

    private async Task OnSearch(string searchText)
    {
        _searchText = searchText;
        await _table.ReloadServerData();
    }

    private async void OnSelectedItemsChanged(HashSet<GetOwnedCharacterDto> selectedItems)
    {
        var selectedItemsList = selectedItems.ToArray();
        await OnSelectedRowsChanged.InvokeAsync(selectedItemsList);
    }

    public async Task ReloadTable() => await _table.ReloadServerData();

    private async Task ShowWeaponDetails(GetOwnedCharacterDto ownedCharacter)
    {
        var parameters = new DialogParameters()
        {
            { "CharacterName", ownedCharacter.Name },
            { "Weapon", ownedCharacter.Weapon }
        };

        await DialogService.ShowAsync<ShowWeapon>("Weapon", parameters, _options);
    }

    private async Task AddNewWeapon(GetOwnedCharacterDto ownedCharacter)
    {
        var parameters = new DialogParameters() { { "CharacterId", ownedCharacter.Id } };
        await DialogService.ShowAsync<AddWeapon>("Add new Weapon", parameters, _options, () => _table.ReloadServerData());
    }

    private async Task ShowSkills(GetOwnedCharacterDto ownedCharacter)
    {
        var parameters = new DialogParameters()
        {
            { "CharacterName", ownedCharacter.Name },
            { "CharacterId", ownedCharacter.Id }
        };

        await DialogService.ShowAsync<CharacterSkills>("Skills", parameters, _options);
    }
}