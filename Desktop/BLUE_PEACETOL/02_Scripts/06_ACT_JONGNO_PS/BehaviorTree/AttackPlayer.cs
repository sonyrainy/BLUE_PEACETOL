using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class AttackPlayer : Action
{
    public SharedGameObject player;
    public float attackRange = 2f;

    [SerializeField] private Animator animator;

    public override void OnStart()
    {
        if (animator == null)
        {
            Debug.LogError("Animator 할당 X");
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (player.Value == null)
        {
            return TaskStatus.Failure;
        }

        // 적과 플레이어 거리 계산
        float distanceToPlayer = Vector3.Distance(transform.position, player.Value.transform.position);

        if (distanceToPlayer <= attackRange)
        {
            if (animator != null)
            {
                animator.SetBool("isAttacking", true);
            }
            return TaskStatus.Running;
        }
        else
        {
            // 플레이어가 범위 밖으로 벗어나면 다시 Chase
            if (animator != null)
            {
                animator.SetBool("isAttacking", false);
            }
            return TaskStatus.Failure;
        }
    }

    public override void OnEnd()
    {
        if (animator != null)
        {
            animator.SetBool("isAttacking", false);
        }
    }

    public override void OnReset()
    {
        player = null;
        attackRange = 2f;
    }
}
