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
                GameManager.instance.addScore(npc.tag);
                Destroy(npc.gameObject);
                break;
            case "JellyFish":
                GameManager.instance.addScore(npc.tag);
                Destroy(npc.gameObject);
                break;
            case "Trash":
                GameManager.instance.addScore(npc.tag);
                Destroy(npc.gameObject);
                break;
            case "Clock":
                GameManager.instance.addScore(npc.tag);
                Destroy(npc.gameObject);
                break;

        }        
    }
}
