using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CellBaseState
{
    public abstract void EnterState(CellStateManager cell);
    public abstract void ExitState(CellStateManager cell, SwitchTypes type);
}
