using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellHighlightedState : CellBaseState
{
    public override void EnterState(CellStateManager cell)
    {
        cell.isHighlighted = true;
        cell.isSelected = false;
        cell.isTaken = false;

        cell.highlightObject.SetActive(true);
    }

    public override void ExitState(CellStateManager cell, SwitchTypes type)
    {
        if (type == SwitchTypes.NotHighlight)
            cell.SwitchState(cell.cellNotHighlightedState);
    }
}
