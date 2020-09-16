using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawnManager : MonoBehaviour
{
    public GameObject fish;
    public GameObject jellyBlue;
    public GameObject jellyOrange;
    public GameObject jellyWhite;


    private int maxFishSpawnAmount = 30;
    private int currentFishAmount;

    private int maxJellyFishSpawnAmount = 15;
    private int currentJellyFishAmount;    
    private GameObject randomJellyFish;



    private float minSpawnXValue = -40f;
    private float maxSpawnXValue = 40f;
    private float minSpawnYValue = -40f;
    private float maxSpawnYValue = -10f;

    


    // Start is called before the first frame update
    void Start()
    {
        currentFishAmount = GameObject.FindGameObjectsWithTag("Fish").Length;
        currentJellyFishAmount = GameObject.FindGameObjectsWithTag("JellyFish").Length;
    }

    // Update is called once per frame
    void Update()
    {
        currentFishAmount = GameObject.FindGameObjectsWithTag("Fish").Length;
        currentJellyFishAmount = GameObject.FindGameObjectsWithTag("JellyFish").Length;

        // Fish 생성
        if (maxFishSpawnAmount > currentFishAmount)
        {
            createNpc(fish);
        }


        // JellyFish 생성
        if (maxJellyFishSpawnAmount > currentJellyFishAmount)
        {
            selectRandomJellyFish();
            createNpc(randomJellyFish);
        }


        Debug.Log("Fish amount = " + currentFishAmount);
    }


    private void createNpc(GameObject npcGameObject)
    {        
        float randomXValue = Random.Range(minSpawnXValue, maxSpawnXValue);
        float randomYValue = Random.Range(minSpawnYValue, maxSpawnYValue);
        Vector3 randomSpawnPosition = new Vector3(randomXValue, randomYValue, 0);

        Vector3 defaultRotation = new Vector3(0f, 0f, 0f); ;
        // 이부분 나중에 enum으로 바꿀것
        if (npcGameObject == fish)
        {
            defaultRotation = new Vector3(0f, 90f, 0f);
        }       

        Instantiate(npcGameObject, randomSpawnPosition, Quaternion.Euler(defaultRotation));
    }


    private void selectRandomJellyFish()
    {
        float randomJellyFishValue = Random.Range(0f, 0.9f);
        if (randomJellyFishValue <= 0.3f)
        {
            randomJellyFish = jellyBlue;
        }
        else if(randomJellyFishValue <= 0.6f)
        {
            randomJellyFish = jellyOrange;
        }
        else
        {
            randomJellyFish = jellyWhite;
        }
    }
    
}
