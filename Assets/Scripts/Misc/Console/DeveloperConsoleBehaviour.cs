using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;
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
    bool triggered;
    [HideInInspector]public bool cheats;
    [HideInInspector]public bool cheatsWereEnabled;
    [HideInInspector]public bool god;
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
    private void FixedUpdate() { if (cheats) { cheatsWereEnabled = true; } }

    public void Toggle(CallbackContext context)
    {
        if (!(InputSystem.GetDevice<Keyboard>().slashKey.isPressed&& InputSystem.GetDevice<Keyboard>().leftShiftKey.isPressed&&InputSystem.GetDevice<Keyboard>().leftCtrlKey.isPressed)) { triggered = false;  return; }
        if (!triggered)
        {
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
        triggered = true;
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
