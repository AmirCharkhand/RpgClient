using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using RPGClient.Models;
using RPGClient.Services.Contracts;

namespace RPGClient.Components;

public partial class Login
{
    private string _userName = string.Empty;
    private string _password = string.Empty;
    private InputType _passwordInput = InputType.Password;
    private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
    private bool _isLoginDisabled = false;
    private bool _isProgressDisabled = true;

    [Inject]
    private IAuthenticationService Service { get; set; } = null!;
    [Inject]
    private ISessionStorageService SessionStorage { get; set; } = null!;
    [Inject]
    private ISnackbar Snackbar { get; set; } = null!;


    [CascadingParameter] public MudDialogInstance Instance { get; set; } = null!;

    private void TogglePasswordVisibility()
    {
        if (_passwordInput == InputType.Password)
        {
            _passwordInput = InputType.Text;
            _passwordInputIcon = Icons.Material.Filled.Visibility;
        }
        else
        {
            _passwordInput = InputType.Password;
            _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
        }
    }

    private async Task LoginUser()
    {
        _isLoginDisabled = true;
        _isProgressDisabled = false;
        
        try
        {
            var userLogin = new UserLoginDto() { UserName = _userName, Password = _password };
            await Service.Login(userLogin);
            Snackbar.Add("Successfully Logged in", Severity.Success);
            Instance.Close();
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
            Instance.Cancel();
        }
    }

    private async Task OnEnter(KeyboardEventArgs e)
    {
        if (e.Code.Equals("Enter")) await LoginUser();
    }

    private void CloseDialog() => Instance.Cancel();
}