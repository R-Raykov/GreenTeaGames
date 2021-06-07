using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Pause Menu")]
    // Pause menu buttons
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restartFromPauseButton;
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private Button _exitButtonFromPause;

    [Space]
    [Header("Game Over screen")]
    // Game Over screen buttons
    [SerializeField] private GameObject _deathScreen;
    [SerializeField] private Button _restartAfterDeathButton;
    [SerializeField] private Button _exitButtonAfterDeath;
    [SerializeField] private GameObject _newHighscore;
    

    /// Note: not happy with this, would have prefered to have a more extensive menu structure split across multiple scripts,
    /// but that would have taken another half day and I wanted to send this ASAP

    private void Awake()
    {
        GameManager.Instance.AttatchMenuManager(this);

        _pauseButton.onClick.AddListener(Pause);
        _resumeButton.onClick.AddListener(Pause);
        _restartFromPauseButton.onClick.AddListener(GameManager.Instance.StartGame);
        _backToMenuButton.onClick.AddListener(GameManager.Instance.BackToMenu);
        _exitButtonFromPause.onClick.AddListener(GameManager.Instance.ExitGame);

        _restartAfterDeathButton.onClick.AddListener(GameManager.Instance.StartGame);
        _exitButtonAfterDeath.onClick.AddListener(GameManager.Instance.ExitGame);

        _pauseMenu.SetActive(false);
        _deathScreen.SetActive(false);
    }

    private void Pause()
    {
        GameManager.Instance.PauseGame();
        _pauseMenu.SetActive(GameManager.Instance.IsPaused);
    }

    /// <summary>
    /// Activates the game over screen
    /// </summary>
    public void GameOverScreen()
    {
        _newHighscore.SetActive(ScoreManager.Instance.IsNewHighscore);
        _pauseMenu.SetActive(false);    // Just in case
        _deathScreen.SetActive(true);
        _pauseButton.onClick.RemoveAllListeners();

        ScoreManager.Instance.GetScoreText.SetActive(false);
    }

}
