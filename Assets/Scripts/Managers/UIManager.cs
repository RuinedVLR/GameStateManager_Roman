using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Canvas References")]
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject gameWinUI;
    [SerializeField] private GameObject gameLoseUI;

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        UIOnGameStateChanged();
    }

    public void UIOnGameStateChanged()
    {
        CloseAllUI();
        switch (GameStateManager.Instance.CurrentState)
        {
            case GameStateManager.GameState.MainMenu:
                mainMenuUI?.SetActive(true);
                break;
            case GameStateManager.GameState.Playing:
                // No UI to show during gameplay
                break;
            case GameStateManager.GameState.Paused:
                pauseMenuUI?.SetActive(true);
                break;
            case GameStateManager.GameState.GameWin:
                gameWinUI?.SetActive(true);
                break;
            case GameStateManager.GameState.GameLose:
                gameLoseUI?.SetActive(true);
                break;
            default:
                CloseAllUI();
                break;
        }
    }

    private void CloseAllUI()
    {
        mainMenuUI?.SetActive(false);
        pauseMenuUI?.SetActive(false);
        gameWinUI?.SetActive(false);
        gameLoseUI?.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
        UIOnGameStateChanged();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
