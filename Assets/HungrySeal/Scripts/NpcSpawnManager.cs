using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawnManager : MonoBehaviour
{
    public GameObject fish;
    public GameObject jellyBlue;
    public GameObject jellyOrange;
    public GameObject jellyWhite;
    public GameObject waterBottle;
    public GameObject beer;
    public GameObject soju;
    public GameObject cup;
    public GameObject toiletPaper;

    private GameObject randomJellyFish;
    private GameObject randomTrash;


    private float minSpawnXValue = -40f;
    private float maxSpawnXValue = 40f;
    private float minSpawnYValue = -40;
    private float maxSpawnYValue = -10f;

    private int maxFishSpawnAmount = 30;
    private int currentFishAmount;

    private int maxJellyFishSpawnAmount = 15;
    private int currentJellyFishAmount;

    private int maxTrashSpawnAmount = 15;
    private int currentTrashAmount;



    // Start is called before the first frame update
    void Start()
    {
        currentFishAmount = GameObject.FindGameObjectsWithTag("Fish").Length;
        currentJellyFishAmount = GameObject.FindGameObjectsWithTag("JellyFish").Length;
        currentTrashAmount = GameObject.FindGameObjectsWithTag("Trash").Length;
    }

    // Update is called once per frame
    void Update()
    {
        currentFishAmount = GameObject.FindGameObjectsWithTag("Fish").Length;
        currentJellyFishAmount = GameObject.FindGameObjectsWithTag("JellyFish").Length;
        currentTrashAmount = GameObject.FindGameObjectsWithTag("Trash").Length;

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

        // Trash 생성
        if (maxTrashSpawnAmount > currentTrashAmount)
        {
            selectRandomTrash();
            createNpc(randomTrash);
        }
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
        float randomJellyFishValue = Random.Range(0.1f, 0.9f);
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


    private void selectRandomTrash()
    {
        float randomTrashValue = Random.Range(0.1f, 1f);
        if (randomTrashValue <= 0.2f)
        {
            randomTrash = waterBottle;
        }
        else if (randomTrashValue <= 0.4f)
        {
            randomTrash = beer;
        }
        else if (randomTrashValue <= 0.6f)
        {
            randomTrash = soju;
        }
        else if (randomTrashValue <= 0.8f)
        {
            randomTrash = cup;
        }
        else
        {
            randomTrash = toiletPaper;
        }
    }

}
