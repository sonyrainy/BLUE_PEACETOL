using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyAI_Final : EnemyAI
{
    public float climbSpeed = 0.01f;



    // 애니메이션 파라미터 초기화
    protected override void InitializeAnimationParameters()
    {
        animationParameters = new Dictionary<string, object>
        {
            { "isRunning", false },
            { "isAttacking", false },
            { "isClimbing", false },
            { "isClimbingDown", false },
            { "Die", false }
        };
    }

    protected override void Start()
    {
        base.Start();

        // 기본 상태가 Chase State
        SetState(new ChaseState(this));
    }

    protected override void Update()
    {
        currentState?.UpdateState();

        if (agent.isOnOffMeshLink)
        {
            SetState(new ClimbState(this));
        }
    }

    public void SetClimbingState(bool isClimbing, bool isClimbingDown)
    {
        animationParameters["isRunning"] = false;
        animationParameters["isAttacking"] = false;
        animationParameters["isClimbing"] = isClimbing;
        animationParameters["isClimbingDown"] = isClimbingDown;
        animationParameters["Die"] = false;
        ApplyAnimationParameters();
    }

    public void SetDeadState()
    {
        animationParameters["isRunning"] = false;
        animationParameters["isAttacking"] = false;
        animationParameters["isClimbing"] = false;
        animationParameters["isClimbingDown"] = false;
        animationParameters["Die"] = true;
        ApplyAnimationParameters();

        Debug.Log($"{gameObject.name} has been set to dead state.");
        StartCoroutine(HandleDeathSequence());
    }

    public void SetRunningState()
    {
        animationParameters["isRunning"] = true;
        animationParameters["isAttacking"] = false;
        animationParameters["isClimbing"] = false;
        animationParameters["isClimbingDown"] = false;
        animationParameters["Die"] = false;
        ApplyAnimationParameters();
    }

    public override void PerformAttack()
    {
        // Debug.Log("PerformAttack() called.");

        if (!PlayerInAttackRange())
        {
            Debug.Log($"{gameObject.name} cannot attack, player is out of range.");
            return;
        }

        animationParameters["isAttacking"] = true;
        ApplyAnimationParameters();

    }

    public virtual void Die()
    {
        animationParameters["Die"] = true;
        ApplyAnimationParameters();

        Debug.Log($"{gameObject.name} is dying.");
        StartCoroutine(HandleDeathSequence());
    }

    private IEnumerator HandleDeathSequence()
    {
        yield return new WaitForSeconds(3f);
        ObjectPoolManager.instance.ReturnToPool(gameObject);
    }

    protected virtual GameObject GetMuzzleFlash()
    {
        return null;
    }

    protected virtual float GetMuzzleFlashDuration()
    {
        return 0.2f;
    }
}
