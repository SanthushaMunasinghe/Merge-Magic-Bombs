using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSelectedState : CellBaseState
{
    public override void EnterState(CellStateManager cell)
    {
        cell.isHighlighted = false;
        cell.isSelected = true;
        cell.isTaken = false;
    }
    public override void ExitState(CellStateManager cell, SwitchTypes type)
    {
        if (type == SwitchTypes.Highlight)
            cell.SwitchState(cell.cellHighlightedState);
    }
}
