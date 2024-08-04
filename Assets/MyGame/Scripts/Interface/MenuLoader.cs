using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    // Method to handle Quit button click
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Method to handle Retry button click
    public void RetryLevel()
    {
        SceneManager.LoadScene("MainHouse");
    }
}
