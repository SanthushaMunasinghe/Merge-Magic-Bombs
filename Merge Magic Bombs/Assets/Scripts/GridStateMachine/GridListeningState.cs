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
        if (colObj.tag == "Cell" && colObj.GetComponent<CellScript>().isActive)
        {
            grid.SwitchState(grid.gridPlaceBombState);
        }
        else if (colObj.tag == "Bomb")
        {
            grid.gridBombSelectedState.selectedBomb = colObj;
            grid.SwitchState(grid.gridBombSelectedState);
        }
    }
}
