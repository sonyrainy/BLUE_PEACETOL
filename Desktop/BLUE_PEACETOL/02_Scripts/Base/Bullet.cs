using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    private Vector3 targetPosition;

    public void SetTarget(Vector3 target)
    {

        targetPosition = target;
    }

    void Update()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);


        if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
        {
            ObjectPoolManager.instance.ReturnToPool(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PLAYER"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); 
            }

            ObjectPoolManager.instance.ReturnToPool(gameObject);


        }
    }
}
