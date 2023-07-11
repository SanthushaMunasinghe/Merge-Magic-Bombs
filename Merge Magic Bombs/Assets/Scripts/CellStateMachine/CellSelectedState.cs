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
        cell.uIManager.bombPanel.SetActive(true);
    }
    public override void ExitState(CellStateManager cell, SwitchTypes type)
    {
        if (type == SwitchTypes.NotHighlight)
        {
            cell.uIManager.bombPanel.SetActive(true);
            cell.SwitchState(cell.cellNotHighlightedState);
        }
    }
}
