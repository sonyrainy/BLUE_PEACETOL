using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ChasePlayer : Action
{

    //target: PLAYER
    public SharedGameObject target;
    private NavMeshAgent agent;
    private Animator animator;

    public override void OnStart()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (target != null)
        {
            agent.isStopped = false;
            agent.destination = target.Value.transform.position;
            animator.SetBool("isRunning", true);
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (target == null)
        {
            return TaskStatus.Failure;
        }

        agent.destination = target.Value.transform.position;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        if (animator != null)
        {
            animator.SetBool("isRunning", false);
        }
    }
}
