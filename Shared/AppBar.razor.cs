using Microsoft.AspNetCore.Components;
using MudBlazor;
using RPGClient.Components;
using RPGClient.Extensions;
using RPGClient.Services.Contracts;

namespace RPGClient.Shared;

public partial class AppBar
{
    [Parameter] public EventCallback<bool> OnThemeChanged { get; set; }
    [Parameter] public EventCallback OnNavButtonClick { get; set; }
    [Inject] private ISessionStorageService StorageService { get; set; } = null!;
    [Inject] private IAuthenticationService AuthenticationService { get; set; } = null!;

    private string? _userName;
    private bool _isUserLoggedIn = false;
    private bool _isDarkMode = false;

    private async void OnToggleTheme()
    {
        _isDarkMode = !_isDarkMode;
        await OnThemeChanged.InvokeAsync(_isDarkMode);
    }

    private async void OpenNavBar() => await OnNavButtonClick.InvokeAsync();

    private string ThemeIcon() => _isDarkMode ? Icons.Material.Rounded.DarkMode : Icons.Material.Rounded.LightMode;

    protected override async Task OnInitializedAsync()
    {
        await SetLoginVariables();
    }

    private async void OpenLoginDialog()
    {
        var options = new DialogOptions()
        {
            CloseOnEscapeKey = true, 
            Position = DialogPosition.Center, 
            MaxWidth = MaxWidth.Small,
            FullWidth = true
        };
        await DialogService.ShowAsync<Login>("Login", options, SetLoginVariables);
    }

    private async Task SetLoginVariables()
    {
        _userName = await StorageService.GetItem<string>("UserName");
        if (_userName == null)
        {
            ResetLoginVariables();
            return;
        }
            
        _isUserLoggedIn = true;
        StateHasChanged();
    }

    private void ResetLoginVariables()
    {
        _userName = string.Empty;
        _isUserLoggedIn = false;
        StateHasChanged();
    }

    private async Task Logout()
    {
        await AuthenticationService.Logout();
        ResetLoginVariables();
    }
}