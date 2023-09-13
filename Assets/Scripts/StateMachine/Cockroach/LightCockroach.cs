using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LightCockroach : StateMachineBehaviour
{
    private NavMeshAgent AgentCockRoach;
    public GameObject Cockroach;
    public GameObject Light;
    private float distant;
    public GameObject Player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Light = GameObject.FindGameObjectWithTag("Firelight");//Le decimos cual es el gameobject de light
        Cockroach = animator.gameObject;
        AgentCockRoach = animator.gameObject.GetComponent<NavMeshAgent>();//Le decimos que utilize el componente del nav mesh agent
        AgentCockRoach.destination = Light.transform.position;// Le doy como destino la posicion del light
        Player = GameObject.FindGameObjectWithTag("Player");//Le decimos quien es el jugador
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distant = Vector3.Distance(Player.transform.position, Cockroach.transform.position);

        if (distant <= 2)// La distancia entre el jugador y la cucaracha
        {
            //Debug.Log("Moridura");
            animator.SetTrigger("DieCockroach");// si se cumple la otra función se activa el estado de muerte
        }


    }


}
