using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadLevel()
    {
        SceneManager.LoadScene("Level0" + (PlayerPrefs.GetInt("CurrentLevel") + 1));
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
}
