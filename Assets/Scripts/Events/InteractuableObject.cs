using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractuableObject : MonoBehaviour
{

    [Header("EVENTOS DE 1 CONDICIÓN")]
    public UnityEvent OnInteract;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        float dist = Vector3.Distance(player.transform.position, this.transform.position); //Distance
        if (dist < 1.5f && Input.GetButtonDown("Interaction"))
        {
            OnInteract.Invoke();
        }
    }



}
