using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
	public Transform player;
    
    public float attackRange = 2.5f;
    public float attackCooldown = 3.0f;
    public int attackDamage = 20;
    //public int enragedDamage = 40;
    public Vector3 attackOffset;
    public float attackRadius = 1.25f;
    public LayerMask attackMask;
    
    public float touchRadius = 0.6f;
    public int touchDamage = 10;
    
    public float speed = 2.5f;


	public bool isFacingRight = true;

    private bool isAttacking = false;

    private Animator bossAnim;
    private MobHealth mobHealthScript;

    private void Start() 
    {
        bossAnim = GetComponent<Animator>();
        mobHealthScript = GetComponent<MobHealth>();
    }

    private void Update() 
    {
        // if(Vector2.Distance(transform.position, player.position) <= touchRadius)
        // {
        //     TouchHit();
        // }
        TouchHit();
    }

	public void LookAtPlayer()
	{
        if(!mobHealthScript.isBeingKnocked)
        {
            Vector3 flipped = transform.localScale;
            flipped.z *= -1f;

            if (transform.position.x > player.position.x && isFacingRight)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFacingRight = false;
            }
            else if (transform.position.x < player.position.x && !isFacingRight)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFacingRight = true;
            }
        }
	}

    public void CheckPlayerDistanceForAttack()
    {
        if(Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            StartCoroutine(Attack());
        }

    }

    private void TouchHit()
    {
        Collider2D colInfo = Physics2D.OverlapCircle(transform.position, touchRadius, attackMask);
        if(colInfo != null)
        {
            PlayerManager.instance.TakeDamage(touchDamage);
            Debug.Log("Touched");
        }
    }

    public void CheckHit()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRadius, attackMask);
        if(colInfo != null)
        {
            PlayerManager.instance.TakeDamage(attackDamage);
        }
    }
    public IEnumerator Attack()
    {
        if(!isAttacking && !mobHealthScript.isBeingKnocked)
        {
            isAttacking = true;
            bossAnim.SetTrigger("Attack");
            yield return new WaitForSeconds(attackCooldown);
            isAttacking = false;
        }
        else
        {
            yield return null;
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(
            new Vector3(transform.position.x + attackOffset.x, transform.position.y + attackOffset.y, transform.position.z), 
            attackRadius
            );
        Gizmos.DrawWireSphere(transform.position, touchRadius);    
    }
}
