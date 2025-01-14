using UnityEngine;
using System.Collections;

public class KimGooDialogueTrigger : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip playerDialogue;
    public AudioClip[] kimGooDialogueClips;

    [Header("Animator and Lip Sync")]
    public Animator kimGooAnimator;
    public string lipSyncParameter = "LipSync";

    [Header("Activation Settings")]
    public GameObject nextGameFlow;

    private AudioSource audioSource;
    private bool hasTriggered = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (nextGameFlow != null)
        {
            nextGameFlow.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PLAYER") && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(PlayDialogueSequence());
        }
    }

    private IEnumerator PlayDialogueSequence()
    {
        // 플레이어 스크립트 사운드 재생
        if (playerDialogue != null)
        {
            audioSource.PlayOneShot(playerDialogue);
            yield return new WaitForSeconds(playerDialogue.length);
        }

        foreach (var clip in kimGooDialogueClips)
        {
            if (clip != null)
            {
                audioSource.clip = clip;
                audioSource.Play();
                kimGooAnimator.SetBool(lipSyncParameter, true);
                yield return new WaitForSeconds(clip.length);
                kimGooAnimator.SetBool(lipSyncParameter, false);
            }
        }

        if (nextGameFlow != null)
        {
            nextGameFlow.SetActive(true);
        }
    }
}
