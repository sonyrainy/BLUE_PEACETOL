using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                //추가
                obj.tag = pool.tag;

                //~추가
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    public void CreatePool(string tag, GameObject prefab, int size)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < size; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(tag, objectPool);
        }
    }
    public GameObject GetFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

        public void ReturnToPool(GameObject obj)
    {
        string tag = obj.tag;

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist!");
            Destroy(obj);
        }
        else
        {
            Player_Shoot gunScript = obj.GetComponent<Player_Shoot>();
            if (gunScript != null)
            {
                gunScript.ResetGun();
            }
            obj.SetActive(false);
            poolDictionary[tag].Enqueue(obj);
        }
    }
    public void ReturnToPoolAfterDelay(GameObject obj, float delay)
    {
        StartCoroutine(ReturnToPoolCoroutine(obj, delay));
    }

    private IEnumerator ReturnToPoolCoroutine(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnToPool(obj);
    }
}
