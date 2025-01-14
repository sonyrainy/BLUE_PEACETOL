using System.Collections;
using UnityEngine;

public abstract class EnemyHealthBase : MonoBehaviour
{
    public int health = 3;
    protected Animator animator;
    protected EnemyAI_Final enemyAI;

    protected virtual void Start()
    {
        enemyAI = GetComponent<EnemyAI_Final>();
        if (enemyAI == null)
        {
            Debug.LogWarning($"EnemyAI component is missing on {gameObject.name}");
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning($"Animator component is missing on {gameObject.name}");
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} is dying...");

        if (enemyAI != null)
        {
            enemyAI.SetState(new DeadState(enemyAI, this)); // DeadState로 전환
        }
        else
        {
            Debug.LogWarning("EnemyAI is null, cannot set DeadState.");
        }
    }

    // 적별로 고유한 무기 드랍 처리
    public abstract void SpawnGunOnDeath(Vector3 position, Quaternion rotation);
}
