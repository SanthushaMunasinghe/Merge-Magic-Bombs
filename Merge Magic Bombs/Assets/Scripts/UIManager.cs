using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    private Camera _mainCam;
    private GridStateManager _gridStateManager;
    private GameplayManager _gameplayManager;

    public GameObject StartPanel;
    public GameObject GameOverPanel;
    public GameObject GameCompletePanel;
    public GameObject bombPanel;
    public GameObject blastBtn;
    public GameObject currentObject;

    [SerializeField] private TextMeshProUGUI timeText;

    private void Awake()
    {
        _mainCam = Camera.main;
        _gridStateManager = GetComponent<GridStateManager>();
        _gameplayManager = GetComponent<GameplayManager>();

    }

    private void Update()
    {
        timeText.text = $"Time Left: {_gameplayManager.formattedTime}";

        if (_gameplayManager.isGameOver)
        {
            _gridStateManager.GameStop();
            GameOverPanel.SetActive(true);
        }

        if (_gameplayManager.isGameComplete)
        {
            _gridStateManager.GameStop();
            GameCompletePanel.SetActive(true);
        }
    }

    public void UITouched(Vector2 pos)
    {
        if (!_gridStateManager.isListen)
        {
            return;
        }

        Ray ray = _mainCam.ScreenPointToRay(pos);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Collider collider = hit.collider;

            if (collider != null)
            {
                currentObject = collider.gameObject;
                _gridStateManager.UITouched(collider);
            }
        }
    }

    public void StartGame()
    {
        StartPanel.SetActive(false);
        _gridStateManager.UpdateState(GridActionType.Start);
        _gameplayManager.isGameStart = true;
    }

    public void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    public void NextLevel()
    {
        int levelUp = (_gameplayManager.levelIndex += 1);
        PlayerPrefs.SetInt("CurrentLevel", levelUp);
        SceneManager.LoadScene("Level0" + (levelUp + 1));
    }

    public void ConfirmRandomBomb()
    {
        _gridStateManager.UpdateState(GridActionType.PlaceBomb);
    }

    public void RejectRandomBomb()
    {
        _gridStateManager.UpdateState(GridActionType.CancelPlaceBomb);
    }

    public void Blast()
    {
        _gridStateManager.UpdateState(GridActionType.Blast);
    }
}
