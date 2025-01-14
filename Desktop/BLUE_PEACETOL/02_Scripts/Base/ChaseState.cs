using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chase → Attack
public class ChaseState : IEnemyState
{
    private readonly EnemyAI enemy;

    public ChaseState(EnemyAI enemy)
    {
        this.enemy = enemy;
    }

    public void EnterState()
    {
        Debug.Log("체이스중인가?");
        enemy.Agent.isStopped = false;
        enemy.SetAnimatorParameter("isRunning", true);
    }

    public void UpdateState()
    {
        if (enemy.PlayerInAttackRange())
        {
            enemy.SetState(new AttackState(enemy));
        }
        else
        {
            enemy.Agent.SetDestination(enemy.Player.position);
        }
    }

    public void ExitState()
    {
        enemy.SetAnimatorParameter("isRunning", false);
    }
}
