using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public List<GameObject> Mobs;

    private bool spawned;

    // Start is called before the first frame update
    void Start()
    {
        spawned = false;
        TurnOffMobs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(!spawned)
            {
                spawned = true;
                SpawnInMobs();
                //MusicCoordinator.instance.BossMusicBGM();
                //Debug.Log("play boss music");
                //bossCollider.enabled = enabled;
            }
        }
    }

    private void SpawnInMobs()
    {
        for(int i = 0; i < Mobs.Count; i++)
        {
            Mobs[i].SetActive(true);
        }
    }

    private void TurnOffMobs()
    {
        for(int i = 0; i < Mobs.Count; i++)
        {
            Mobs[i].SetActive(false);
        }
    }
}
