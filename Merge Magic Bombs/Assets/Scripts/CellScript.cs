using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    //State Output
    public bool isActive = false;
    public bool isSelected = false;

    //Tile Data
    public GameObject highlightObject;

    void Start()
    {
    }

    void Update()
    {
        if (isActive)
        {
            if (isSelected)
                highlightObject.SetActive(true);
            else
                highlightObject.SetActive(false);
        }
    }
}
