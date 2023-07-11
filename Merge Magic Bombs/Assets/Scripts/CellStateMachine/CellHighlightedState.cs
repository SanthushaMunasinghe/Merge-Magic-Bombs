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
    }

    public override void ExitState(CellStateManager cell, SwitchTypes type)
    {
        if (type == SwitchTypes.Select)
            cell.SwitchState(cell.cellSelectedState);
        else if (type == SwitchTypes.Take)
            cell.SwitchState(cell.cellTakenState);
    }
}
