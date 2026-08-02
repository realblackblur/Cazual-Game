using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerControlToggle : MonoBehaviour
{
    [Header("UI")]
    public Toggle playerToggle;
    public TMP_Text toggleLabel;

    private void Start()
    {
        if (playerToggle != null)
        {
            playerToggle.onValueChanged.AddListener(OnToggleChanged);
        }

        UpdateLabel();
    }

    public bool IsPlayerAActive()
    {
        return playerToggle == null || !playerToggle.isOn;
    }

    private void OnToggleChanged(bool isPlayerB)
    {
        UpdateLabel();

        if (isPlayerB)
        {
            Debug.Log("Player B is active");
        }
        else
        {
            Debug.Log("Player A is active");
        }
    }

    private void UpdateLabel()
    {
        if (toggleLabel == null)
        {
            return;
        }

        if (IsPlayerAActive())
        {
            toggleLabel.text = "PLAYER A";
        }
        else
        {
            toggleLabel.text = "PLAYER B";
        }
    }
}