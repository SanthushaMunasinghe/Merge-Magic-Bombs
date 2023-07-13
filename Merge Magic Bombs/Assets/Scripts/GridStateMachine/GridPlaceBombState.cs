using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlaceBombState : GridBaseState
{
    public override void EnterState(GridStateManager grid)
    {
        grid.isListen = false;
        grid.uIManager.currentObject.GetComponent<CellScript>().isSelected = true;
        grid.uIManager.bombPanel.SetActive(true);
    }
    public override void UpdateState(GridStateManager grid, GridActionType action)
    {
        if (action == GridActionType.PlaceBomb)
        {
            grid.CreateBomb(grid.uIManager.currentObject);
            grid.uIManager.bombPanel.SetActive(false);
            grid.uIManager.currentObject.GetComponent<CellScript>().isSelected = false;
            grid.SwitchState(grid.gridListeningState);
        }
        else if (action == GridActionType.CancelPlaceBomb)
        {
            grid.uIManager.bombPanel.SetActive(false);
            grid.uIManager.currentObject.GetComponent<CellScript>().isSelected = false;
            grid.SwitchState(grid.gridListeningState);
        }
    }

    public override void UITouched(GridStateManager grid, GameObject colObj)
    {

    }

    public override void ExitState(GridStateManager grid)
    {
        grid.gridNotListeningState.currentState = this;
        grid.SwitchState(grid.gridNotListeningState);
    }
}
