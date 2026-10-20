using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameWin,
        GameLose
    }

    public GameState CurrentState { get; private set; } = GameState.MainMenu;

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetGameState(GameState newState)
    {
        CurrentState = newState;
        UIManager.Instance?.UIOnGameStateChanged();
    }
}
