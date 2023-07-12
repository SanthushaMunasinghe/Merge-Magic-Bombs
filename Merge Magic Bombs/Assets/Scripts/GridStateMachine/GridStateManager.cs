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
    public GameObject[] bombModification01Prefabs;
    public GameObject[] bombModification02Prefabs;
    public GameObject[] bombModification03Prefabs;
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

    public void UpdateState(GridActionType type)
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

    public void CreateModification(GameObject cube)
    {
        int randCubeIndex = Random.Range(0, _cubeTypeUnlockValue);
        int randModIndex = Random.Range(0, 3);
        Vector3 pos = cube.transform.position;

        GameObject modClone;
        if (randModIndex == 0)
        {
            modClone = Instantiate(bombModification01Prefabs[randCubeIndex], new Vector3(pos.x, 0.5f, pos.z), Quaternion.identity);
        }
        else if (randModIndex == 1)
        {
            modClone = Instantiate(bombModification02Prefabs[randCubeIndex], new Vector3(pos.x, 0.5f, pos.z), Quaternion.identity);
        }
        else
        {
            modClone = Instantiate(bombModification03Prefabs[randCubeIndex], new Vector3(pos.x, 0.5f, pos.z), Quaternion.identity);
        }

        BombModification bombModification = modClone.GetComponent<BombModification>();
        bombModification.parentCell = ReturnAvailableCell(pos);
        bombModification.gridStateManager = this;
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

    public GameObject ReturnAvailableCell(Vector3 pos)
    {
        GameObject cell = null;
        Debug.Log("Pos: " + pos);
        foreach (GameObject currentCell in availableCells)
        {
            Debug.Log(currentCell.transform.position);
            if (currentCell.transform.position.x == pos.x && currentCell.transform.position.z == pos.z)
            {
                cell = currentCell;
            }
        }

        return cell;
    }

}

public enum GridActionType
{
    PlaceBomb,
    CancelPlaceBomb,
    Blast
}

public enum ModificationType
{
    modtype01,
    modtype02,
    modtype03,
}
