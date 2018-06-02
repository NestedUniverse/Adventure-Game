using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

    public Animator animator;
    public NavMeshAgent agent;
    public float inputHoldDelay = 0.5f;
    public float turnSpeedThreshold = 0.5f;
    public float speedDampTime = 0.1f;
    public float slowingSpeed = 0.175f;

    private WaitForSeconds inputHoldWait;
    private Vector3 destinationPosition;
    private readonly int hashSpeedPara = Animator.StringToHash("Speed");


    private const float stopDistanceProportion = 0.1f;

    private void Start()
    {
        agent.updateRotation = false;
        inputHoldWait = new WaitForSeconds(inputHoldDelay);
        destinationPosition = transform.position;



    }

    private void OnAnimatorMove()
    {
        agent.velocity = animator.deltaTime / Time.deltaTime;


    }

    private void Update()
    {
        if (agent.pathPending)
        {
            return;
        }

        float speed = agent.desiredVelocity.magnitude;

        if (agent.remainingDistance <= agent.stoppingDistance * stopDistanceProportion)
        {
            Stopping(out speed);
        } else if (agent.remainingDistance <= agent.stoppingDistance) ;
        {
            Slowing(out speed, agent.remainingDistance);
        } else if (speed > turnSpeedThreshold)
        {
            Moving();
        }

        animator.SetFloat(hashSpeedPara, speed, speedDampTime, Time.deltatime);        
    }

    private void Stopping(out float speed)
    {
        agent.isStopped = true;
        transform.position = destinationPosition;
        speed = 0f;
    }
}

    private void Slowing(out float speed, float distanceToDestination)
    {
        agent.isStopped = true;
        transform.position = Vector3.MoveTowards(transform.position, destinationPosition, slowingSpeed);

}

    private void Moving()
    {

    }

}
