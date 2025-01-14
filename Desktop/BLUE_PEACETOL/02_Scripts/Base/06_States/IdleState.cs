using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IdleState : IEnemyState
{
    private readonly EnemyAI_Stand enemy;

    public IdleState(EnemyAI_Stand enemy)
    {
        this.enemy = enemy;
    }

    public void EnterState()
    {
        enemy.Animator.SetBool("isIdle", true);
        enemy.Agent.isStopped = true; // 대기 상태에서는 멈춤
    }

    public void UpdateState()
    {
        // SafeArea 안에 있는 경우 아무것도 하지 않음
        if (!EnemyAI_Jongno.IsPlayerDetectedGlobally && enemy.PlayerInSafeArea())
        {
            return;
        }

        // SafeArea 밖에서 플레이어가 감지되면 추적 상태로 전환
        if (enemy.PlayerInDetectionRange(enemy.detectionRange, enemy.fieldOfViewAngle))
        {
            EnemyAI_Jongno.IsPlayerDetectedGlobally = true;
            enemy.SetState(new ChaseState(enemy));
            return;
        }

        // 공격 범위 내에 있으면 공격 상태로 전환
        if (enemy.PlayerInAttackRange())
        {
            enemy.SetState(new AttackState(enemy));
        }
    }

    public void ExitState()
    {
        enemy.Animator.SetBool("isIdle", false);
    }
}
