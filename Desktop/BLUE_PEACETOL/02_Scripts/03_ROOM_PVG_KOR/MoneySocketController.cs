using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MoneySocketController : MonoBehaviour
{
    public GameObject gunPrefab; 
    public Transform gunSpawnLocation;  
    public Animator npcAnimator;       
    public string triggerName = "PlayAnimation"; 
    public GameObject nextGameFlow;      
    public AudioClip soundEffect;      
    public string allowedItemTag = "MONEY"; 

    private XRSocketInteractor socketInteractor;
    private AudioSource audioSource;
    private bool hasGeneratedGun = false; 

    void Start()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
        audioSource = gameObject.AddComponent<AudioSource>();

        // 사운드 클립 및 AudioSource 초기 설정
        if (soundEffect != null)
        {
            audioSource.clip = soundEffect;
            audioSource.spatialBlend = 0f; 
            audioSource.volume = 1f;    
        }

        // 100원이 Socket될 때 호출되는 이벤트 연결
        socketInteractor.selectEntered.AddListener(OnItemPlaced);
    }

    private void OnItemPlaced(SelectEnterEventArgs args)
    {
        // 한 번만 총 생성되도록 설정
        if (hasGeneratedGun)
        {
            return;
        }

        // 100원 올렸는지 확인
        if (args.interactableObject.transform.CompareTag(allowedItemTag))
        {
            if (gunPrefab != null && gunSpawnLocation != null)
            {
                Instantiate(gunPrefab, gunSpawnLocation.position, gunSpawnLocation.rotation);
                hasGeneratedGun = true;
            }

            // NPC(임시정부 동료) 애니메이션 실행
            if (npcAnimator != null && !string.IsNullOrEmpty(triggerName))
            {
                npcAnimator.SetTrigger(triggerName);
            }

            if (nextGameFlow != null)
            {
                nextGameFlow.SetActive(true);
            }

            if (audioSource != null && soundEffect != null)
            {
                audioSource.Play();
            }
        }
    }

    private void OnDestroy()
    {
        socketInteractor.selectEntered.RemoveListener(OnItemPlaced);
    }
}
