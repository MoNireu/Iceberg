using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawnManager : MonoBehaviour
{
    public GameObject fish;

    private int maxFishSpawnAmount = 30;
    private int currentfishAmount;

    private float minSpawnXValue = -40f;
    private float maxSpawnXValue = 40f;
    private float minSpawnYValue = -40f;
    private float maxSpawnYValue = -10f;

    


    // Start is called before the first frame update
    void Start()
    {
        currentfishAmount = GameObject.FindGameObjectsWithTag("Fish").Length;        
    }

    // Update is called once per frame
    void Update()
    {
        currentfishAmount = GameObject.FindGameObjectsWithTag("Fish").Length;

        // Fish 생성
        if (maxFishSpawnAmount > currentfishAmount)
        {
            createNpc(fish);
        }       


        Debug.Log("Fish amount = " + currentfishAmount);
    }


    private void createNpc(GameObject npcGameObject)
    {        
        float randomXValue = Random.Range(minSpawnXValue, maxSpawnXValue);
        float randomYValue = Random.Range(minSpawnYValue, maxSpawnYValue);
        Vector3 randomSpawnPosition = new Vector3(randomXValue, randomYValue, 0);
        Vector3 defaultRotation = new Vector3(0f, 90f, 0f);

        Instantiate(npcGameObject, randomSpawnPosition, Quaternion.Euler(defaultRotation));
    }
    
}
