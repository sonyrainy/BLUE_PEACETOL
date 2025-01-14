using UnityEngine;

public class Enemy_Health_1 : EnemyHealthBase
{

    public override void SpawnGunOnDeath(Vector3 position, Quaternion rotation)
    {
        Quaternion adjustedRotation = rotation * Quaternion.Euler(0, 70, 0);

        GameObject gun = ObjectPoolManager.instance.GetFromPool("GUN_1", position, adjustedRotation);
        if (gun != null)
        {
            ObjectPoolManager.instance.ReturnToPoolAfterDelay(gun, 5f);
        }

        Debug.Log($"{gameObject.name} dropped a pistol.");
    }
}
