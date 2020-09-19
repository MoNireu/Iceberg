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
        switch (npc.tag)
        {
            case "Fish":
                // 점수 함수.
                Destroy(npc.gameObject);
                break;
            case "JellyFish":
                // 점수 함수.
                Destroy(npc.gameObject);
                break;
            case "Trash":
                // 점수 함수.
                Destroy(npc.gameObject);
                break;
            case "Clock":
                // 점수 함수.
                Destroy(npc.gameObject);
                break;

        }        
    }
}
