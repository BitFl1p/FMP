using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menu, instructions;
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Instructions()
    {
        if(instructions.activeInHierarchy) instructions.SetActive(false);
        else instructions.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
