using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using MudBlazor;
using RPGClient.Extensions;
using RPGClient.Models.Weapon;
using RPGClient.Services.Contracts;

namespace RPGClient.Components.Weapon;

public partial class AddWeapon
{
    [CascadingParameter] public MudDialogInstance Instance { get; set; } = null!;
    [Parameter] public int CharacterId { get; set; }
    [Inject] private IWeaponService Service { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    private readonly AddWeaponDto _model = new();
    private EditContext _context = null!;
    private bool _isSubmitDisabled = false;

    protected override void OnInitialized()
    {
        _model.CharacterRef = CharacterId;
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
            await Service.AddWeapon(_model);
            Snackbar.Add("Weapon Successfully added", Severity.Success);
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
        await e.DoFuncIfEnter(JsRuntime, Submit);
    }
}