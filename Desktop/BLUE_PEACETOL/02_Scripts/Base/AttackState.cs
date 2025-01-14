using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackState : IEnemyState
{
    private readonly EnemyAI enemy;

    public AttackState(EnemyAI enemy)
    {
        this.enemy = enemy;
    }

    public void EnterState()
    {
        enemy.Agent.isStopped = true;
        enemy.SetAnimatorParameter("isAttacking", true);
        Debug.Log($"{enemy.gameObject.name} entered AttackState.");

    }
public void UpdateState()
{
    if (enemy.PlayerInAttackRange())
    {
        // 플레이어가 공격 범위 안에 있는 경우
        enemy.LookAtPlayer();
        Debug.Log($"{enemy.gameObject.name} is attacking!");
        enemy.FireBullet();

    }
    else
    {
        // 플레이어가 공격 범위 밖으로 나간 경우 추적 상태로 전환
        Debug.Log($"{enemy.gameObject.name} lost attack range, switching to ChaseState.");
        enemy.SetState(new ChaseState(enemy));
    }
}


    public void ExitState()
    {
        enemy.SetAnimatorParameter("isAttacking", false);
    }
}
