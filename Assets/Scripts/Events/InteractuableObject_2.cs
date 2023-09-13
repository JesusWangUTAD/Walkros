using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractuableObject_2 : MonoBehaviour
{

    [Header("EVENTOS DE 2 CONDICIONES")]
    public UnityEvent OnFilledCondition;
    public UnityEvent OnUnfilledCondition;

    public bool booleanValueToCompare = false; //por defecto empieza en false
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        float dist = Vector3.Distance(player.transform.position, this.transform.position); //Distance
        if (dist < 2f && Input.GetButtonDown("Interaction"))
        {
            switch (booleanValueToCompare)
            {
                case true:
                    {
                        OnFilledCondition.Invoke();
                        break;
                    }
                case false:
                    {
                        OnUnfilledCondition.Invoke();
                        break;
                    }
            }
        }
    }

    public void ChangeBoolValue(bool boolValue)
    {
        if (boolValue == true)
        {
            booleanValueToCompare = true;
        }
        else
        {
            booleanValueToCompare = false;
        }
    }
}
