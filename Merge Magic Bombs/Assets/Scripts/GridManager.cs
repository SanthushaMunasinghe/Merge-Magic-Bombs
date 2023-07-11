using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : GameplayManager
{
    //Cube Data
    private int _cubeTypeUnlockValue = 3;
    private int _cubeStrengthRange = 2;

    [Header("Cube Data")]
    [SerializeField] private GameObject[] _cubePrefabs;
    [SerializeField] private int _maxCubeStrengthRange;

    //Grid Data
    [Header("Grid Data")]
    [SerializeField] private Transform _cellParent;
    public List<GameObject> _initialCellTf = new List<GameObject>();
    public List<GameObject> availableCells = new List<GameObject>();

    void Start()
    {
        UnlockCubeValues();
        GenerateGrid();
    }

    void Update()
    {
        
    }

    private void UnlockCubeValues()
    {
        int currentUnlockValue = levelIndex - 1;

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
        foreach (GameObject posObj in _initialCellTf)
        {
            int randCubeIndex = Random.Range(0, _cubeTypeUnlockValue);
            Vector3 spawnPos = new Vector3(posObj.transform.position.x, posObj.transform.position.y + 0.25f, posObj.transform.position.z);

            GameObject cubeClone = Instantiate(_cubePrefabs[randCubeIndex], spawnPos, Quaternion.identity);
            cubeClone.GetComponent<CubeObject>().cubeStrength = Random.Range(1, _cubeStrengthRange + 1);
        }
    }

    private void SetInitialCells()
    {
        for (int i = 0; i < _cellParent.childCount; i++)
        {
            if (i < 5)
                availableCells.Add(_cellParent.GetChild(i).gameObject);
            else
                _initialCellTf.Add(_cellParent.GetChild(i).gameObject);
        }

        UpdateAvailableCells();
    }

    private void UpdateAvailableCells()
    {
        foreach (GameObject cell in availableCells)
        {
            cell.GetComponent<CellScript>().isAvailable = true;
        }
    }
}
