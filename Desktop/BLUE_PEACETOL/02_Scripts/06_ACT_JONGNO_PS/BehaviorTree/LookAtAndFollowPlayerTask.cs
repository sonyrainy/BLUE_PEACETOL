using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class LookAtAndFollowPlayerTask : Action
{
    public SharedTransform player;       
    public float rotationSpeed = 5f;           
    public float followSpeed = 3f;              
    public float stopDistance = 2f;            
    public GameObject patrolPoint;             

    private Animator animator;
    private bool patrolPointDisabled = false;   

    public override void OnStart()
    {
        animator = GetComponent<Animator>();
        patrolPointDisabled = false;            
    }

    public override TaskStatus OnUpdate()
    {
        if (animator != null)
        {
            // 플레이어 방향을 계산 후, 적을 회전
            Vector3 direction = player.Value.position - transform.position;
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            float distance = Vector3.Distance(transform.position, player.Value.position);

            if (distance > stopDistance)
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isAttacking", false);

                if (patrolPoint != null && patrolPointDisabled)
                {
                    patrolPoint.SetActive(true);
                    patrolPointDisabled = false;
                }

                transform.position = Vector3.MoveTowards(transform.position, player.Value.position, Time.deltaTime * followSpeed);
            }
            else
            {
                // 공격 범위에 도달한 경우
                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", true);

                // 순찰 지점을 한 번만 비활성화
                if (patrolPoint != null && !patrolPointDisabled)
                {
                    patrolPoint.SetActive(false);
                    patrolPointDisabled = true; // 순찰 지점이 비활성화되었음을 추적
                }
            }

            return TaskStatus.Running;  // 동작을 계속 유지
        }

        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
        // 태스크가 끝나면 애니메이션 상태 초기화
        if (animator != null)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", false);
        }

        // 순찰 지점을 다시 활성화
        if (patrolPoint != null)
        {
            patrolPoint.SetActive(true);
            patrolPointDisabled = false;
        }
    }
}
