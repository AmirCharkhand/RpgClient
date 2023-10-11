using Microsoft.AspNetCore.Components;
using MudBlazor;
using RPGClient.Models.Character;
using RPGClient.Services.Contracts;

namespace RPGClient.Components.Character;

public partial class RemoveCharacter
{
    [Parameter] public GetOwnedCharacterDto[] Characters { get; set; } = null!;
    [CascadingParameter] public MudDialogInstance Instance { get; set; } = null!;
    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [Inject] public ICharacterService Service { get; set; } = null!;

    private bool _isDeleteDisabled = false;

    private void Cancel() => Instance.Cancel();

    private async Task DeleteCharacters()
    {
        _isDeleteDisabled = true;
        
        try
        {
            var ids = Characters.Select(c => c.Id).ToList();
            await Service.DeleteCharacters(ids);
            Snackbar.Add("Characters Successfully Deleted", Severity.Success);
            Instance.Close();
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }

        _isDeleteDisabled = false;
    }
}