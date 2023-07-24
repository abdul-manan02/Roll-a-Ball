using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class lvl_buttons : MonoBehaviour
{
    public GameObject resumeButton;
    public GameObject exitButton;
    public GameObject pauseButton;

    private void Start()
    {
        resumeButton = GameObject.Find("resumeButton");
        exitButton = GameObject.Find("exitButton");
        pauseButton = GameObject.Find("pauseButton");
        resumeButton.SetActive(false);
        exitButton.SetActive(false);
    }

    public void button_pressed()
    {
        if (EventSystem.current.currentSelectedGameObject == pauseButton)
        {
            pauseButton.SetActive(false);
            pauseFunctionality();
        }
        else if (EventSystem.current.currentSelectedGameObject == resumeButton)
        {
            GameObject.Find("resumeButton").SetActive(false);
            GameObject.Find("exitButton").SetActive(false);
            Time.timeScale = 1;
            FindObjectOfType<playerMovement>().gamePaused = false;
            pauseButton.SetActive(true);
        }
        else if (EventSystem.current.currentSelectedGameObject == exitButton)
        {
            SceneManager.LoadScene("LevelSelection");
        }
    }

    public void pauseFunctionality()
    {
        Time.timeScale = 0;
        FindObjectOfType<playerMovement>().gamePaused = true;
        resumeButton.SetActive(true);
        exitButton.SetActive(true);
    }
}
