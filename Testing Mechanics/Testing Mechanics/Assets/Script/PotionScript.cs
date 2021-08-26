using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour
{
    public int healAmt = 20;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
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
