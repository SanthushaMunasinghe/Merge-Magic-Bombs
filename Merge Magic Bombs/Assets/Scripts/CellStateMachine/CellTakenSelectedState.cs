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
        Debug.Log("TakenSelected");
    }
    public override void ExitState(CellStateManager cell, SwitchTypes type)
    {
        if (type == SwitchTypes.NotHighlight)
            cell.SwitchState(cell.cellTakenState);
        else if (type == SwitchTypes.Highlight)
            cell.SwitchState(cell.cellHighlightedState);
    }
}
