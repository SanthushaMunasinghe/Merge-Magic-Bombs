using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public int levelIndex;

    void Awake()
    {
        levelIndex = 1;
    }

    void Update()
    {
        
    }
}

public enum CubeColor
{
    Blue,
    Green,
    Red,
    Yellow
}
