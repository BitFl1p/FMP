using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using TMPro;

public class DeveloperConsoleBehaviour : MonoBehaviour
{
    [SerializeField] private string prefix = string.Empty;
    [SerializeField] private ConsoleCommand[] commands = new ConsoleCommand[0];

    [Header("UI")]
    [SerializeField] private GameObject uiCanvas = null;
    [SerializeField] private TMP_InputField inputField = null;
    [SerializeField] private GameObject mainUI = null;
    [SerializeField] private TMP_Text outputField = null;
    private float pausedTimeScale;

    private static DeveloperConsoleBehaviour instance;

    private DeveloperConsole developerConsole;

    private DeveloperConsole DeveloperConsole
    {
        get
        {
            if (developerConsole != null) { return developerConsole; }
            return developerConsole = new DeveloperConsole(prefix, commands);
        }
    }
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Toggle(CallbackContext context)
    {
        if (!context.action.triggered) { return; }
        if (uiCanvas.activeSelf)
        {
            Time.timeScale = pausedTimeScale;
            uiCanvas.SetActive(false);
            mainUI.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            pausedTimeScale = Time.timeScale;
            Time.timeScale = 0;
            uiCanvas.SetActive(true);
            mainUI.SetActive(false);
            inputField.ActivateInputField();
            inputField.Select();
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void ProcessCommand(string inputValue)
    {
        DeveloperConsole.ProcessCommand(inputValue);

        inputField.text = string.Empty;
    }
    public void Out(string output)
    {
        outputField.text += "\n> " + output;
    }
    public void Clear()
    {
        outputField.text = string.Empty;
    }
}
