using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellTakenHighlightedState : CellBaseState
{
    public override void EnterState(CellStateManager cell)
    {
        cell.isHighlighted = true;
        cell.isSelected = false;
        cell.isTaken = true;

        cell.highlightObject.SetActive(true);
    }
    public override void ExitState(CellStateManager cell, SwitchTypes type)
    {
        if (type == SwitchTypes.NotHighlight)
            cell.SwitchState(cell.cellTakenState);
    }
}
