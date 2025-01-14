using UnityEngine;

public class EnemyAI_Stand : EnemyAI_Jongno
{
    protected override void Start()
    {
        base.Start();
        SetState(new IdleState(this)); // Idle 상태로 시작
    }
}
