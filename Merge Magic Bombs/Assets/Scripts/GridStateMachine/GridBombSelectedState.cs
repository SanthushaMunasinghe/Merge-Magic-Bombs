using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBombSelectedState : GridBaseState
{
    public GameObject selectedBomb;

    public override void EnterState(GridStateManager grid)
    {
        foreach (GameObject bomb in grid.availableBombs)
        {
            grid.HighlightCells(bomb.GetComponent<BombController>().parentCell);
        }
    }
    public override void UpdateState(GridStateManager grid, GridActionTypes action)
    {

    }

    public override void UITouched(GridStateManager grid, GameObject colObj)
    {
        if (colObj.tag == "Bomb")
        {
            if (colObj != selectedBomb)
            {
                selectedBomb.GetComponent<BombController>().truggerMoveToPosition(colObj.transform.position);
                colObj.GetComponent<BombController>().truggerMoveToPosition(selectedBomb.transform.position);
                grid.ActivateAvailableCells();
                grid.SwitchState(grid.gridListeningState);
            }
        }
        else
        {
            grid.ActivateAvailableCells();
            grid.SwitchState(grid.gridListeningState);
        }
    }
}
