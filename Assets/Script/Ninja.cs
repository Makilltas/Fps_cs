using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class Ninja : MonoBehaviour
{
    public Transform target;
    public float runDistance = 5;
    public float runSpeed = 5;
    public float walkSpeed = 1;


    private NavMeshAgent agent;
    private Animator animator;

    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = walkSpeed;
    }

    
    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);

        var distance = Vector3.Distance(transform.position, target.position);

        if (distance >= runDistance ) 
        {
            agent.speed = runSpeed;
        }
        else agent.speed = walkSpeed;

        animator.SetBool("run", distance >= runDistance);

        if(target != null && agent.enabled)
        {
            agent.SetDestination(target.position);
        }



    }


    public void GetHurt()
    {

        animator.Play("Head Hit 1");
        StartCoroutine(StopAndwait());

    }

    IEnumerator StopAndwait()
    {
        agent.enabled = false;
        yield return new WaitForSeconds(2);
        agent.enabled = true;
    }
}
