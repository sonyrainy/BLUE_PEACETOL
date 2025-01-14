using UnityEngine;

public class EnemyAI_Patrol : EnemyAI_Jongno
{
    public Transform[] patrolPoints;
    private int currentPatrolIndex;

    protected override void Start()
    {
        base.Start();
        SetState(new PatrolState(this)); 
    }

    public void MoveToNextPatrolPoint()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;

        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        Agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }
}
