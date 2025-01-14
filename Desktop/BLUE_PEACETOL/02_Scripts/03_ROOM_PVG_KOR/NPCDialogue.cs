using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public Dialogue[] dialogues;
    private AudioSource audioSource;
    private Animator animator;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void PlayDialogue(int index)
    {
        if (index < 0 || index >= dialogues.Length) return;

        Dialogue dialogue = dialogues[index];
        audioSource.clip = dialogue.clip;
        audioSource.Play();

        // 김구 대사라면, 김구 립싱크 애니메이션 실행
        if (dialogue.isKimGoo)
        {
            animator.SetBool("LipSync", true);
            StartCoroutine(StopLipSyncAfterClip(dialogue.clip.length));
        }
    }

    private IEnumerator StopLipSyncAfterClip(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        animator.SetBool("LipSync", false);
    }
}
