using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Camera _mainCam;
    private GridManager _gridManager;

    public GameObject bombPanel;

    public GameObject _currentObject;

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
        if (Physics.Raycast(ray, out hit) && bombPanel.activeSelf == false)
        {
            Collider collider = hit.collider;
            _currentObject = collider.gameObject;

            if (collider.tag == "Cell")
            {
                _gridManager.SelectAvailableCell(_currentObject);
            }
        }
    }

    public void ConfirmRandomBomb()
    {
        _gridManager.CreateBomb(_currentObject.transform.position);
        _gridManager.TakeCell(_currentObject);
        bombPanel.SetActive(false);
    }

    public void RejectRandomBomb()
    {
        _gridManager.ActivateAvailableCells();
        bombPanel.SetActive(false);
    }
}
