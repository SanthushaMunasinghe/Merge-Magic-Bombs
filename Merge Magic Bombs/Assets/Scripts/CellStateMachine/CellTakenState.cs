using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellTakenState : CellBaseState
{
    public override void EnterState(CellStateManager cell)
    {
        cell.isHighlighted = false;
        cell.isSelected = false;
        cell.isTaken = true;

        cell.highlightObject.SetActive(false);
    }
    public override void ExitState(CellStateManager cell, SwitchTypes type)
    {
        if (type == SwitchTypes.Select)
            cell.SwitchState(cell.cellTakenSelectedState);
        else if (type == SwitchTypes.Highlight)
            cell.SwitchState(cell.cellTakenHighlightedState);
    }
}
