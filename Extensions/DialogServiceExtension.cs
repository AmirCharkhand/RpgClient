using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace RPGClient.Extensions;

public static class DialogServiceExtension
{
    public static async Task ShowAsync<T>(this IDialogService dialogService, string title, DialogParameters parameters, DialogOptions options, Func<Task> onClose) where T : ComponentBase
    {
        var dialog = await dialogService.ShowAsync<T>(title, parameters, options);
        var result = await dialog.Result;
        if (!result.Canceled) await onClose.Invoke();
    }
    
    public static async Task ShowAsync<T>(this IDialogService dialogService, string title, DialogOptions options, Func<Task> onClose) where T : ComponentBase
    {
        var dialog = await dialogService.ShowAsync<T>(title, options);
        var result = await dialog.Result;
        if (!result.Canceled) await onClose.Invoke();
    }
}