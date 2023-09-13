using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertWolfOne : StateMachineBehaviour
{
    //public bool stealthOn = false; //Sigilo. ESTÁ EN EL GAMEMANAGER

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{        
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(GameManager.Instance.stealthOn == true)//Estado sigilo activado
       {
            Debug.Log("El Lobo detecta,pero jugador en sigilo");
       }
       if (GameManager.Instance.stealthOn == false)//Estado sigilo DESactivado
       {
            animator.SetBool("Detect", false);
            animator.SetBool("NoStealth", true);//Pasa al estado Attack
       }
    }

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
