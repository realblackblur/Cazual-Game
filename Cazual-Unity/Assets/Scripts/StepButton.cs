using UnityEngine;

public class StepButton : MonoBehaviour
{
    public DualCharacterMovement movementManager;

    public int stepAmount = 1;

    public void ClickStepButton()
    {
        Debug.Log("Step button clicked: " + stepAmount);

        if (movementManager == null)
        {
            Debug.LogError("Movement Manager is not assigned.");
            return;
        }

        movementManager.SelectSteps(stepAmount);
    }
}