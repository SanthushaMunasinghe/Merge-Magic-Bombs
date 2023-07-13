using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNotListeningState : GridBaseState
{
    public GridBaseState currentState;

    public override void EnterState(GridStateManager grid)
    {
        grid.isListen = false;
    }
    public override void UpdateState(GridStateManager grid, GridActionType action)
    {
        if (action == GridActionType.Start)
            grid.SwitchState(grid.gridListeningState);
        else if (action == GridActionType.Continue)
            grid.SwitchState(currentState);
    }

    public override void UITouched(GridStateManager grid, GameObject colObj)
    {
        
    }

    public override void ExitState(GridStateManager grid)
    {
        
    }
}
