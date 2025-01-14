using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI_Jongno : EnemyAI
{
    public Transform[] safeAreas;
    public static bool IsPlayerDetectedGlobally = false; // 전역 감지 여부
    public float detectionRange = 15f; // 감지 거리
    public float fieldOfViewAngle = 150f; // 시야각
    protected override void InitializeAnimationParameters()
    {
        animationParameters = new Dictionary<string, object>
        {
            { "isWalking", false },
            { "isRunning", false },
            { "isAttacking", false }
        };
    }
    protected override void Start()
    {
        base.Start();

        // EnemyManager에 등록
        if (EnemyManager_Jongno.instance != null)
        {
            EnemyManager_Jongno.instance.RegisterEnemy(this);
            Debug.Log($"{gameObject.name} registered to EnemyManager.");
        }    
        else
        {
            Debug.LogError("EnemyManager_Jongno instance is null! Enemy registration failed.");
        }
    }

    public override void PerformAttack()
    {
        Debug.Log($"{gameObject.name} is performing an attack!");

        // 공격 애니메이션 설정
        SetAnimatorParameter("isAttacking", true);
        FireBullet(); // 총알 발사

        // EnemyManager에 공격 이벤트 알림
        if (EnemyManager_Jongno.instance != null)
        {
            Debug.Log($"{gameObject.name} is calling NotifyAttack.");
            EnemyManager_Jongno.instance.NotifyAttack();
        }
        else
        {
            Debug.LogError("EnemyManager_Jongno instance is null. Cannot notify attack.");
        }
    }

    public override void StartChasingPlayer()
    {
        // 부모 클래스의 기본 동작 호출
        base.StartChasingPlayer();

        // 종로 전용 추가 동작
        Debug.Log($"{gameObject.name} is chasing the player with Jongno-specific logic!");

        // 예: 종로에서 적들이 추적 시작 시 추가 효과
        if (EnemyManager_Jongno.instance != null)
        {
            EnemyManager_Jongno.instance.NotifyAttack(); // 다른 적들에게 감지 상태 알림
        }
    }

    public bool PlayerInSafeArea()
    {
        foreach (Transform safeArea in safeAreas)
        {
            Collider safeAreaCollider = safeArea.GetComponent<Collider>();
            if (safeAreaCollider != null && safeAreaCollider.bounds.Contains(player.position))
            {
                return true;
            }
        }
        return false;
    }
    public override void FireBullet()
    {
        base.FireBullet(); // 기본 FireBullet 동작 유지

        Debug.Log("자식게 먼저되네?");

        // if (Time.time < nextFireTime) return;

        // EnemyManager에 공격 알림
        if (EnemyManager_Jongno.instance != null)
        {
            Debug.Log($"{gameObject.name} is notifying other enemies.");
            EnemyManager_Jongno.instance.NotifyAttack();
        }
        else
        {
            Debug.LogError("EnemyManager_Jongno instance is null.");
        }
    }
    public bool PlayerInDetectionRange(float detectionRange, float fieldOfViewAngle)
    {
        if (player == null) return false;

        if (PlayerInSafeArea() && !IsPlayerDetectedGlobally) return false;

        float distance = Vector3.Distance(transform.position, player.position);
        Vector3 direction = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, direction);

        return distance <= detectionRange && angle <= fieldOfViewAngle / 2;
    }
        
     // Gizmos를 사용하여 시야 범위 표시
    private void OnDrawGizmosSelected()
    {
        if (player == null) return;

        // 감지 범위 원을 그림
        Gizmos.color = new Color(1, 0, 0, 0.3f); // 반투명 빨간색
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // 시야각을 표시하기 위한 시작 방향
        Vector3 forward = transform.forward;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-fieldOfViewAngle / 2, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(fieldOfViewAngle / 2, Vector3.up);

        // 시야각의 양 끝 레이 방향 계산
        Vector3 leftRayDirection = leftRayRotation * forward * detectionRange;
        Vector3 rightRayDirection = rightRayRotation * forward * detectionRange;

        // 시야각 레이를 그림
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, leftRayDirection);
        Gizmos.DrawRay(transform.position, rightRayDirection);

        // 시야각 내부를 메쉬로 표시할 수도 있음 (선택 사항)
    }
}
