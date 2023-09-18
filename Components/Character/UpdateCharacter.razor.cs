using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using RPGClient.Models.Character;
using RPGClient.Services.Contracts;

namespace RPGClient.Components.Character;

public partial class UpdateCharacter
{
    [Parameter] public int CharacterId { get; set; }
    [Parameter] public UpdateCharacterDto Model { get; set; } = null!;
    [CascadingParameter] public MudDialogInstance Instance { get; set; } = null!;
    [Inject] private ICharacterService Service { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    private EditContext _context = null!;
    private bool _isSubmitDisabled = false;

    protected override void OnInitialized()
    {
        _context = new EditContext(Model);
    }

    private void Cancel() => Instance.Cancel();

    private async Task Submit()
    {
        var isValid = _context.Validate();
        StateHasChanged();
        if(!isValid) return;

        try
        {
            _isSubmitDisabled = true;
            await Service.UpdateCharacter(Model, CharacterId);
            Snackbar.Add("Character Successfully Updated", Severity.Success);
            Instance.Close();
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
            _isSubmitDisabled = false;
        }
    }

    private async Task OnEnter(KeyboardEventArgs e)
    {
        if (e.Code.Equals("Enter")) await Submit();
    }
}