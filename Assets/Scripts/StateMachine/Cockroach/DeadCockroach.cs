using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCockroach : StateMachineBehaviour
{
    //Aqui ira la animación de morir aplastada. Cuando este finalizada.
    //Cuando el objeto haya terminado su animación, se destruira.

    float timer;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;// Inicio el tiempo con el time delta time
        if (timer >= 3f)//El tiempo que tendra la animación
        {
            Destroy(animator.gameObject);// Destruyo el objeto
        }
    }
}
