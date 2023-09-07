﻿using Microsoft.AspNetCore.Components;
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

    [Inject]
    private ILoginService Service { get; set; } = null!;
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
        try
        {
            _isLoginDisabled = true;
            var userLogin = new UserLoginDto() { UserName = _userName, Password = _password };
            var jwt = await Service.Login(userLogin);
            await SessionStorage.AddItem("UserToken", jwt);
            Snackbar.Add("Successfully Logged in", Severity.Success);
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Warning);
        }
        
        _isLoginDisabled = false;
        Instance.Close();
    }

    private void CloseDialog() => Instance.Cancel();
}