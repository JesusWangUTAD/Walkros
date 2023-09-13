using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScapeCockroach : StateMachineBehaviour
{

    private NavMeshAgent AgentCockRoach;
    private float distant;
    public GameObject Player;
    private GameObject Exit;
    public GameObject Cockroach;
    float timer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;// Le decimos que la variable tiempo es igual 0  
        Player = GameObject.FindGameObjectWithTag("Player");//Le decimos quien es el jugador
        Cockroach = animator.gameObject;
        AgentCockRoach = animator.gameObject.GetComponent<NavMeshAgent>();//Le decimos que utilize el componente del nav mesh agent
        Exit = GameObject.FindGameObjectWithTag("ExitCockroach");//Le digo donde es la salida
        AgentCockRoach.destination = Exit.transform.position;// le especifico el punto exacto
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        timer += Time.deltaTime;//Inicia el timepo 
        distant = Vector3.Distance(Player.transform.position, Cockroach.transform.position);//Mira la distancia entre el Player y la cucaracha

        if (!AgentCockRoach.pathPending && AgentCockRoach.remainingDistance < 0.5f)
        {
            if (distant >= 3) //Si el jugador esta a más de 3 metros
            {
                if (timer >= 8f)// Si el tiempo es mayor o igual a 8 segundos
                {
                    animator.SetBool("Runcockroach", false);   // Vuelve al estado Iddle          
                }
            }
        }
        


        //distant = Vector3.Distance(Player.transform.position, Cockroach.transform.position); //Espacio entre cockraoch y Player

        //if (distant <= 3) //Si el jugador esta a menos de 3 metros
        //{
        //    animator.SetBool("Runcockroach", true); // Pasa al estado de huida
        //}
    }

 
}
