using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellStateManager : MonoBehaviour
{
    //State Output
    public bool isActive = false;
    public bool isHighlighted = false;
    public bool isSelected = false;
    public bool isTaken = false;

    //State Data
    CellBaseState currentState;

    public CellInactiveState cellInactiveState = new CellInactiveState();
    public CellHighlightedState cellHighlightedState = new CellHighlightedState();
    public CellNotHighlightedState cellNotHighlightedState = new CellNotHighlightedState();
    public CellSelectedState cellSelectedState = new CellSelectedState();
    public CellTakenState cellTakenState = new CellTakenState();
    public CellTakenSelectedState cellTakenSelectedState = new CellTakenSelectedState();
    public CellTakenHighlightedState cellTakenHighlightedState = new CellTakenHighlightedState();

    void Start()
    {
        currentState = cellInactiveState;
        currentState.EnterState(this);
    }

    void Update()
    {
        
    }

    public void SwitchState(CellBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    //Update States
    public void ExitState(SwitchTypes type)
    {
        currentState.ExitState(this, type);
    }
}

public enum SwitchTypes
{
    Highlight,
    NotHighlight,
    Select,
    Take,
    TakeSelect,
    TakeHighlight,
}
