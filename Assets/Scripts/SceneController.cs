using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{

    string sceneName = "main";

    public GameObject player;
    public GameObject mainCamera;
    public GameObject mainPanel;
    public GameObject colorSwitch;
    ColorController colorController;

    Animator mainAnimator;

    public GameObject restartPanel;
    Animator restartAnimator;

    public GameObject gamePanel;
    Animator gameAnimator;

    public bool IsPlayerAlive { get; set; }
    public bool IsPaused { get; set; }

    public GameObject scoreObject;
    public GameObject bestScoreObject;
    Text scoreText;
    Text bestScoreText;

    public GameObject adsTimerObject;
    AdsTimer adsTimer;

    public int score;
    public int bestScore;

    public int RingCounter { get; set; }

    CameraController cameraController;
    PlayerController playerController;
    RingSpawner ringSpawner;

    private void Awake()
    {
        colorSwitch = GameObject.Find("ColorManagement");
        colorController = colorSwitch.GetComponent<ColorController>();
        colorController.changeColor = true;
    }
    // Use this for initialization
    void Start()
    {
        ringSpawner = GetComponent<RingSpawner>();
        scoreText = scoreObject.GetComponent<Text>();

        score = 0;
        UpdateScore();

        bestScoreText = bestScoreObject.GetComponent<Text>();
        LoadGame();

        cameraController = mainCamera.GetComponent<CameraController>();
        playerController = player.GetComponent<PlayerController>();

        mainAnimator = mainPanel.GetComponent<Animator>();
        restartAnimator = restartPanel.GetComponent<Animator>();
        gameAnimator = gamePanel.GetComponent<Animator>();

        adsTimer = adsTimerObject.GetComponent<AdsTimer>();



    }

    private void Update()
    {
        if (IsPlayerAlive == false)
        {
            gameAnimator.SetBool("Close", true);
            gameAnimator.SetBool("Open", false);


            if (bestScore <= score)
            {
                bestScore = score;
                UpdateBestScore();
            }
            restartAnimator.SetBool("Close", false);
            restartAnimator.SetBool("Open", true);

            adsTimer.startTimer = true;
        }
    }

    public void WatchedAd()
    {
        adsTimer.watchAds.SetActive(false);
        IsPlayerAlive = true;
        IsPaused = false;
        restartAnimator.SetBool("Close", true);
        restartAnimator.SetBool("Open", false);
        gameAnimator.SetBool("Close", false);
        gameAnimator.SetBool("Open", true);
    }

    public void LoadScene(string sceneName)
    {

        SceneManager.LoadScene(sceneName);
    }

    public void RestartScene()
    {

        ResetGravity();

        SaveGame();

        LoadScene(sceneName);
    }

    public void ResetGravity()
    {
        playerController.jumpHeight = 58;
        Physics.gravity = new Vector3(0, -200, 0);
    }
    public void UpdateGravity()
    {
        if (Physics.gravity.y > -376)
        {
            playerController.jumpHeight += (58 * 0.0275f);
           // Debug.Log((58 * 0.15f));
            Physics.gravity = new Vector3(0, Physics.gravity.y + (-200 * 0.055f), 0);
        }
    }

    public void Play()
    {
        if (IsPlayerAlive == true && IsPaused == true)
        {
            ringSpawner.SpawnStart();
            mainAnimator.SetTrigger("Expand");
            gameAnimator.SetBool("Close", false);
            gameAnimator.SetBool("Open", true);
            IsPaused = false;
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    void UpdateBestScore()
    {
        bestScoreText.text = bestScore.ToString();
    }

    public void FollowCamera(bool boo)
    {
        if (boo == true)
            cameraController.follow = true;
        else if (boo == false)
            cameraController.follow = false;
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        save.score = bestScore;
        return save;
    }

    public void SaveGame()
    {
        // 1
        Save save = CreateSaveGameObject();

        // 2
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamescore.save");
        bf.Serialize(file, save);
        file.Close();
        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        // 1
        if (File.Exists(Application.persistentDataPath + "/gamescore.save"))
        {

            // 2
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamescore.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();


            // 4

            bestScore = save.score;
            UpdateBestScore();

            Debug.Log("Game Loaded");
        }
        else
        {
            bestScore = 0;
            UpdateBestScore();
        }
    }

}
