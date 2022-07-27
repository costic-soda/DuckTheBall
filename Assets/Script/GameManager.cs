using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    //public Text scoreText;
    [HideInInspector] public int score;
    public GameObject gameoverScreen;
    public PlayerAnimation player;
    float t;
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        gameoverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
          if (player == null)
        {
            GameOver(true);
        }


        //scoreText.text = "SCORE: " + score; 
    }
    public void GameOver(bool actve)
    {
      gameoverScreen.SetActive(actve);        
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
