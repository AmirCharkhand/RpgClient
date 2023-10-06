using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace RPGClient.Extensions;

public static class EventArgsExtension
{
    public static async Task DoFuncIfEnter(this KeyboardEventArgs keyboardEventArgs, IJSRuntime jsRuntime, Func<Task> afterEnter)
    {
        if (keyboardEventArgs.Code.Equals("Enter"))
        {
            await jsRuntime.InvokeVoidAsync("document.activeElement.blur");
            await Task.Delay(200);
            await afterEnter.Invoke();
        }
    }
}