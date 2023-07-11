using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    protected int levelIndex;

    void Awake()
    {
        levelIndex = 1;
    }

    void Update()
    {
        
    }
}

public enum CubeColors
{
    Blue,
    Green,
    Red,
    Yellow
}
