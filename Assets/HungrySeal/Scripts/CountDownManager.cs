using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownManager : MonoBehaviour
{
    private Text countDown;
    private bool isThreeCounted = false;
    private bool isTwoCounted = false;
    private bool isOneCounted = false;
    private bool isStartCounted = false;

    void Start()
    {
        countDown = gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float countDownLeft = GameManager.instance.countDownLeft;        

        if (countDownLeft > 3 && countDownLeft <= 4)
        {
            countDown.text = "3";
            if(!isThreeCounted)
            {
                AudioManager.instance.playSoundSoundGameCountDown();
                isThreeCounted = true;
            }            
        }
        else if (countDownLeft > 2 && countDownLeft <= 3)
        {
            countDown.text = "2";
            if (!isTwoCounted)
            {
                AudioManager.instance.playSoundSoundGameCountDown();
                isTwoCounted = true;
            }
        }
        else if (countDownLeft > 1 && countDownLeft <= 2)
        {
            countDown.text = "1";
            if (!isOneCounted)
            {
                AudioManager.instance.playSoundSoundGameCountDown();
                isOneCounted = true;
            }
        }
        else if (countDownLeft > 0.1 && countDownLeft <= 1)
        {
            countDown.text = "START";
            if (!isStartCounted)
            {
                AudioManager.instance.playSoundGameStart();
                isStartCounted = true;
            }
        }
        else if (countDownLeft < 0.1)
        {
            countDown.text = "";
            isThreeCounted = false;
            isTwoCounted = false;
            isOneCounted = false;
            isStartCounted = false;
        }
    }
}
