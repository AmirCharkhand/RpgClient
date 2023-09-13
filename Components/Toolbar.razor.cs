using Microsoft.AspNetCore.Components;

namespace RPGClient.Components;

public partial class Toolbar
{
    private bool _addBtnDisabled = false;
    private bool _editBtnDisabled = true;
    private bool _removeBtnDisabled = true;
    
    [Parameter] public EventCallback OnAddBtnClicked { get; set; }
    [Parameter] public EventCallback OnEditBtnClicked { get; set; }
    [Parameter] public EventCallback OnRemoveBtnClicked { get; set; }

    private async Task OnAddButtonClicked() => await OnAddBtnClicked.InvokeAsync();
    private async Task OnEditButtonClicked() => await OnEditBtnClicked.InvokeAsync();
    private async Task OnRemoveButtonClicked() => await OnRemoveBtnClicked.InvokeAsync();

    public void ChangeButtonsState(int tablesSelectedRowsCount)
    {
        switch (tablesSelectedRowsCount)
        {
            case 0:
                _addBtnDisabled = false;
                _editBtnDisabled = true;
                _removeBtnDisabled = true;
                break;
            case 1:
                _addBtnDisabled = false;
                _editBtnDisabled = false;
                _removeBtnDisabled = false;
                break;
            case > 1:
                _addBtnDisabled = false;
                _editBtnDisabled = true;
                _removeBtnDisabled = false;
                break;
        }
        StateHasChanged();
    }
}