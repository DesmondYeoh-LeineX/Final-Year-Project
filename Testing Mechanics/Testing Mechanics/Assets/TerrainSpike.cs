using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpike : MonoBehaviour
{
    public Transform respawnPoint;
    public int terrainSpikeDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if(PlayerManager.instance.playerHealth - terrainSpikeDamage > 0)
            {
                PlayerManager.instance.player.transform.position = respawnPoint.position;
            }
            PlayerManager.instance.TakeDamage(terrainSpikeDamage);
        }
    }
}
