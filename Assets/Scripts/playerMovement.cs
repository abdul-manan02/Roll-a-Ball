using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using CnControls;
public class playerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float zForce;
    public float xForce;
    public float yForce;
    public int score;
    public Text scoreText;
    public int highscore;
    public Text highscoreText;
    public GameObject winBg;
    public GameObject loseBg;
    public GameObject explosionPrefab;
    public GameObject minDistanceCollectible;
    List<GameObject> allFires = new List<GameObject>();
    List<GameObject> allCollectibles = new List<GameObject>();
    private bool canJump;
    public bool death;
    public bool gamePaused;

    // Start is called before the first frame update
    void Start()
    {
        allFires.AddRange( GameObject.FindGameObjectsWithTag("Fire") );
        allCollectibles.AddRange( GameObject.FindGameObjectsWithTag("Collectible") );
        allCollectibles.AddRange( GameObject.FindGameObjectsWithTag("deathCollectible") );
        allCollectibles.AddRange( GameObject.FindGameObjectsWithTag("2xCollectible") );
        rb = GetComponent<Rigidbody>();
        death = false;
        gamePaused = false;
        zForce = 10f;
        xForce = 10f;
        yForce = 10f;
        score = 0;
        scoreText.text = "SCORE : " + score.ToString();
        highscore = PlayerPrefs.GetInt("HighScore", 0); // Load the high score from PlayerPrefs
        highscoreText.text = "HIGH SCORE : " + highscore.ToString();
        canJump = true;
        foreach (GameObject obstacles in allFires)
            obstacles.SetActive(true);
        StartCoroutine(newcoroutine());

        GameObject.Find("Explosion").SetActive(false);
        winBg.SetActive(false);
        loseBg.SetActive(false);
    }

    private IEnumerator newcoroutine()
    {
        yield return new WaitForSeconds(7f);
        foreach (GameObject obstacles in allFires)
            obstacles.SetActive(false);

        yield return new WaitForSeconds(7f);
        foreach (GameObject obstacles in allFires)
            obstacles.SetActive(true);

        StartCoroutine(newcoroutine());
    }

    void Explode()
    {
        var exp = GetComponent<ParticleSystem>();
        var main = exp.main;
        exp.Play();
        Destroy(gameObject, main.duration);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gamePaused)
        {
            minDistance();

            /*if (Input.GetKey(KeyCode.Space) && canJump)
            {
                rb.velocity = new Vector3(rb.velocity.x, zForce, rb.velocity.y);
                canJump = false;
            }*/
            if (CnInputManager.GetButtonDown("Jump") && canJump)
            {
                rb.velocity = new Vector3(rb.velocity.x, zForce, rb.velocity.y);
                canJump = false;
            }
            if (Input.GetKey(KeyCode.R))
            {
                ResetHighScore();
                FindObjectOfType<gameManager>().Endgame();
            }
            if (Input.GetKey(KeyCode.L))
            {
                ResetLevels();
                FindObjectOfType<gameManager>().Endgame();
            }

            
            /*float moveZ = Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) ?
                zForce : Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) ?
                -zForce : 0f;
            float moveX = Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) ?
                -xForce : Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) ?
                xForce : 0f;*/

            rb.velocity = new Vector3(CnInputManager.GetAxis("Horizontal")*xForce, rb.velocity.y, CnInputManager.GetAxis("Vertical")*yForce);

            /*if (Input.GetKeyUp(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0f);
            }
            
            if (Input.GetKeyUp(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = new Vector3(0f, rb.velocity.y, rb.velocity.z);
            }*/

            if (transform.position.y < 0)
            {
                if (score > highscore)
                {
                    highscore = score;
                    PlayerPrefs.SetInt("HighScore", highscore); // Save the new high score in PlayerPrefs
                    highscoreText.text = "HIGH SCORE : " + highscore.ToString();
                }
                loseBg.SetActive(true);
                FindObjectOfType<gameManager>().Endgame();
            }
        }    
    }


    void minDistance ()
    {
        float minDistance = 100000f;
        float tempDistance = 0f;
        foreach (GameObject collectible in allCollectibles)
        {
            tempDistance = Vector3.Distance(transform.position, collectible.transform.position);
            if (tempDistance < minDistance)
            {
                minDistance = tempDistance;
                minDistanceCollectible = collectible;
            }
        }
        //nameText.text = minDistanceCollectible.name;  
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true; // Enable jumping when character lands on the ground
        }

        if (collision.gameObject.CompareTag("deathCollectible") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("enemy"))
        {
            Instantiate(explosionPrefab, collision.contacts[0].point, Quaternion.identity);
            death = true;
            rb.AddForce(20f, 20f, 20f, ForceMode.Impulse);
            if (score > highscore)
            {
                highscore = score;
                PlayerPrefs.SetInt("HighScore", highscore); // Save the new high score in PlayerPrefs
                highscoreText.text = "HIGH SCORE : " + highscore.ToString();
            }
            loseBg.SetActive(true);
            PlayerPrefs.SetInt("lastLevelPlayed", SceneManager.GetActiveScene().buildIndex - 1);
            FindObjectOfType<gameManager>().Endgame();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Collectible"))
        {
            collider.gameObject.SetActive(false);
            score = score + 1;
            scoreText.text = "SCORE : " + score.ToString();
        }
        else if (collider.gameObject.CompareTag("2xCollectible"))
        {
            collider.gameObject.SetActive(false);
            score = score + 2;
            scoreText.text = "SCORE : " + score.ToString();
        }
        else if (collider.gameObject.CompareTag("Fire") || collider.gameObject.CompareTag("Saw"))
        {
            death = true;
            rb.AddForce(20f, 20f, 20f, ForceMode.Impulse);
            if (score > highscore)
            {
                highscore = score;
                PlayerPrefs.SetInt("HighScore", highscore); 
                highscoreText.text = "HIGH SCORE : " + highscore.ToString();
            }
            loseBg.SetActive(true);
            PlayerPrefs.SetInt("lastLevelPlayed", SceneManager.GetActiveScene().buildIndex - 1);
            FindObjectOfType<gameManager>().Endgame();
        }
    }
    
    // Function to reset the high score
    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0); // Reset the high score to 0 in PlayerPrefs
        highscore = 0;
        highscoreText.text = "HIGH SCORE : " + highscore.ToString();
    }

    public void ResetLevels()
    {
        PlayerPrefs.SetInt("levelAt", 1); // Reset the high score to 0 in PlayerPrefs
    }
}
