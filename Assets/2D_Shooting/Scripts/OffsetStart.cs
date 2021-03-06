using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ss;

public class OffsetStart : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo state, int layerIndex)
    {
        Player player = GameManager.I._player;
        Vector3 offset = new Vector3();

        if (state.IsName("Melee1"))
        {
            offset = player._offset_melee;
        }
        else if (state.IsName("Shoot"))
        {
            offset = player._offset_shoot;
        }
        else if (state.IsName("Move"))
        {
            offset = player._offset_move;
        }

        if (player._flip == true)
        {
            offset.y *= -1;
        }
        player._sword_sp.transform.localPosition = offset;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
