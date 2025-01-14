using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; 
    private int currentHealth;
    private AudioSource audioSource;
    private float breathSoundCooldown = 5f;
    private float nextBreathSoundTime = 0f;

    void Start()
    {
        currentHealth = maxHealth; 
        PlayBreathSound(); 
    }

    void Update()
    {
        // 플레이어의 체력이 0 이하가 되면 씬 재시작
        if (currentHealth <= 0)
        {
            //RestartScene();
        }

        // 일정 시간이 지나면 숨소리 반복 재생
        if (Time.time >= nextBreathSoundTime)
        {
            PlayBreathSound();
            nextBreathSoundTime = Time.time + breathSoundCooldown;
        }
    }
   public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("플레이어의 현재 체력: " + currentHealth);

        // 랜덤하게 플레이어가 총에 맞았을 때 재생할 사운드를 선택
        string[] hitSounds = {
            "Voice_Male_V2_Hit_Short_Mono_04",
            "Voice_Male_V2_Hit_Short_Mono_06",
            "Voice_Male_V1_Hit_Short_Mono_13"
        };
        int randomIndex = Random.Range(0, hitSounds.Length);
        SoundManager.Instance.PlaySFX(hitSounds[randomIndex], 1.0f, false);

        if (currentHealth <= 0)
        {
            //RestartScene();
        }
    }




        private void PlayBreathSound()
    {
        // 숨소리 사운드 재생
        SoundManager.Instance.PlaySFX("Voice_Male_V1_Breath_Mouth_Moderate_Sequence_Mono", 0.2f, false);
    }

    //void RestartScene()
    //{
    //    Debug.Log("Restarting scene...");
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}
}
