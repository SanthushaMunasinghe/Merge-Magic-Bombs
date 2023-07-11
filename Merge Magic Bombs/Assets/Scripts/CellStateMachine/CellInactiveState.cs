using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellInactiveState : CellBaseState
{
    public override void EnterState(CellStateManager cell)
    {
        cell.isActive = false;
        cell.isHighlighted = false;
        cell.isSelected = false;
        cell.isTaken = false;
    }
    public override void ExitState(CellStateManager cell, SwitchTypes type)
    {
        if (type == SwitchTypes.Highlight)
        {
            cell.isActive = true;
            cell.SwitchState(cell.cellHighlightedState);
        }
    }
}
