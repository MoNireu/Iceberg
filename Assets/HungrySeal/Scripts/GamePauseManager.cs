using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePauseManager : MonoBehaviour
{

    private Button ResumeBtn;
    private Button RestartBtn;
    private Button QuitBtn;



    // Start is called before the first frame update
    void Start()
    {
        ResumeBtn = gameObject.GetComponentsInChildren<Button>()[0];
        RestartBtn = gameObject.GetComponentsInChildren<Button>()[1];
        QuitBtn = gameObject.GetComponentsInChildren<Button>()[2];

        ResumeBtn.onClick.AddListener(resumeBtnClicked);
        RestartBtn.onClick.AddListener(restartBtnClicked);
        QuitBtn.onClick.AddListener(quitBtnClicked);
    }
   
    private void resumeBtnClicked()
    {
        GameManager.instance.gameStart();
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
