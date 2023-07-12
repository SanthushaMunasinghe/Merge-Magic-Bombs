using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridListeningState : GridBaseState
{
    public override void EnterState(GridStateManager grid)
    {
        grid.isListen = true;
    }
    public override void UpdateState(GridStateManager grid, GridActionTypes action)
    {
        
    }

    public override void UITouched(GridStateManager grid, GameObject colObj)
    {
        if (colObj.tag == "Cell")
        {
            grid.SelectAvailableCell(colObj);
            grid.SwitchState(grid.gridPlaceBombState);
        }
        else if (colObj.tag == "Bomb")
        {
            grid.SelectAvailableCell(colObj.GetComponent<BombController>().parentCell);
            grid.gridBombSelectedState.selectedBomb = colObj;
            grid.SwitchState(grid.gridBombSelectedState);
        }
    }
}
