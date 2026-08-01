using System.Collections;
using UnityEngine;

public class DualCharacterMovement : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] private Transform playerA;
    [SerializeField] private Transform playerB;

    [Header("Control")]
    [SerializeField] private PlayerControlToggle playerControlToggle;

    [Header("Movement Settings")]
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private float moveSpeed = 5f;

    [Header("Step Settings")]
    [SerializeField] private int selectedSteps = 3;

    private bool isMoving = false;
    private bool halfStepBanked = false;

    private void Update()
    {
        if (isMoving)
        {
            return;
        }

        HandleStepSelection();
        HandleMovementInput();
    }

    private void HandleStepSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedSteps = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedSteps = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedSteps = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedSteps = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectedSteps = 5;
        }
    }

    private void HandleMovementInput()
    {
        Vector2 direction = Vector2.zero;

        if (
            Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.UpArrow)
        )
        {
            direction = Vector2.up;
        }
        else if (
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.DownArrow)
        )
        {
            direction = Vector2.down;
        }
        else if (
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.LeftArrow)
        )
        {
            direction = Vector2.left;
        }
        else if (
            Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.RightArrow)
        )
        {
            direction = Vector2.right;
        }

        if (direction != Vector2.zero)
        {
            StartCoroutine(MoveTurn(direction));
        }
    }

    private IEnumerator MoveTurn(Vector2 direction)
    {
        isMoving = true;

        Transform activePlayer;
        Transform passivePlayer;

        if (playerControlToggle.IsPlayerAActive)
        {
            activePlayer = playerA;
            passivePlayer = playerB;
        }
        else
        {
            activePlayer = playerB;
            passivePlayer = playerA;
        }

        int passiveSteps = CalculatePassiveSteps();

        Vector3 activeTarget =
            activePlayer.position +
            (Vector3)direction *
            selectedSteps *
            cellSize;

        Vector3 passiveTarget =
            passivePlayer.position +
            (Vector3)direction *
            passiveSteps *
            cellSize;

        while (
            Vector3.Distance(
                activePlayer.position,
                activeTarget
            ) > 0.001f
            ||
            Vector3.Distance(
                passivePlayer.position,
                passiveTarget
            ) > 0.001f
        )
        {
            activePlayer.position =
                Vector3.MoveTowards(
                    activePlayer.position,
                    activeTarget,
                    moveSpeed * Time.deltaTime
                );

            passivePlayer.position =
                Vector3.MoveTowards(
                    passivePlayer.position,
                    passiveTarget,
                    moveSpeed * Time.deltaTime
                );

            yield return null;
        }

        activePlayer.position = activeTarget;
        passivePlayer.position = passiveTarget;

        Debug.Log(
            "Active player moved " +
            selectedSteps +
            " cells. Passive player moved " +
            passiveSteps +
            " cells."
        );

        isMoving = false;
    }

    private int CalculatePassiveSteps()
    {
        if (selectedSteps % 2 == 0)
        {
            return selectedSteps / 2;
        }

        if (halfStepBanked)
        {
            halfStepBanked = false;

            return selectedSteps / 2 + 1;
        }

        halfStepBanked = true;

        return selectedSteps / 2;
    }
}