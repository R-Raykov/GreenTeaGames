using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            // In case the game gets started from the main scene instead of the menu scene
            if (_instance == null)
            {
                GameObject gameManagerObject = new GameObject();
                gameManagerObject.name = "GameManager";
                gameManagerObject.AddComponent<GameManager>();

                DontDestroyOnLoad(gameManagerObject);

            }
            return _instance;
        }
    }


    public GameObject Player;

    private bool _gameIsPaused;
    private MenuManager _menuManager;

    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null)
            Destroy(_instance.gameObject);
        
        _instance = this;

        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// Loads the main level
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("mainScene");

        _gameIsPaused = false;
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// Load the menu scene
    /// </summary>
    public void BackToMenu()
    { 
        SceneManager.LoadScene("MenuScene");
    }

    /// <summary>
    /// Exits the game
    /// </summary>
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    /// <summary>
    /// Pauses and resumes the game
    /// </summary>
    public void PauseGame()
    {
        _gameIsPaused = !_gameIsPaused;

        if (_gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// When the player dies
    /// </summary>
    public void GameOver()
    {
        _menuManager.GameOverScreen();

        PauseGame();
    }

    // We don't want to be able to set the value from anoher script
    public bool IsPaused { get => _gameIsPaused; }

    /// <summary>
    /// Attatches the manu manager, I did this because I wanted to avoid using a singleton in the MenuManager when it wouldn't have been used anywhere else
    /// </summary>
    /// <param name="pMenuManager"> Manager to attatch </param>
    public void AttatchMenuManager(MenuManager pMenuManager)
    {
        _menuManager = pMenuManager;
    }

    private void OnDestroy()
    {
        _instance = null;
    }
}
