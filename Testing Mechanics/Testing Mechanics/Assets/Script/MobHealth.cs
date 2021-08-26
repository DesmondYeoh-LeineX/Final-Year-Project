using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobHealth : MonoBehaviour
{
    public int mobHealth;
    public int mobMaxHealth = 100;

    public float knockbackDuration;
    
    public Image healthBar;

    public bool isDead;
    public bool isBeingKnocked;
    
    public ParticleSystem deathParticles;
    public ParticleSystem halfhealthParticles;

    private Rigidbody2D mobRb;
    private Mob mob;
    [SerializeField] private float knockbackStrength;
    [SerializeField] private Vector2 knockbackDirection;



    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        isBeingKnocked = false;
        mobHealth = mobMaxHealth;
        mobRb = GetComponent<Rigidbody2D>();
        mob = GetComponent<Mob>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if(mobHealth > 0)
        {
            mobHealth -= damage;
            healthBar.fillAmount = mobHealth * 1.0f / mobMaxHealth;
        }

        if(mobHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    private void Death()
    {
        //play death animation
        deathParticles.transform.position = transform.position;
        deathParticles.Play();
        isDead = true;
        //after animation finish
        this.gameObject.SetActive(false);
    }

    public IEnumerator KnockBack()
    {
        if(mob.isFacingRight)
        {
            knockbackDirection = new Vector2 (-knockbackDirection.x, knockbackDirection.y);
            if(knockbackDirection.x > 0)
            {
                knockbackDirection = new Vector2 (-knockbackDirection.x, knockbackDirection.y);
            }
        }
        else
        {
            knockbackDirection = new Vector2 (knockbackDirection.x, knockbackDirection.y);
            if(knockbackDirection.x < 0)
            {
                knockbackDirection = new Vector2 (-knockbackDirection.x, knockbackDirection.y);
            }
        }
        // knockbackDirection = new Vector2 (knockbackDirection.x, knockbackDirection.y);
        mobRb.AddForce(knockbackDirection, ForceMode2D.Impulse);
        isBeingKnocked = true;
        yield return new WaitForSeconds(knockbackDuration);
        isBeingKnocked = false;
    }

}
