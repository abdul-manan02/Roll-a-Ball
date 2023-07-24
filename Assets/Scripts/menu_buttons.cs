using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class menu_buttons : MonoBehaviour
{
    public GameObject startButton;
    public GameObject quit;

    public void button_pressed()
    {
        if (EventSystem.current.currentSelectedGameObject == startButton)
        {
            SceneManager.LoadScene("LevelSelection");
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "settings")
        {

        }
        else if (EventSystem.current.currentSelectedGameObject == quit)
        {
            #if UNITY_STANDALONE
                            Application.Quit();
            #endif
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
