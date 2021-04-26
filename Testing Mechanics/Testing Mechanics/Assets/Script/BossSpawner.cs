using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject boss;

    private MobHealth bossHealth;
    private BoxCollider2D bossCollider;

    // Start is called before the first frame update
    void Start()
    {
        //bossCollider = boss.GetComponentInChildren<BoxCollider2D>();
        boss.SetActive(false);
        bossHealth = boss.GetComponent<MobHealth>();
        //bossCollider.enabled = !enabled;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(!bossHealth.isDead)
            {
                boss.SetActive(true);
                //bossCollider.enabled = enabled;
            }
        }
    }
}
