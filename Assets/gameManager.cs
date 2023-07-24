using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Reflection;

public class gameManager : MonoBehaviour
{
    public float restartDelay;
    private bool isGameEnded;
    public Rigidbody rb;

    void Start()
    {
        isGameEnded = false;
    }
    public void Endgame()
    {
        if (!isGameEnded)
        {
            isGameEnded = true;
            //rb.isKinematic = true;
            Invoke("Restart", restartDelay);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}
