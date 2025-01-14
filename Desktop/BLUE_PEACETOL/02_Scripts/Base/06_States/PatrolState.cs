using UnityEngine;

public class PatrolState : IEnemyState
{
    private readonly EnemyAI_Patrol enemy;

    public PatrolState(EnemyAI_Patrol enemy)
    {
        this.enemy = enemy;
    }

    public void EnterState()
    {
        enemy.Animator.SetBool("isWalking", true);
        enemy.Animator.SetBool("isRunning", false);
        enemy.Animator.SetBool("isAttacking", false);

        enemy.MoveToNextPatrolPoint();
    }

    public void UpdateState()
    {
        // 플레이어가 SafeArea에 있고 아직 감지되지 않은 경우
        if (!EnemyAI_Jongno.IsPlayerDetectedGlobally && enemy.PlayerInSafeArea())
        {
            // SafeArea에서는 계속 순찰
            if (!enemy.Agent.pathPending && enemy.Agent.remainingDistance < 0.5f)
            {
                enemy.MoveToNextPatrolPoint();
            }
            return;
        }

        // 플레이어가 감지 범위 내에 있고 SafeArea에 있지 않을 때 추적 상태로 전환
        if (enemy.PlayerInDetectionRange(enemy.detectionRange, enemy.fieldOfViewAngle))
        {
            EnemyAI_Jongno.IsPlayerDetectedGlobally = true;
            enemy.SetState(new ChaseState(enemy));
            return;
        }

        // 플레이어가 공격 범위 내에 있는 경우 공격 상태로 전환
        if (enemy.PlayerInAttackRange())
        {
            enemy.SetState(new AttackState(enemy));
        }

        // 순찰 유지
        if (!enemy.Agent.pathPending && enemy.Agent.remainingDistance < 0.5f)
        {
            enemy.MoveToNextPatrolPoint();
        }
    }

    public void ExitState()
    {
        enemy.Animator.SetBool("isWalking", false);
    }
}
