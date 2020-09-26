using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManger : MonoBehaviour
{

    public static UIManger instance;

    private float[] allScores;
    
    public Text ScoreText;
    public Text TimeText;
    public Text EnergyText;
    public Text OxygenText;
    public Text IndicatorText;
    private Button pauseBtn;    
    GameObject NpcCheck;    

    private Color defaultTextColor;
    private Color warnTextColor;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        NpcCheck = GameObject.Find("NpcCheck");
        //IndicatorCanvas = GameObject.Find("IndicatorCanvas").gameObject.GetComponent<Canvas>();

        allScores = GameManager.instance.getAllScores();
        defaultTextColor = ScoreText.color;
        warnTextColor = Color.red;

        pauseBtn = gameObject.GetComponentsInChildren<Button>()[0];
        pauseBtn.onClick.AddListener(pauseBtnClicked);
    }
    
    void Update()
    {        
        allScores = GameManager.instance.getAllScores();
        updateUI();
    }

    private void updateUI()
    {
        int gameScore = (int)allScores[0];
        float gameTime = allScores[1];
        float playerEnergy = allScores[2];
        float playerOxygen = allScores[3];

        ScoreText.text = "Score : " + gameScore;
        TimeText.text = "Time : " + (int)gameTime + " sec";
        EnergyText.text = "Energy : " + (int)playerEnergy + "/100";
        OxygenText.text = "Oxygen : " + (int)playerOxygen + "/100";

        if (gameTime <= 10)
        {
            TimeText.color = warnTextColor;
        }
        else
        {
            TimeText.color = defaultTextColor;
        }

        if (playerEnergy <= 10)
        {
            EnergyText.color = warnTextColor;
        }
        else
        {
            EnergyText.color = defaultTextColor;
        }

        if (playerOxygen <= 10)
        {
            OxygenText.color = warnTextColor;
        }
        else
        {
            OxygenText.color = defaultTextColor;
        }
    }


    private void pauseBtnClicked()
    {
        GameManager.instance.gamePause();
    }

    public void ShowIndicator(Npc npc)
    {
        Text newIndicator = Instantiate(IndicatorText);        

        Vector3 uiPosition = Camera.main.WorldToScreenPoint(NpcCheck.transform.position);
        newIndicator.transform.localPosition = uiPosition;
        newIndicator.transform.SetParent(this.gameObject.transform);        

        switch (npc)
        {
            case Npc.fish:
                newIndicator.text = "+5";
                newIndicator.color = Color.white;
                break;
            case Npc.jellyFish:                
                newIndicator.text = "-5";
                newIndicator.color = Color.red;
                break;
            case Npc.trash:                
                newIndicator.text = "-10";
                newIndicator.color = Color.red;
                break;
            case Npc.clock:                
                newIndicator.text = "+10s";
                newIndicator.color = Color.yellow;
                break;            
        }

        
    }
}
