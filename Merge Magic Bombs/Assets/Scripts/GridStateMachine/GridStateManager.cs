using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridStateManager : MonoBehaviour
{
    private GameplayManager gameplayManager;
    public UIManager uIManager;

    //Cube Data
    private int _cubeTypeUnlockValue = 3;
    private int _cubeStrengthRange = 2;

    [Header("Cube Data")]
    [SerializeField] private GameObject[] _cubePrefabs;
    [SerializeField] private int _maxCubeStrengthRange;

    [Header("Bomb Data")]
    [SerializeField] private GameObject[] _bombPrefabs;
    public GameObject[] bombExplosionPrefabs;
    public List<GameObject> availableBombs = new List<GameObject>();

    //Grid Data
    [Header("Grid Data")]
    [SerializeField] private Transform _cellParent;
    public List<GameObject> initialCells = new List<GameObject>();
    public List<GameObject> availableCells = new List<GameObject>();
    public GameObject selectedCell;
    public bool isListen = false;

    //State Data
    GridBaseState currentState;

    public GridListeningState gridListeningState = new GridListeningState();
    public GridPlaceBombState gridPlaceBombState = new GridPlaceBombState();
    public GridBombSelectedState gridBombSelectedState = new GridBombSelectedState();
    public GridModificationSelectedState gridModificationSelectedState = new GridModificationSelectedState();

    void Start()
    {
        gameplayManager = GetComponent<GameplayManager>();
        uIManager = GetComponent<UIManager>();

        UnlockCubeValues();
        GenerateGrid();

        currentState = gridListeningState;
        currentState.EnterState(this);
    }

    void Update()
    {
        
    }

    public void SwitchState(GridBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void UITouched(Collider col)
    {
        currentState.UITouched(this, col.gameObject);
    }

    public void UpdateState(GridActionTypes type)
    {
        currentState.UpdateState(this, type);
    }

    private void UnlockCubeValues()
    {
        int currentUnlockValue = gameplayManager.levelIndex - 1;

        for (int i = 1; i < currentUnlockValue; i++)
        {
            //Unlock Type
            if (_cubeTypeUnlockValue < _cubePrefabs.Length)
                _cubeTypeUnlockValue++;

            //Unlock Strength
            if (_cubeStrengthRange <= _maxCubeStrengthRange)
                _cubeStrengthRange++;
        }
    }

    private void GenerateGrid()
    {
        SetInitialCells();

        //Generate Cubes
        foreach (GameObject posObj in initialCells)
        {
            int randCubeIndex = Random.Range(0, _cubeTypeUnlockValue);
            Vector3 spawnPos = new Vector3(posObj.transform.position.x, posObj.transform.position.y + 0.25f, posObj.transform.position.z);

            GameObject cubeClone = Instantiate(_cubePrefabs[randCubeIndex], spawnPos, Quaternion.identity);
            cubeClone.GetComponent<CubeObject>().cubeStrength = Random.Range(1, _cubeStrengthRange + 1);
            cubeClone.GetComponent<CubeObject>().gridStateManager = this;
        }
    }

    private void SetInitialCells()
    {
        for (int i = 0; i < _cellParent.childCount; i++)
        {
            if (i < 5)
                availableCells.Add(_cellParent.GetChild(i).gameObject);
            else
                initialCells.Add(_cellParent.GetChild(i).gameObject);
        }

        ActivateAvailableCells();
    }

    public void CreateBomb(GameObject cell)
    {
        int randCubeIndex = Random.Range(0, _cubeTypeUnlockValue);
        Vector3 pos = cell.transform.position;
        GameObject bombClone = Instantiate(_bombPrefabs[randCubeIndex], new Vector3(pos.x, 0.5f, pos.z), Quaternion.identity);
        BombController bombController = bombClone.GetComponent<BombController>();
        bombController.parentCell = cell;
        bombController.gridStateManager = this;
        availableBombs.Add(bombClone);
    }

    public void ActivateAvailableCells()
    {
        foreach (GameObject cell in availableCells)
        {
            initialCells.Remove(cell);
            cell.GetComponent<CellScript>().isActive = true;
        }
    }

    public void ActivateNewCell(Vector3 currentCubePos)
    {
        foreach (GameObject cell in initialCells)
        {
            if (cell.transform.position == currentCubePos)
            {
                availableCells.Add(cell);
                initialCells.Remove(cell);
                cell.GetComponent<CellScript>().isActive = true;
                return;
            }
        }
    }

}

public enum GridActionTypes
{
    PlaceBomb,
    CancelPlaceBomb,
    Blast
}
