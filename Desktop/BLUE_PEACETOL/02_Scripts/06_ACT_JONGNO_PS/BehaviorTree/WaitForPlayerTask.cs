using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class WaitForPlayerTask : Action
{
    public SharedFloat waitTime = 2.0f;  // 대기 시간, 기본값은 2초
    private float startTime;
    private Animator animator;

    public override void OnStart()
    {
        startTime = Time.time;
        animator = GetComponent<Animator>();

        // Idle 애니메이션 시작
        if (animator != null)
        {
            animator.SetBool("isIdle", true);
        }
    }

    public override TaskStatus OnUpdate()
    {
        // 대기 시간이 경과하면 성공 반환
        if (Time.time - startTime >= waitTime.Value)
        {
            if (animator != null)
            {
                animator.SetBool("isIdle", false);
            }
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        // Idle 상태 종료
        if (animator != null)
        {
            animator.SetBool("isIdle", false);
        }
    }
}
