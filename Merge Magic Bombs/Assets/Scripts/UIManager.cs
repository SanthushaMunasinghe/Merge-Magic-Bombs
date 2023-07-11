using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Camera _mainCam;
    private GridManager _gridManager;

    public GameObject bombPanel;

    private GameObject _currentObject;

    private void Awake()
    {
        _mainCam = Camera.main;
        _gridManager = GetComponent<GridManager>();
    }

    private void Update()
    {

    }

    public void UITouched(Vector2 pos)
    {
        Ray ray = _mainCam.ScreenPointToRay(pos);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Collider collider = hit.collider;
            _currentObject = collider.gameObject;

            if (collider.tag == "Cell")
            {
                if (bombPanel.activeSelf == false)
                {
                    _gridManager.SelectAvailableCell(_currentObject);
                }
            }
        }
    }

    public void ConfirmRandomBomb()
    {

    }

    public void RejectRandomBomb()
    {
        _gridManager.ActivateAvailableCells();
        bombPanel.SetActive(false);
    }
}
