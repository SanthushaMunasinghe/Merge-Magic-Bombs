using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridBaseState
{
    public abstract void EnterState(GridStateManager grid);
    public abstract void UpdateState(GridStateManager grid, GridActionTypes action);
    public abstract void UITouched(GridStateManager grid, GameObject colObj);
}
