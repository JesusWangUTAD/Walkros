using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Vuela : StateMachineBehaviour
{
    private NavMeshAgent AgentBird;
    private GameObject Point;
    private float distant;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AgentBird = animator.gameObject.GetComponent<NavMeshAgent>();
        Point= GameObject.FindGameObjectWithTag("BirdObject");
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AgentBird.destination = Point.transform.position;//Va a por el jugador.
        distant = Vector3.Distance(AgentBird.transform.position, Point.transform.position);//pa destruirse

        if (distant <= 2)
        {
            animator.SetTrigger("Destroy");
            // SceneManager.LoadScene("6_Death");//Vamos a la escena de MUERTE.
        }
    }
    }
