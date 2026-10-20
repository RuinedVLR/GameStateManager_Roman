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

    private void Update()
    {
        if (GameStateManager.Instance.CurrentState != GameStateManager.Instance.LastState)
        {
            UIOnGameStateChanged();
        }
    }

    public void UIOnGameStateChanged()
    {
        CloseAllUI();
        switch (GameStateManager.Instance.CurrentState)
        {
            case GameStateManager.GameState.MainMenu:
                if (mainMenuUI != null) mainMenuUI.SetActive(true);
                break;
            case GameStateManager.GameState.Playing:
                // No UI to show during gameplay
                break;
            case GameStateManager.GameState.Paused:
                if (pauseMenuUI != null) pauseMenuUI.SetActive(true);
                break;
            case GameStateManager.GameState.GameWin:
                if (gameWinUI != null) gameWinUI.SetActive(true);
                break;
            case GameStateManager.GameState.GameLose:
                if (gameLoseUI != null) gameLoseUI.SetActive(true);
                break;
        }
    }

    private void CloseAllUI()
    {
        if (mainMenuUI != null) mainMenuUI.SetActive(false);
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        if (gameWinUI != null) gameWinUI.SetActive(false);
        if (gameLoseUI != null) gameLoseUI.SetActive(false);
    }

    public void StartGame()
    {
        GameStateManager.Instance.SetGameState(GameStateManager.GameState.Playing);
        SceneManager.LoadScene("Gameplay");
    }

    public void Continue()
    {
        GameStateManager.Instance.SetGameState(GameStateManager.GameState.Playing);
        UIOnGameStateChanged();
    }

    public void QuitToMenu()
    {
        GameStateManager.Instance.SetGameState(GameStateManager.GameState.MainMenu);
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
        GameStateManager.Instance.SetGameState(GameStateManager.GameState.Playing);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
