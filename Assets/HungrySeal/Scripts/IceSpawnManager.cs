using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpawnManager : MonoBehaviour
{
    public GameObject ice1;
    public GameObject ice2;
    public GameObject ice3;

    private float randomSelectValue;
    private float randomDelayTime;

    private float delayTimeMin = 7f;
    private float delayTimeMax = 15f;


    // Start is called before the first frame update
    void Start()
    {
        randomSelectValue = Random.Range(0f, 0.9f);
        randomDelayTime = Random.RandomRange(delayTimeMin, delayTimeMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (randomDelayTime < 0)
        {
            resetRandomValues();
            createIce(randomSelectValue);                        
        }
        else
        {
            randomDelayTime -= 1f * Time.deltaTime;
        }
    }


    private void createIce(float selectValue)
    {
        if (selectValue <= 0.3f)
        {
            Instantiate(ice1, transform.position, Quaternion.identity);
        }
        else if (selectValue <= 0.6f) {
            Instantiate(ice2, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(ice3, transform.position, Quaternion.identity);
        }
        
    }

    private void resetRandomValues()
    {
        randomSelectValue = Random.Range(0f, 0.9f);
        randomDelayTime = Random.RandomRange(delayTimeMin, delayTimeMax);
    }
}
