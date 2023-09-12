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
    private MudTable<GetCharacterDto> _table = null!;

    [Inject] private ICharacterService Service { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Parameter] public EventCallback<int> OnSelectedRowsChanged { get; set; }

    private async Task<TableData<GetCharacterDto>> GetServerData(TableState state)
    {
        var tableMeta = new TableMetaData()
        {
            SortingPropName = string.IsNullOrEmpty(state.SortLabel) ? "Id" : state.SortLabel,
            Ascending = state.SortDirection != SortDirection.Descending,
            PageIndex = state.Page,
            PageSize = state.PageSize
        };
        
        TableData<GetCharacterDto> tableData;
        try
        {
            if (string.IsNullOrEmpty(_searchText))
                tableData = await Service.GetCharacters(tableMeta);
            else
                tableData = await Service.SearchCharacters(tableMeta, _searchText);
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Warning);
            tableData = new TableData<GetCharacterDto>() { TotalItems = 0 };
        }

        return tableData;
    }

    private async Task OnSearch(string searchText)
    {
        _searchText = searchText;
        await _table.ReloadServerData();
    }

    private async void OnSelectedItemsChanged(HashSet<GetCharacterDto> selectedItems)
    {
        var itemsCount = selectedItems.Count;
        await OnSelectedRowsChanged.InvokeAsync(itemsCount);
    }

    public async Task ReloadTable() => await _table.ReloadServerData();
}