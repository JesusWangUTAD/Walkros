using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//EL WALKROS CIEGO TAMBIÉN USA ESTE SCRIPT
public class WalkrosAraña : MonoBehaviour
{
    public GameObject Target;
    public Transform[] PatrolPoints;
    public bool bone = false;
    public Transform BoneDestination;
    public Transform StartWalkros;

    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
    }
    public void DIE() //Animation Event
    {
        SceneManager.LoadScene("6_Death"); //Death Scene. 
    }

    public void boneEventDone()
    {
        bone = true;
    }
}
