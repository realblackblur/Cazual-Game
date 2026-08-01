using UnityEngine;
using UnityEngine.UI;

public class PlayerControlToggle : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Toggle playerToggle;
    [SerializeField] private Text toggleLabel;

    public bool IsPlayerAActive { get; private set; } = true;

    private void Start()
    {
        playerToggle.isOn = true;

        UpdateToggleLabel();

        playerToggle.onValueChanged.AddListener(
            OnToggleChanged
        );
    }

    private void OnToggleChanged(bool isOn)
    {
        IsPlayerAActive = isOn;

        UpdateToggleLabel();

        if (IsPlayerAActive)
        {
            Debug.Log("Player A is active");
        }
        else
        {
            Debug.Log("Player B is active");
        }
    }

    private void UpdateToggleLabel()
    {
        if (IsPlayerAActive)
        {
            toggleLabel.text = "PLAYER A";
        }
        else
        {
            toggleLabel.text = "PLAYER B";
        }
    }
}