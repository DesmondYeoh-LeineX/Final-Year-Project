using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobIdleBehaviour : StateMachineBehaviour
{
    Transform mobTransform;
    Transform player;
    Rigidbody2D rb;
    Mob mob;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       mob = animator.GetComponent<Mob>();
       mobTransform = animator.GetComponent<Transform>();
       rb = animator.GetComponent<Rigidbody2D>();
       player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(Vector2.Distance(player.position, mobTransform.position) > 2.0f)
       {
            animator.SetTrigger("Chase");
       } 
       mob.CheckPlayerDistanceForAttack();
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("Attack");
    }

}
