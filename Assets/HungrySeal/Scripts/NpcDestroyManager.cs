using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDestroyManager : MonoBehaviour
{   
    private void OnTriggerEnter(Collider npc)
    {
        switch (npc.tag)
        {
            case "Fish":
                GameManager.instance.addScore(npc.tag);                
                NpcSpawnManager.instance.ReturnObject(npc.gameObject);
                UIManger.instance.ShowIndicator(Npc.fish);
                break;
            case "JellyFish":
                GameManager.instance.addScore(npc.tag);                
                NpcSpawnManager.instance.ReturnObject(npc.gameObject);
                UIManger.instance.ShowIndicator(Npc.jellyFish);
                break;
            case "Trash":
                GameManager.instance.addScore(npc.tag);                
                NpcSpawnManager.instance.ReturnObject(npc.gameObject);
                UIManger.instance.ShowIndicator(Npc.trash);
                break;
            case "Clock":
                GameManager.instance.addScore(npc.tag);
                NpcSpawnManager.instance.ReturnObject(npc.gameObject);
                UIManger.instance.ShowIndicator(Npc.clock);
                break;
        }        
    }
}
