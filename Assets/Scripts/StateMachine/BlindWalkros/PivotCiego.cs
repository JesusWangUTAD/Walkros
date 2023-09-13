using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotCiego : StateMachineBehaviour
{
    public float speed = 5f;
    float timer;

    private Transform agentTransform;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        agentTransform = animator.gameObject.GetComponent<Transform>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        animator.gameObject.transform.Rotate(new Vector3(0, 10, 0) * Time.deltaTime * speed);
        RaycastHit raycastHit;
        if (Physics.Raycast((new Vector3(agentTransform.position.x, agentTransform.position.y + 1f, agentTransform.position.z)), agentTransform.TransformDirection(Vector3.forward), out raycastHit, 15f)) // 5 METROS
        {
            if (raycastHit.collider.tag == "Player") //Hit
            {
                Debug.Log("attack");
                animator.SetBool("Attack", true); //Attack state
                animator.SetBool("Pivot", false);
            }
        }
        Debug.DrawRay((new Vector3(agentTransform.position.x, agentTransform.position.y + 1f, agentTransform.position.z)), agentTransform.transform.forward * 15f, Color.blue);
        if (timer >= 8f)
        {

            animator.SetBool("Pivot", false);
            animator.SetBool("Attack", false);
            animator.SetBool("Leave", true);

        }

    }
}
