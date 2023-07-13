using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBombSelectedState : GridBaseState
{
    public GameObject selectedBomb;

    public override void EnterState(GridStateManager grid)
    {
        selectedBomb.GetComponent<BombController>().bombSelected = true;
        grid.uIManager.blastBtn.SetActive(true);

        foreach (GameObject bomb in grid.availableBombs)
        {
            bomb.GetComponent<BombController>().parentCell.GetComponent<CellScript>().isSelected = true;
        }
        
        foreach (GameObject mod in grid.availableMods)
        {
            mod.GetComponent<BombModification>().parentCell.GetComponent<CellScript>().isSelected = true;
        }
    }
    public override void UpdateState(GridStateManager grid, GridActionType action)
    {
        if (action == GridActionType.Blast)
        {
            foreach (GameObject explosion in grid.bombExplosionPrefabs)
            {
                if (explosion.GetComponent<ExplosionEffect>().expColor == selectedBomb.GetComponent<BombController>().bombColor)
                {
                    foreach (GameObject bomb in grid.availableBombs)
                    {
                        bomb.GetComponent<BombController>().parentCell.GetComponent<CellScript>().isSelected = false;
                    }

                    foreach (GameObject mod in grid.availableMods)
                    {
                        mod.GetComponent<BombModification>().parentCell.GetComponent<CellScript>().isSelected = false;
                    }

                    selectedBomb.GetComponent<BombController>().ProcessBlast(explosion);
                    grid.uIManager.blastBtn.SetActive(false);
                    grid.SwitchState(grid.gridListeningState);
                }
            }
        }
    }

    public override void UITouched(GridStateManager grid, GameObject colObj)
    {
        if (colObj.tag == "Bomb")
        {
            if (colObj != selectedBomb)
            {
                BombController selectedBombController = selectedBomb.GetComponent<BombController>();
                BombController currentBombController = colObj.GetComponent<BombController>();
                GameObject selectedParentCell = selectedBombController.parentCell;

                selectedBombController.bombSelected = false;
                grid.uIManager.blastBtn.SetActive(false);
                selectedBombController.triggerMoveToPosition(colObj.transform.position);
                selectedBombController.parentCell = currentBombController.parentCell;

                currentBombController.triggerMoveToPosition(selectedBomb.transform.position);
                currentBombController.parentCell = selectedParentCell;

                foreach (GameObject bomb in grid.availableBombs)
                {
                    bomb.GetComponent<BombController>().parentCell.GetComponent<CellScript>().isSelected = false;
                }

                foreach (GameObject mod in grid.availableMods)
                {
                    mod.GetComponent<BombModification>().parentCell.GetComponent<CellScript>().isSelected = false;
                }

                grid.SwitchState(grid.gridListeningState);
            }
        }
        else if (colObj.tag == "Mod")
        {
            BombController selectedBombController = selectedBomb.GetComponent<BombController>();
            BombModification currentBombMod = colObj.GetComponent<BombModification>();
            GameObject selectedParentCell = selectedBombController.parentCell;

            selectedBombController.bombSelected = false;
            grid.uIManager.blastBtn.SetActive(false);
            selectedBombController.triggerMoveToPosition(colObj.transform.position);
            selectedBombController.parentCell = currentBombMod.parentCell;

            currentBombMod.triggerMoveToPosition(selectedBomb.transform.position);
            currentBombMod.parentCell = selectedParentCell;

            foreach (GameObject bomb in grid.availableBombs)
            {
                bomb.GetComponent<BombController>().parentCell.GetComponent<CellScript>().isSelected = false;
            }

            foreach (GameObject mod in grid.availableMods)
            {
                mod.GetComponent<BombModification>().parentCell.GetComponent<CellScript>().isSelected = false;
            }

            grid.SwitchState(grid.gridListeningState);
        }
        else if (colObj.tag == "Cell")
        {
            selectedBomb.GetComponent<BombController>().bombSelected = false;
            grid.uIManager.blastBtn.SetActive(false);

            foreach (GameObject bomb in grid.availableBombs)
            {
                bomb.GetComponent<BombController>().parentCell.GetComponent<CellScript>().isSelected = false;
            }

            foreach (GameObject mod in grid.availableMods)
            {
                mod.GetComponent<BombModification>().parentCell.GetComponent<CellScript>().isSelected = false;
            }

            grid.SwitchState(grid.gridListeningState);
        }
    }

    public override void ExitState(GridStateManager grid)
    {
        grid.gridNotListeningState.currentState = this;
        grid.SwitchState(grid.gridNotListeningState);
    }
}
