using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance { get; private set; }

    public Transform[] spawnPoints;
    public int maxTotalEnemies = 15;

    private List<GameObject> activeEnemies = new List<GameObject>();

    // void Awake()
    // {
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    void Start()
    {
        StartCoroutine(SpawnInitialEnemies());
    }

    void Update()
    {
        // 적의 수 체크 후, 부족하면 생성
        activeEnemies.RemoveAll(enemy => enemy == null || !enemy.activeSelf); 
        if (activeEnemies.Count < maxTotalEnemies)
        {
            StartCoroutine(SpawnEnemyWithDelay());
        }
    }

    private IEnumerator SpawnInitialEnemies()
    {
        while (activeEnemies.Count < maxTotalEnemies)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator SpawnEnemyWithDelay()
    {
        yield return new WaitForSeconds(1f);
        if (activeEnemies.Count < maxTotalEnemies)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        string enemyTag = (Random.value > 0.5f) ? "LONGATTACKENEMY" : "SHORTATTACKENEMY";
        
        // 적 생성 시, Pool에서 빼온 후 활성화
        GameObject enemy = ObjectPoolManager.instance.GetFromPool(enemyTag, spawnPoint.position, spawnPoint.rotation);

        if (enemy != null)
        {
            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = spawnPoint.rotation;

            enemy.SetActive(true);

            // enemy.GetComponent<EnemyAI_oneHand_Attack>()?.StartChasingPlayer();
            // enemy.GetComponent<EnemyAI_twoHand_Attack>()?.StartChasingPlayer();
            
            // 적의 기본 상태 ChaseState로 설정
            EnemyAI_Final enemyAI = enemy.GetComponent<EnemyAI_Final>();
            if (enemyAI != null)
            {
                enemyAI.SetState(new ChaseState(enemyAI));
            }

            activeEnemies.Add(enemy);
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
        }
    }
}
