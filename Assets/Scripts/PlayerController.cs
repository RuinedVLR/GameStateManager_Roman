using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (GameStateManager.Instance.CurrentState != GameStateManager.GameState.Playing)
            return; // Do not process movement if the game is not in the Playing state

        float moveHorizontal = 0f;
        if (Keyboard.current.aKey.isPressed)
        {
            moveHorizontal = -1f;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            moveHorizontal = 1f;
        }

        Vector2 movement = new Vector2(moveHorizontal, 0f);
        rb.AddForce(movement * speed);
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (GameStateManager.Instance.CurrentState == GameStateManager.GameState.Playing)
            { 
                Debug.Log("Paused");
                GameStateManager.Instance.SetGameState(GameStateManager.GameState.Paused);
            }
            else if (GameStateManager.Instance.CurrentState == GameStateManager.GameState.Paused)
            {
                Debug.Log("Unpaused");
                GameStateManager.Instance.SetGameState(GameStateManager.GameState.Playing);
            }
        }
    }
}
