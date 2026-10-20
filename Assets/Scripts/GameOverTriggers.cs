using UnityEngine;

public class GameOverTriggers : MonoBehaviour
{
    public enum TriggerType
    {
        Win,
        Lose
    }

    [SerializeField] private TriggerType triggerType;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            if (triggerType == TriggerType.Win)
            {
                GameStateManager.Instance.SetGameState(GameStateManager.GameState.GameWin);
            }
            else
            {
                GameStateManager.Instance.SetGameState(GameStateManager.GameState.GameLose);
            }
        }
    }
}
