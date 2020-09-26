using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum PlayerLocation
{
    air,
    ground,
    water
}


public class GameManager : MonoBehaviour
{
    public static GameManager instance;    

    //load Canvas
    private Canvas CountDownCanvas;
    private Canvas GamePauseCanvas;
    private Canvas GameOverCanvas;

    private PlayerLocation currentPlayerLocation;
    private PlayerLocation lastPlayerLocation;
    
    private bool isInitComplete = false;        
    public float countDownLeft = 4f;

    private int gameScore = 0;
    private float gameTime = 60;
    private float currentPlayerEnergy = 100;
    private float currentPlayerOxygen = 100;

    // Oxygen
    private float waterOxygenConsumption = -2f;
    private float airOxygenRecover = 5f;    
    private float groundOxygenRecover = 50f;

    // Energy
    private float fishEnergy = 5f;
    private float jellyfishEnergy = -5f;
    private float trashEnergy = -10f;

    // Gravity
    private float groundGravity = -20f;
    private float waterGravity = -1f;
    private float pauseGravity = 0f;

    private void Awake()
    {
        instance = this;
    }

    public bool isGameStarted = false;

    void Start()
    {
        Physics.gravity = new Vector3(0, pauseGravity, 0);        
        CountDownCanvas = GameObject.Find("CountDownCanvas").gameObject.GetComponent<Canvas>();
        GamePauseCanvas = GameObject.Find("GamePauseCanvas").gameObject.GetComponent<Canvas>();
        GameOverCanvas = GameObject.Find("GameOverCanvas").gameObject.GetComponent<Canvas>();
    }

    
    void Update()
    {
        if (!isInitComplete)
        {            
            gameStart();            
            isInitComplete = true;
        }

        updateTime();
        updateEnergy();
        updateOxygen();
        updatePlayerSoundByPlayerLocation();

        if (isGameOver())
        {
            gameOver();
        }

        lastPlayerLocation = currentPlayerLocation;
    }

    // Public Functions
    public void addScore(string objTag)
    {
        float energy = currentPlayerEnergy;
        switch(objTag)
        {
            case "Fish":
                gameScore += 10;
                energy += fishEnergy;
                AudioManager.instance.playSoundAddScore();
                break;

            case "JellyFish":
                energy += jellyfishEnergy;
                AudioManager.instance.playSoundDamaged();
                break;

            case "Trash":
                energy += trashEnergy;
                AudioManager.instance.playSoundDamaged();
                break;

            case "Clock":
                gameTime += 10;
                break;
        }
        currentPlayerEnergy = Mathf.Clamp(energy, 0, 100);        
    }

    public void changePlayerLocation(PlayerLocation newPlayerLocation)
    {
        currentPlayerLocation = newPlayerLocation;
    }

    public void playerDashed()
    {
        currentPlayerEnergy -= 1;
    }

    public float[] getAllScores()
    {
        float[] allScores = {
            (float)gameScore,
            gameTime,
            currentPlayerEnergy,
            currentPlayerOxygen
        };

        return allScores;
    }

    public bool isGameOver()
    {
        if ((gameTime <= 0) ||
            (currentPlayerEnergy <= 0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // Private Functions


    private void updateTime()
    {
        gameTime -= 1f * Time.deltaTime;
    }

    private void updateEnergy()
    {
        currentPlayerEnergy -= 1f * Time.deltaTime;
        if (currentPlayerOxygen <= 0)
        {
            currentPlayerEnergy -= 3f * Time.deltaTime;
        }
    }

    private void updateOxygen()
    {
        float oxygenValue = 0f;

        switch (currentPlayerLocation)
        {            
            case PlayerLocation.air:
                oxygenValue = airOxygenRecover;
                break;

            case PlayerLocation.ground:
                oxygenValue = groundOxygenRecover;
                break;

            case PlayerLocation.water:
                oxygenValue = waterOxygenConsumption;                
                break;
        }

        currentPlayerOxygen = Mathf.Clamp(
            currentPlayerOxygen + oxygenValue * Time.deltaTime,
            0, 100);
    }

    private void updatePlayerSoundByPlayerLocation()
    {
        switch (currentPlayerLocation)
        {
            case PlayerLocation.air:
                AudioManager.instance.playSoundSwimming(false);
                if (lastPlayerLocation == PlayerLocation.water)
                {
                    AudioManager.instance.playSoundGetOutWater();
                }
                break;
            case PlayerLocation.ground:
                AudioManager.instance.playSoundSwimming(false);
                if (lastPlayerLocation == PlayerLocation.water)
                {
                    AudioManager.instance.playSoundGetOutWater();
                }
                break;
            case PlayerLocation.water:
                AudioManager.instance.playSoundSwimming(true);
                if(lastPlayerLocation != currentPlayerLocation)
                {
                    AudioManager.instance.playSoundGetInWater();
                }
                break;
        }
    }

    IEnumerator StartCountDown()
    {
        Time.timeScale = 0;
        float pauseTime = Time.realtimeSinceStartup + 5f;
        while (Time.realtimeSinceStartup < pauseTime)
        {
            countDownLeft = pauseTime - Time.realtimeSinceStartup;
            yield return 0;
        }
        Time.timeScale = 1;               
        changeGravity();

        isGameStarted = true;
        CountDownCanvas.gameObject.SetActive(false);
        AudioManager.instance.playeBGM(true);
    }  


    public void gameStart()
    {        
        GamePauseCanvas.gameObject.SetActive(false);
        GameOverCanvas.gameObject.SetActive(false);
        CountDownCanvas.gameObject.SetActive(true);
        StartCoroutine(StartCountDown());        
    }

    public void gamePause()
    {                
        changeGravity();        
        AudioManager.instance.playeBGM(false);
        AudioManager.instance.playSoundSwimming(false);
        AudioManager.instance.playSoundGamePause();
        GamePauseCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void gameOver()
    {        
        changeGravity();
        AudioManager.instance.playSoundGameOver();
        AudioManager.instance.playeBGM(false);
        AudioManager.instance.playSoundSwimming(false);                
        GameOverCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void changeGravity()
    {
        if (currentPlayerLocation == PlayerLocation.water)
        {
            Physics.gravity = new Vector3(0, waterGravity, 0);
        }
        else
        {
            Physics.gravity = new Vector3(0, groundGravity, 0);
        }
    }
   
}
