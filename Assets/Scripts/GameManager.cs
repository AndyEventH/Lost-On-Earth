using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject bulletContainer;
    [SerializeField] public GameObject collectibleContainer;
    [SerializeField] EnvironmentManager environmentManager;

    public static GameManager gameManager { get; private set; }

    public UnitHealth _playerHealth = new UnitHealth(100, 100);
    [SerializeField] private int numberOfEnemies;
    public int[] enemiesKilled;
    public int[] randomDropRateCollectible;
    public bool[] isCollected;
    [SerializeField] private int minEnemiesKilledCollectible;
    [SerializeField] private int maxEnemiesKilledCollectible;

    public Transform PlayerReference;

    public int collectiblesAchieved = 0;

    [SerializeField] private UITextTypeWriter UITTW;
    [SerializeField] UIManager UIM;

    [SerializeField] AudioSource backgroundMusic;

    [SerializeField] AudioSource UISound;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip lossSound;

    public enum GameStatus
    {
        gameStart,
        introGame,
        gameRunning,
        gamePause,
        inMenu
    }

    [SerializeField] public GameStatus gameStatus;

    [SerializeField] float deadUISeconds = 2f;




    // Start is called before the first frame update
    void Awake()
    {
        gameStatus = GameStatus.inMenu; //inMenu
        if (numberOfEnemies != 0)
        {
            enemiesKilled = new int[numberOfEnemies];
            randomDropRateCollectible = new int[numberOfEnemies];
            isCollected = new bool[numberOfEnemies];
            Debug.Log(enemiesKilled.Length);
        }
        for (int i = 0; i < randomDropRateCollectible.Length; i++)
        {
            randomDropRateCollectible[i] = Random.Range(minEnemiesKilledCollectible, maxEnemiesKilledCollectible); ;
        }
        //check if gameManager exists
        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
    }
    private bool hasPassedLevel2;
    private bool hasPassedLevel3;
    //private bool onlyOnce;
    void Update()
    {
        if (!hasPassedLevel2 && collectiblesAchieved == 1)
        {
            environmentManager.PassLevel2();
            hasPassedLevel2 = true;
        }
        else if (!hasPassedLevel3 && collectiblesAchieved == 2)
        {
            environmentManager.PassLevel3();
            hasPassedLevel3 = true;
        }

        if (Input.GetKey(KeyCode.Space) && gameStatus == GameStatus.inMenu)
        {
            gameStatus = GameStatus.gameStart;
        }



        if (gameStatus == GameStatus.inMenu)
        {
            UIM.ShowStartMenu();
            Time.timeScale = 0;
        }
        else if (gameStatus == GameStatus.gameStart)
        {
            StartGame();
            UIM.IntroGameCanvas.SetActive(false);
            gameStatus = GameStatus.gameRunning;
            Time.timeScale = 1;
            backgroundMusic.Play();
        }
        else if (gameStatus == GameStatus.gamePause)
        {

            Time.timeScale = 0;
            backgroundMusic.Pause();
        }

        if (Input.GetKeyDown(KeyCode.Space) && gameStatus == GameStatus.introGame)
        {
            if (!UITTW.isDone)
            {
                UITTW.FinishWriting();
            }
            else
            {
                gameStatus = GameStatus.gameStart;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && gameStatus == GameStatus.gameRunning)
        {
            gameStatus = GameStatus.gamePause;
            UIM.InMenuCanvas.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gameStatus == GameStatus.gamePause)
        {
            gameStatus = GameStatus.gameRunning;
            UIM.InMenuCanvas.SetActive(false);
            Time.timeScale = 1;
            backgroundMusic.Play();
        }


        if (_playerHealth.Health == 0)
        {
            StartCoroutine(FinishGame(false));
        }else if(collectiblesAchieved == numberOfEnemies)
        {
            StartCoroutine(FinishGame(true));
        }
    }

    //UI
    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        gameStatus = GameStatus.introGame;
        UIM.StartGameCanvas.SetActive(false);
        UIM.IntroGameCanvas.SetActive(true);
        UITTW.StartWriting();
    }

    public void ResumeGame()
    {
        gameStatus = GameStatus.gameRunning;
        UIM.InMenuCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator FinishGame(bool isWin)
    {

        yield return new WaitForSeconds(deadUISeconds);
        backgroundMusic.Stop();
        if (isWin)
        {
            UIM.loseText.SetActive(false);
            UIM.winText.SetActive(true);
            UISound.PlayOneShot(winSound);
        }
        else
        {
            UIM.winText.SetActive(false);
            UIM.loseText.SetActive(true);
            UISound.PlayOneShot(lossSound);
        }
        Time.timeScale = 0;
        UIM.StartGameCanvas.SetActive(false);
        UIM.InMenuCanvas.SetActive(false);
        UIM.EndGameCanvas.SetActive(true);
    }
}
