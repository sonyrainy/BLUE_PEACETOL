using UnityEngine;

public class DeadState : IEnemyState
{
    private readonly EnemyAI enemy;
    private readonly EnemyHealthBase healthBase;

    public DeadState(EnemyAI enemy, EnemyHealthBase healthBase)
    {
        this.enemy = enemy;
        this.healthBase = healthBase;
    }

    public void EnterState()
    {
        Debug.Log($"{enemy.gameObject.name} entered DeadState.");

        // 사망 애니메이션 재생
        if (enemy.Animator != null)
        {
            enemy.SetAnimatorParameter("isDead", true);
        }

        // 무기 스폰
        healthBase.SpawnGunOnDeath(enemy.transform.position, enemy.transform.rotation);

        // 일정 시간 후 객체 풀로 반환
        enemy.StartCoroutine(DeactivateAfterDelay(3f));
    }

    public void UpdateState()
    {
        // DeadState에서는 아무 행동도 하지 않음
    }

    public void ExitState()
    {
        Debug.Log($"{enemy.gameObject.name} exited DeadState.");
    }

    private System.Collections.IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 적을 비활성화하기 전에 SpawnManager에게 알림
        EnemySpawnManager.Instance.RemoveEnemy(enemy.gameObject);

        // 오브젝트를 풀로 반환
        ObjectPoolManager.instance.ReturnToPool(enemy.gameObject);
    }
}
