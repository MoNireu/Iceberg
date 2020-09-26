using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{    
    private Button RestartBtn;
    private Button QuitBtn;
    private Text ScoreText;
    
    // Start is called before the first frame update
    void Start()
    {        
        RestartBtn = gameObject.GetComponentsInChildren<Button>()[0];
        QuitBtn = gameObject.GetComponentsInChildren<Button>()[1];
        
        RestartBtn.onClick.AddListener(restartBtnClicked);
        QuitBtn.onClick.AddListener(quitBtnClicked);

        ScoreText = gameObject.GetComponentsInChildren<Text>()[0];        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = ((int)GameManager.instance.getAllScores()[0]).ToString() + "점";      
    }
   
    private void restartBtnClicked()
    {        
        SceneManager.LoadScene("HungrySeal", LoadSceneMode.Single);
    }

    private void quitBtnClicked()
    {
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }
}
