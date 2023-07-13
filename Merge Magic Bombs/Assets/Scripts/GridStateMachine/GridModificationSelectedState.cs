using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridModificationSelectedState : GridBaseState
{
    public GameObject selectedMod;

    public override void EnterState(GridStateManager grid)
    {
        selectedMod.GetComponent<BombModification>().modSelected = true;

        selectedMod.GetComponent<BombModification>().parentCell.GetComponent<CellScript>().isSelected = true;

        BombModification selectedModObj = selectedMod.GetComponent<BombModification>();

        foreach (GameObject bomb in grid.availableBombs)
        {
            if (bomb.GetComponent<BombController>().CheckAvailability(selectedModObj.modType, selectedModObj.modColor))
            {
                bomb.GetComponent<BombController>().parentCell.GetComponent<CellScript>().isSelected = true;
            }
        }
    }
    public override void UpdateState(GridStateManager grid, GridActionType action)
    {

    }

    public override void UITouched(GridStateManager grid, GameObject colObj)
    {
        if (colObj.tag == "Bomb")
        {
            BombModification selectedModObj = selectedMod.GetComponent<BombModification>();
            BombController selectedBomb = colObj.GetComponent<BombController>();

            if (colObj != selectedMod && selectedBomb.CheckAvailability(selectedModObj.modType, selectedModObj.modColor))
            {
                selectedModObj.parentCell.GetComponent<CellScript>().isSelected = false;

                selectedModObj.modSelected = false;
                selectedModObj.triggerMoveToPosition(colObj.transform.position);
                selectedModObj.parentCell = selectedBomb.parentCell;

                colObj.GetComponent<BombController>().AttachModification(selectedMod, selectedModObj.modType);

                selectedMod.GetComponent<BombModification>().parentCell.GetComponent<CellScript>().isSelected = false;

                foreach (GameObject bomb in grid.availableBombs)
                {
                    bomb.GetComponent<BombController>().parentCell.GetComponent<CellScript>().isSelected = false;
                }

                grid.SwitchState(grid.gridListeningState);
            }
        }
        else if (colObj.tag == "Cell")
        {
            selectedMod.GetComponent<BombModification>().modSelected = false;

            selectedMod.GetComponent<BombModification>().parentCell.GetComponent<CellScript>().isSelected = false;

            foreach (GameObject bomb in grid.availableBombs)
            {
                bomb.GetComponent<BombController>().parentCell.GetComponent<CellScript>().isSelected = false;
            }

            grid.SwitchState(grid.gridListeningState);
        }
    }
}
