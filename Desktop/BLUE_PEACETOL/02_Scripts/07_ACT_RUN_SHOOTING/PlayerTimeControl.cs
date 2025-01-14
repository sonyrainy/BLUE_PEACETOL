using System.Collections;
using UnityEngine;

public class PlayerTimeControl : MonoBehaviour
{
    public float normalTimeScale = 1f;  
    public float slowTimeScale = 0.2f;   
    public float shootTimeScale = 1f;    
    public float shootTimeDuration = 0.2f;
    public float movementThreshold = 0.1f;

    private CharacterController characterController; 
    private Vector3 lastPosition;
    private bool isShooting = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        lastPosition = transform.position;
    }

    void Update()
    {
        if (isShooting)
        {
            // 총쏘는 동안 시간에는 빠른 시간 흐름(정상)
            Time.timeScale = normalTimeScale;
        }
        else
        {
            float playerSpeed = Vector3.Distance(lastPosition, transform.position) / Time.deltaTime;

            if (playerSpeed > movementThreshold)
            {
                // 움직이는 동안에는 빠른 시간 흐름(정상)
                Time.timeScale = normalTimeScale;
            }
            else
            {
                // 가만히 있으면 느린 시간 흐름
                Time.timeScale = slowTimeScale;
            }
        }

        lastPosition = transform.position;

        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    public void OnPlayerShoot()
    {
        if (!isShooting)
        {
            StartCoroutine(HandleShootTime());
        }
    }

    private IEnumerator HandleShootTime()
    {
        isShooting = true;
        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        yield return new WaitForSecondsRealtime(shootTimeDuration);

        isShooting = false;
    }
}
