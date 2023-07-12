using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlaceBombState : GridBaseState
{
    public override void EnterState(GridStateManager grid)
    {
        grid.isListen = false;
        grid.uIManager.bombPanel.SetActive(true);
    }
    public override void UpdateState(GridStateManager grid, GridActionTypes action)
    {
        if (action == GridActionTypes.PlaceBomb)
        {
            grid.CreateBomb(grid.uIManager.currentObject);
            grid.TakeCell(grid.uIManager.currentObject);
            grid.uIManager.bombPanel.SetActive(false);
            grid.SwitchState(grid.gridListeningState);
        }
        else if (action == GridActionTypes.CancelPlaceBomb)
        {
            grid.ActivateAvailableCells();
            grid.uIManager.bombPanel.SetActive(false);
            grid.SwitchState(grid.gridListeningState);
        }
    }

    public override void UITouched(GridStateManager grid, GameObject colObj)
    {

    }
}
