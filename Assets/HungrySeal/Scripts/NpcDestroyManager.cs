using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDestroyManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider npc)
    {
        if (npc.tag == "Fish")
        {         
            // 점수 함수.
            Destroy(npc.gameObject);
        }

        if (npc.tag == "JellyFish")
        {            
            // 점수 함수.
            Destroy(npc.gameObject);
        }

        if (npc.tag == "Trash")
        {            
            // 점수 함수.
            Destroy(npc.gameObject);
        }
    }
}
