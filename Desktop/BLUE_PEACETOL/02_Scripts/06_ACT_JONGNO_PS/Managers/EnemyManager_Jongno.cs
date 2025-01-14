using UnityEngine;
using System.Collections.Generic;

public class EnemyManager_Jongno : MonoBehaviour
{
    public static EnemyManager_Jongno instance;

    private List<EnemyAI_Jongno> enemies = new List<EnemyAI_Jongno>();
    public bool IsPlayerDetectedGlobally { get; private set; } = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
   private void Start()
    {
        // 초기화 작업이 필요하다면 이곳에 추가
        Debug.Log("EnemyManager_Jongno started and ready to manage enemies.");
    }

    public void RegisterEnemy(EnemyAI_Jongno enemy)
    {
        if (!enemies.Contains(enemy))
        {
            enemies.Add(enemy);

        }
    }

    public void NotifyAttack()
    {
        if (IsPlayerDetectedGlobally) return;

        IsPlayerDetectedGlobally = true; 

        foreach (var enemy in enemies)
        {
            enemy.StartChasingPlayer(); 
        }
    }
}
