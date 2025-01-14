using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public abstract class EnemyAI : MonoBehaviour
{
    protected NavMeshAgent agent;
    public NavMeshAgent Agent => agent;

    protected Animator animator;
    public Animator Animator => animator;

    protected Transform player;
    public Transform Player => player;

    protected IEnemyState currentState;

    // 공격 범위
    public float attackRange = 15.0f;
    public float nextFireTime = 1.0f;
    public float fireRate = 1.0f;
    public GameObject bulletPrefab;
    public Transform firePoint; 

    // 발사 소리 이름 필드 추가
    protected string fireSoundName = "default_fire_sound";



    // 애니메이션 parameters
    protected Dictionary<string, object> animationParameters = new Dictionary<string, object>();

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        GameObject playerObject = GameObject.FindWithTag("PLAYER");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        
        InitializeAnimationParameters();

    }
    protected abstract void InitializeAnimationParameters();

    protected virtual void Update()
    {
        currentState?.UpdateState();
    }

    public void SetState(IEnemyState newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public virtual void FireBullet()
    {

        //Debug.Log("되고 있나? 총쏘기");
        if (Time.time < nextFireTime) return;

        nextFireTime = Time.time + fireRate;

        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning($"Bullet or FirePoint is missing on {gameObject.name}");
            return;
        }

        GameObject bullet = ObjectPoolManager.instance.GetFromPool("BULLET", firePoint.position, firePoint.rotation);

            // 총알의 목표 설정
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null && player != null)
            {
                bulletScript.SetTarget(player.position);
            }

            Debug.Log($"{gameObject.name} fired a bullet!");
            SoundManager.Instance.PlaySFX(fireSoundName, 1.0f, false);



    }

    public void LookAtPlayer()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public virtual void StartChasingPlayer()
    {
        if (player == null)
        {
            Debug.LogWarning($"{gameObject.name}: Player not set.");
            return;
        }

        agent.isStopped = false;
        agent.SetDestination(player.position);

        animationParameters["isRunning"] = true;
        animationParameters["isWalking"] = false;
        animationParameters["isAttacking"] = false;
        ApplyAnimationParameters();
        SetState(new ChaseState(this));

        Debug.Log($"{gameObject.name} is now chasing the player!");
    }

    public abstract void PerformAttack();

    public bool PlayerInAttackRange()
    {
        if (player == null) return false;
        return Vector3.Distance(transform.position, player.position) <= attackRange;
    }
    protected void ApplyAnimationParameters()
    {
        foreach (var param in animationParameters)
        {
            SetAnimatorParameter(param.Key, param.Value);
        }
    }

    // parameter 설정 메서드 추가
    public void SetAnimatorParameter(string parameterName, object value)
    {
        if (animator == null)
        {
            Debug.LogWarning($"{gameObject.name}: Animator is not assigned.");
            return;
        }

        if (value is bool boolValue)
        {
            animator.SetBool(parameterName, boolValue);
        }
        else if (value is float floatValue)
        {
            animator.SetFloat(parameterName, floatValue);
        }
        else if (value is int intValue)
        {
            animator.SetInteger(parameterName, intValue);
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: Unsupported animator parameter type.");
        }
    }
}
