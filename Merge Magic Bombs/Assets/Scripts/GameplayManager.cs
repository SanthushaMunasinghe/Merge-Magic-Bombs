using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public int levelIndex = 0;

    [SerializeField] private float duration = 1.0f;
    public string formattedTime;

    public bool isGameStart = false;
    public bool isGameOver = false;
    public bool isGameComplete = false;

    void Awake()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            levelIndex = PlayerPrefs.GetInt("CurrentLevel");
        }
    }

    void Update()
    {
        if (isGameStart && !isGameComplete)
            GameTimer();
    }

    private void GameTimer()
    {
        if (duration > 0)
            duration -= Time.deltaTime;
        else
        {
            isGameOver = true;
        }

        int minutes = Mathf.FloorToInt(duration / 60f);
        int seconds = Mathf.FloorToInt(duration % 60f);
        int milliseconds = Mathf.FloorToInt((duration * 1000f) % 1000f);

        formattedTime = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}

public enum CubeColor
{
    Blue,
    Green,
    Red,
    Yellow
}
