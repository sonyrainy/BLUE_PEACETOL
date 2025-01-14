using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class Player_Shoot : MonoBehaviour
{
    [SerializeField]
    private Transform muzzle;

    [SerializeField]
    private GameObject muzzleFlash;

    [SerializeField]
    private AudioSource shootAudio;

    [SerializeField]
    private float fireCooldown = 1f;

    private XRGrabInteractable grabInteractable;
    private float nextFireTime = 0f;
    private int maxShots = 7; 
    private int currentShots = 0; 

    private bool isDeactivating = false; 
    private PlayerTimeControl playerTimeControl;


    void Start()
    {
        playerTimeControl = GetComponentInParent<PlayerTimeControl>(); 

        shootAudio = GetComponent<AudioSource>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.activated.AddListener(OnActivate);

        if (muzzleFlash != null)
        {
            muzzleFlash.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        // 이벤트 등록 해제
        //grabInteractable.activated.RemoveListener(OnActivate);
    }

    private void OnActivate(ActivateEventArgs arg)
    {
        // 쿨타임 확인 후 발사, 최대 발사 횟수 체크하기
        if (Time.time >= nextFireTime && currentShots < maxShots)
        {
            TriggerShoot(arg);
            nextFireTime = Time.time + fireCooldown;
            currentShots++;
        }

        if (currentShots >= maxShots)
        {
            Debug.Log("Max shots reached. Gun cannot be fired anymore.");
            StartCoroutine(FadeOutAndDeactivate());

        }
        
        // 총을 쏠 때 시간 흐름 조정 메서드 호출
        if (playerTimeControl != null)
         {
                playerTimeControl.OnPlayerShoot();
        }
    }

    public void TriggerShoot(ActivateEventArgs arg)
    {

        // 햅틱 진동 추가
        if (arg.interactorObject is XRBaseControllerInteractor controllerInteractor)
        {
            var controller = controllerInteractor.xrController;
            if (controller != null)
            {
                controller.SendHapticImpulse(1.0f, 0.2f); // 진동 세기(1.0)와 지속 시간(0.2초) 설정
            }
        }

        // MuzzleFlash 활성화
        if (muzzleFlash != null)
        {
            muzzleFlash.SetActive(true);
            Invoke("DeactivateMuzzleFlash", 1f);
        }

        // 레이캐스트 발사
        Ray ray = new Ray(muzzle.position, muzzle.forward);
        RaycastHit hitInfo;
        Debug.DrawRay(muzzle.position, muzzle.forward * 100f, Color.red, 2f);

        if (Physics.Raycast(ray, out hitInfo, 100f))
        {
            // 충돌한 오브젝트가 있을 경우
            //Debug.Log("Hit: " + hitInfo.collider.name);

            // 적을 맞췄을 경우 적에게 데미지를 입힘
            Enemy_Health_1 enemyHealth_1 = hitInfo.collider.GetComponent<Enemy_Health_1>();
            if (enemyHealth_1 != null)
            {
                enemyHealth_1.TakeDamage(1); 
                Debug.Log("1감소");
            }
            Enemy_Health_2 enemyHealth_2 = hitInfo.collider.GetComponent<Enemy_Health_2>();
            if (enemyHealth_2 != null)
            {
                enemyHealth_2.TakeDamage(1);
                Debug.Log("1감소");
            }
        }

        if (shootAudio != null)
        {
            shootAudio.Play();
        }

        
    }

    private void DeactivateMuzzleFlash()
    {
        // MuzzleFlash 비활성화
        if (muzzleFlash != null)
        {
            muzzleFlash.SetActive(false);
        }
    }
    private IEnumerator FadeOutAndDeactivate()
    {
        isDeactivating = true;

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Material material = renderer.material;
            Color originalColor = material.color;

            float fadeDuration = 10.0f;
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }
        }

        // 5초 후에 오브젝트 비활성화 후 풀로 반환
        ObjectPoolManager.instance.ReturnToPool(gameObject);
        isDeactivating = false;
    }
    public void ResetGun()
    {
        // 총을 풀로 되돌릴 때 발사 횟수 초기화 및 알파 값 복원
        currentShots = 0;
        isDeactivating = false;

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Material material = renderer.material;
            Color originalColor = material.color;
            material.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
        }
    }

}
