using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellTakenSelectedState : CellBaseState
{
    public override void EnterState(CellStateManager cell)
    {
        cell.isHighlighted = false;
        cell.isSelected = true;
        cell.isTaken = true;

        cell.highlightObject.SetActive(true);
    }
    public override void ExitState(CellStateManager cell, SwitchTypes type)
    {
        if (type == SwitchTypes.NotHighlight)
            cell.SwitchState(cell.cellTakenState);
        else if (type == SwitchTypes.Untake)
            cell.SwitchState(cell.cellAvailableState);
    }
}
