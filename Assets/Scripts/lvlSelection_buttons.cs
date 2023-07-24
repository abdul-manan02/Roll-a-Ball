using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class lvlSelection_buttons : MonoBehaviour
{
    public Button[] buttons;

    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 1);
        for(int i=0; i<buttons.Length; i++)
        {
            if (levelAt-1<i)
            {
                buttons[i].interactable = false;
            }
        }
        //Debug.Log(PlayerPrefs.GetInt("levelAt", 1));

        int lastLevelPlayed = PlayerPrefs.GetInt("lastLevelPlayed", 1) - 1;
        buttons[lastLevelPlayed].GetComponent<Image>().color = Color.red;
    }


    public void makeInteractive(int i)
    {
        buttons[i].interactable = true;
    }
    public void button_pressed()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Lvl1")
        {
            SceneManager.LoadScene("Level1");
        }
        else if(EventSystem.current.currentSelectedGameObject.name == "Lvl2")
        {
            SceneManager.LoadScene("Level2");
        }
        else if(EventSystem.current.currentSelectedGameObject.name == "Lvl3")
        {
            SceneManager.LoadScene("Level3");
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Lvl4")
        {
            SceneManager.LoadScene("Level4");
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Lvl5")
        {
            SceneManager.LoadScene("Level5");
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Lvl6")
        {
            SceneManager.LoadScene("Level6");
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "exit")
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
