using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour
{
    public int healAmt = 20;
    private bool healed;
    // Start is called before the first frame update
    void Start()
    {
        // to prevent double call
        healed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(!healed)
            {
                healed = true;
                int health = PlayerManager.instance.playerHealth;
                int maxHealth = PlayerManager.instance.playerMaxHealth;
                if(health < maxHealth)
                {
                    health += healAmt;
                    if (health > maxHealth)
                    {
                        health = maxHealth;
                    }
                    PlayerManager.instance.playerHealth = health;
                    UIManager.instance.UpdateHealthBar();
                }
                PlayerManager.instance.HealEffect();
                transform.parent.gameObject.SetActive(false);
            }
        }
    }

    
}
