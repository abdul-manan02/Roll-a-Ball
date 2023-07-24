using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class moveToNextLevel : MonoBehaviour
{
    public Text winText;
    
    // Start is called before the first frame update

    
    void checkLevel()
    {
        int x = PlayerPrefs.GetInt("levelAt", 1);
        if (x!=6)
        {
            if(PlayerPrefs.GetInt("lastLevelPlayed", 1) >= x)
            {
                PlayerPrefs.SetInt("levelAt", x+1);
            }    
        } 
    }

    void loadScene()
    {
        PlayerPrefs.SetInt("lastLevelPlayed", SceneManager.GetActiveScene().buildIndex - 1);
        checkLevel();
        SceneManager.LoadScene("LevelSelection");
    }
    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<playerMovement>().score > 5 && SceneManager.GetActiveScene().buildIndex == 2)
        {
            FindObjectOfType<playerMovement>().rb.isKinematic = true;
            FindAnyObjectByType<playerMovement>().winBg.SetActive(true);                
            Invoke("loadScene", 2);
            
        }
        else if(GameObject.FindObjectOfType<playerMovement>().score > 10 && SceneManager.GetActiveScene().buildIndex == 3)
        {
            FindObjectOfType<playerMovement>().rb.isKinematic = true;
            FindAnyObjectByType<playerMovement>().winBg.SetActive(true);
            Invoke("loadScene", 2);
        }
        else if(GameObject.FindObjectOfType<playerMovement>().score > 15 && SceneManager.GetActiveScene().buildIndex == 4)
        {
            FindObjectOfType<playerMovement>().rb.isKinematic = true;
            FindAnyObjectByType<playerMovement>().winBg.SetActive(true);
            Invoke("loadScene", 2);
        }
        else if (GameObject.FindObjectOfType<playerMovement>().score > 20 && SceneManager.GetActiveScene().buildIndex == 5)
        {
            FindObjectOfType<playerMovement>().rb.isKinematic = true;
            FindAnyObjectByType<playerMovement>().winBg.SetActive(true);
            Invoke("loadScene", 2);
        }
        else if (GameObject.FindObjectOfType<playerMovement>().score > 25 && SceneManager.GetActiveScene().buildIndex == 6)
        {
            FindObjectOfType<playerMovement>().rb.isKinematic = true;
            FindAnyObjectByType<playerMovement>().winBg.SetActive(true);
            Invoke("loadScene", 2);
        }
        else if (GameObject.FindObjectOfType<playerMovement>().score > 30 && SceneManager.GetActiveScene().buildIndex == 7)
        {
            FindObjectOfType<playerMovement>().rb.isKinematic = true;
            FindAnyObjectByType<playerMovement>().winBg.SetActive(true);
            Invoke("loadScene", 2);
        }
    }
}
