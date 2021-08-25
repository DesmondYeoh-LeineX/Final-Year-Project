using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : StateMachineBehaviour
{
    Transform bossTransform;
    Transform player;
    Rigidbody2D rb;
    Boss boss;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       boss = animator.GetComponent<Boss>();
       bossTransform = animator.GetComponent<Transform>();
       rb = animator.GetComponent<Rigidbody2D>();
       player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       boss.LookAtPlayer();
       if(Vector2.Distance(player.position, bossTransform.position) > 2.0f)
       {
         Vector2 target = new Vector2 (player.position.x, rb.position.y);
         bossTransform.position = Vector2.MoveTowards(rb.position, target, boss.speed * Time.fixedDeltaTime);
       } 
       //rb.MovePosition(newPos);

       boss.CheckPlayerDistanceForAttack();
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("Attack");
    }


}
