using Microsoft.AspNetCore.Components;
using MudBlazor;
using RPGClient.Components;

namespace RPGClient.Shared;

public partial class AppBar
{
    [Parameter] public EventCallback<bool> OnThemeChanged { get; set; }
    [Parameter] public EventCallback OnNavButtonClick { get; set; }
    private bool _isDarkMode = false;

    private async void OnToggleTheme()
    {
        _isDarkMode = !_isDarkMode;
        await OnThemeChanged.InvokeAsync(_isDarkMode);
    }

    private async void OpenNavBar() => await OnNavButtonClick.InvokeAsync();

    private string ThemeIcon() => _isDarkMode ? Icons.Material.Rounded.DarkMode : Icons.Material.Rounded.LightMode;

    private void OpenLoginDialog()
    {
        var options = new DialogOptions()
        {
            CloseOnEscapeKey = true, Position = DialogPosition.Center, MaxWidth = MaxWidth.Large
        };
        DialogService.Show<Login>("Login", options);
    }
}