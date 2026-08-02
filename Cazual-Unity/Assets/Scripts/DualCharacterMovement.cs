using UnityEngine;

public class DualCharacterMovement : MonoBehaviour
{
    [Header("Players")]
    public Transform playerA;
    public Transform playerB;

    [Header("Controller")]
    public PlayerControlToggle playerControlToggle;

    [Header("Movement")]
    public float cellSize = 1f;

    private int selectedSteps = 1;
    private void Start()
    {
        Debug.Log("DualCharacterMovement started");
    }
    public void TestStep1Click()
    {
        Debug.Log("Step 1 button was clicked");
    }

    public void SelectSteps(int steps)
    {
        selectedSteps = steps;
        Debug.Log("Selected steps: " + selectedSteps);
    }

    public void MoveUp()
    {
        MoveSelectedPlayer(Vector2.up);
    }

    public void MoveDown()
    {
        MoveSelectedPlayer(Vector2.down);
    }

    public void MoveLeft()
    {
        MoveSelectedPlayer(Vector2.left);
    }

    public void MoveRight()
    {
        MoveSelectedPlayer(Vector2.right);
    }

    private void MoveSelectedPlayer(Vector2 direction)
    {
        if (playerControlToggle == null)
        {
            Debug.LogError("Player Control Toggle is not assigned.");
            return;
        }

        Transform activePlayer;

        if (playerControlToggle.IsPlayerAActive())
        {
            activePlayer = playerA;
            Debug.Log("Moving Player A");
        }
        else
        {
            activePlayer = playerB;
            Debug.Log("Moving Player B");
        }

        if (activePlayer == null)
        {
            Debug.LogError("The active player is not assigned.");
            return;
        }

        Vector3 movement = new Vector3(
            direction.x,
            direction.y,
            0f
        );

        activePlayer.position += movement * selectedSteps * cellSize;
    }
}