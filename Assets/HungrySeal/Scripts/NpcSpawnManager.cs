using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Npc
{
    fish,
    jellyFish,
    trash,
    clock
}


public class NpcSpawnManager : MonoBehaviour
{
    public static NpcSpawnManager instance;

    public GameObject fish;
    public GameObject jellyBlue;
    public GameObject jellyOrange;
    public GameObject jellyWhite;
    public GameObject waterBottle;
    public GameObject beer;
    public GameObject soju;
    public GameObject cup;
    public GameObject toiletPaper;
    public GameObject clock;

    private Queue<GameObject> fishQueue = new Queue<GameObject>();
    private Queue<GameObject> jellyFishQueue = new Queue<GameObject>();
    private Queue<GameObject> trashQueue = new Queue<GameObject>();
    private Queue<GameObject> clockQueue = new Queue<GameObject>();

    private int standardValue = 10;


    [SerializeField]
    private int maxFishSpawnAmount = 30;    

    [SerializeField]
    private int maxJellyFishSpawnAmount = 15;    

    [SerializeField]
    private int maxTrashSpawnAmount = 15;   

    [SerializeField]
    private int maxClockSpawnAmount = 2;   


    private float minSpawnXValue = -40f;
    private float maxSpawnXValue = 40f;
    private float minSpawnYValue = -40;
    private float maxSpawnYValue = -10f;

    private void Awake()
    {
        instance = this;        
    }

    void Start()
    {       
        initQueue();        
    }
    
    void Update()
    {
        if (instance.fishQueue.Count > standardValue)
        {
            var obj = fishQueue.Dequeue();
            obj.gameObject.SetActive(true);
        }

        if (instance.jellyFishQueue.Count > standardValue)
        {
            var obj = jellyFishQueue.Dequeue();
            obj.gameObject.SetActive(true);
        }

        if (instance.trashQueue.Count > standardValue)
        {
            var obj = trashQueue.Dequeue();
            obj.gameObject.SetActive(true);
        }

        if (instance.clockQueue.Count > standardValue)
        {
            var obj = clockQueue.Dequeue();
            obj.gameObject.SetActive(true);
        }
    }

    private void initQueue()
    {
        for (int i = 1; i < maxFishSpawnAmount + standardValue; i++)
        {
            fishQueue.Enqueue(createNpc(Npc.fish));
        }

        for (int i = 1; i < maxJellyFishSpawnAmount + standardValue; i++)
        {
            jellyFishQueue.Enqueue(createNpc(Npc.jellyFish));
        }

        for (int i = 1; i < maxTrashSpawnAmount + standardValue; i++)
        {
            trashQueue.Enqueue(createNpc(Npc.trash));
        }

        for (int i = 1; i < maxClockSpawnAmount + standardValue; i++)
        {
            clockQueue.Enqueue(createNpc(Npc.clock));
        }
    }

    private GameObject createNpc(Npc npc)
    {
        GameObject npcGameObject = null;
        float randomXValue = Random.Range(minSpawnXValue, maxSpawnXValue);
        float randomYValue = Random.Range(minSpawnYValue, maxSpawnYValue);
        Vector3 randomSpawnPosition = new Vector3(randomXValue, randomYValue, 0);

        Vector3 defaultRotation = new Vector3(0f, 0f, 0f); ;
     
        switch (npc)
        {
            case Npc.fish:
                npcGameObject = fish;
                defaultRotation = new Vector3(0f, 90f, 0f);
                break;
            case Npc.jellyFish:
                npcGameObject = selectRandomJellyFish();
                break;
            case Npc.trash:
                npcGameObject = selectRandomTrash();
                break;
            case Npc.clock:
                npcGameObject = clock;
                defaultRotation = new Vector3(0f, -107.5f, 0f);
                break;
        }
        
        var queueObj = Instantiate(npcGameObject, randomSpawnPosition, Quaternion.EulerAngles(defaultRotation));
        queueObj.gameObject.SetActive(false);        
        return queueObj;
    }


    private GameObject selectRandomJellyFish()
    {
        GameObject randomJellyFish;
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
        return randomJellyFish;
    }


    private GameObject selectRandomTrash()
    {
        GameObject randomTrash;
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
        return randomTrash;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        switch (obj.gameObject.tag)
        {
            case "Fish":                               
                instance.fishQueue.Enqueue(obj);
                break;
            case "JellyFish":               
                instance.jellyFishQueue.Enqueue(obj);
                break;
            case "Trash":                
                instance.trashQueue.Enqueue(obj);
                break;
            case "Clock":                
                instance.clockQueue.Enqueue(obj);
                break;
        }        
    }
}
