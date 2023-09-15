using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using RPGClient.Models.Character;
using RPGClient.Services.Contracts;

namespace RPGClient.Components.Character;

public partial class AddCharacter
{
    [CascadingParameter] public MudDialogInstance Instance { get; set; } = null!;
    [Inject] private ICharacterService Service { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    private readonly AddCharacterDto _model = new AddCharacterDto();
    private EditContext _context = null!;
    private bool _isSubmitDisabled = false;

    protected override void OnInitialized()
    {
        _context = new EditContext(_model);
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
            await Service.AddCharacter(_model);
            Snackbar.Add("Character Successfully added", Severity.Success);
            Instance.Close();
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
            _isSubmitDisabled = false;
        }
    }
}