using System.Collections;
using UnityEngine;

public class ClimbState : IEnemyState
{
    private readonly EnemyAI_Final enemy;

    public ClimbState(EnemyAI_Final enemy)
    {
        this.enemy = enemy;
    }

    public void EnterState()
    {
        enemy.Agent.isStopped = true;

        Vector3 startPos = enemy.transform.position;
        Vector3 endPos = enemy.Agent.currentOffMeshLinkData.endPos;

        if (endPos.y > startPos.y)
        {
            // 올라가는 애니메이션 설정
            enemy.SetClimbingState(isClimbing: true, isClimbingDown: false);
        }
        else
        {
            // 내려가는 애니메이션 설정
            enemy.SetClimbingState(isClimbing: false, isClimbingDown: true);
        }

        enemy.StartCoroutine(HandleClimbing(endPos));
    }

    public void UpdateState()
    {
        // Climbing 동안 Update는 사용하지 않음
    }

    public void ExitState()
    {
        // Climbing 종료 시 애니메이션 리셋
        enemy.SetClimbingState(isClimbing: false, isClimbingDown: false);
    }

    private IEnumerator HandleClimbing(Vector3 endPos)
    {
        while (Vector3.Distance(enemy.transform.position, endPos) > 0.1f)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, endPos, enemy.climbSpeed * Time.deltaTime);
            yield return null;
        }

        enemy.Agent.CompleteOffMeshLink(); // OffMeshLink 이동 완료
        enemy.Agent.isStopped = false;

        // 추적 상태로 복귀
        enemy.SetState(new ChaseState(enemy));
    }
}
