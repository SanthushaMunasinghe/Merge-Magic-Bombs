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

        cell.highlightObject.SetActive(true);
    }
    public override void ExitState(CellStateManager cell, SwitchTypes type)
    {
        if (type == SwitchTypes.NotHighlight)
            cell.SwitchState(cell.cellNotHighlightedState);
        else if (type == SwitchTypes.Take)
            cell.SwitchState(cell.cellTakenState);
    }
}
