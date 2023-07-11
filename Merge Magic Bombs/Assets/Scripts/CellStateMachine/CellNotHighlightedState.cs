using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellNotHighlightedState : CellBaseState
{
    public override void EnterState(CellStateManager cell)
    {
        cell.isHighlighted = false;
        cell.isSelected = false;
        cell.isTaken = false;

        cell.highlightObject.SetActive(false);
    }

    public override void ExitState(CellStateManager cell, SwitchTypes type)
    {
        if (type == SwitchTypes.Select)
            cell.SwitchState(cell.cellSelectedState);
        else if (type == SwitchTypes.Take)
            cell.SwitchState(cell.cellTakenState);
    }
}
