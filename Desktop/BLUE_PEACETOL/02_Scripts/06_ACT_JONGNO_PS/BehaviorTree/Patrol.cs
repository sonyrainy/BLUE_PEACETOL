using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;


public class Patrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPoint = 0;
    private NavMeshAgent agent;
    private Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (patrolPoints.Length > 0)
        {
            agent.destination = patrolPoints[currentPoint].position;
            animator.SetBool("isWalking", true); 
        }
    }

    private void Update()
    {
        if (CanSeeObject.isPlayerSpotted)
        {
            StopPatrol();
            return;
        }

        if (patrolPoints.Length == 0) return;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
            agent.destination = patrolPoints[currentPoint].position;
        }
    }

    public void StopPatrol()
    {
        agent.isStopped = true; 
        animator.SetBool("isWalking", false);
    }

    public void ResumePatrol()
    {
        agent.isStopped = false;
        agent.destination = patrolPoints[currentPoint].position; 
        animator.SetBool("isWalking", true);
    }
}
