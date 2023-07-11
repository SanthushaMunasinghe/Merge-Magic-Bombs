using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    public bool isAvailable = false;
    public bool isHighlighted = false;
    public bool isSelected = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SwitchHighlightedCell()
    {
        if (isAvailable)
            isHighlighted = !isHighlighted;
    }

    public void SwitchSelectedCell()
    {
        if (isAvailable)
            isSelected = !isSelected;
    }

    private void UpdateCellState()
    {
        
    }
}
