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
            Debug.Log("NPC Collide");
            // 점수를 올리는 함수.
            Destroy(npc.gameObject);
        }        
    }
}
