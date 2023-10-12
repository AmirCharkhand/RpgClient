using Microsoft.AspNetCore.Components;
using MudBlazor;
using RPGClient.Services.Contracts;

namespace RPGClient.Components.Fight;

public partial class ShowFightResult
{
    [CascadingParameter] public MudDialogInstance Instance { get; set; } = null!;
    [Parameter] public List<int> Ids { get; set; } = null!;
    [Inject] private IFightService Service { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    private bool _isLoading = true;
    private List<string> _fightResult = null!;

    protected override async Task OnInitializedAsync()
    {
        await GetFightResult();
    }

    private async Task GetFightResult()
    {
        try
        {
            _fightResult = await Service.Fight(Ids);
            _isLoading = false;
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
            Instance.Cancel();
        }
    }

    private void Close() => Instance.Close();
}