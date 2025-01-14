using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Dialogue
{
    public AudioClip clip; 
    public bool isKimGoo;
}

public class KimGooEventSequence : MonoBehaviour
{
    [Header("Kim Goo Settings")]
    public Transform kimGoo;
    public Transform targetPositionA;
    public Transform targetLookPositionB;
    public Animator kimGooAnimator;
    public string walkParameter = "isWalking";
    public string lipSyncParameter = "LipSync";
    public string photoTrigger = "Photo";
    private AudioSource audioSource;

    [Header("Dialogue Sequence")]
    public Dialogue[] dialogueSequence;

    [Header("Object and Sound Settings")]
    public GameObject objectK;
    public AudioClip soundClipForK;
    public GameObject[] objectsL;

    public GameObject nextGameFlow;

    public float moveSpeed = 2.0f;
    private bool hasTriggered = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        nextGameFlow.SetActive(false);

        // 카메라 빛, 김상옥 이미지 비활성화
        if (objectK != null) objectK.SetActive(false);
        foreach (GameObject obj in objectsL)
        {
            if (obj != null) obj.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PLAYER") && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(EventSequence());
        }
    }

    private IEnumerator EventSequence()
    {
        // 김구 카메라 찍는 위치로 이동
        kimGooAnimator.SetBool(walkParameter, true);
        yield return StartCoroutine(MoveToPosition(targetPositionA.position));

        FaceTarget(targetLookPositionB.position);

        kimGooAnimator.SetBool(photoTrigger, true);
        
        yield return new WaitForSeconds(1f);

        
        // 카메라 불빛 + 소리 재생
        if (objectK != null)
        {
            objectK.SetActive(true);

            if (soundClipForK != null)
            {
                audioSource.PlayOneShot(soundClipForK);

            }
            yield return new WaitForSeconds(1f);
            objectK.SetActive(false);
        }
        
        kimGooAnimator.SetBool(photoTrigger, false);

        // 김상옥 이미지 활성화
        foreach (GameObject obj in objectsL)
        {
            if (obj != null) obj.SetActive(true);

        }

        // 스크립트 재생
        foreach (Dialogue dialogue in dialogueSequence)
        {
            if (dialogue.clip != null)
            {
                audioSource.clip = dialogue.clip;
                audioSource.Play();

                // 김구 대사라면, 김구 립싱크 애니메이션 실행
                if (dialogue.isKimGoo)
                {
                    kimGooAnimator.SetBool(lipSyncParameter, true);
                }

                yield return new WaitForSeconds(dialogue.clip.length);

                if (dialogue.isKimGoo)
                {
                    kimGooAnimator.SetBool(lipSyncParameter, false);
                }
            }
        }
        if (nextGameFlow != null) nextGameFlow.SetActive(true);


    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(kimGoo.position, targetPosition) > 0.01f)
        {
            Vector3 lookDirection = new Vector3(targetPosition.x, kimGoo.position.y, targetPosition.z);
            kimGoo.LookAt(lookDirection);

            kimGoo.position = Vector3.MoveTowards(kimGoo.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        kimGoo.position = targetPosition;
        kimGooAnimator.SetBool(walkParameter, false);
    }

    private void FaceTarget(Vector3 targetPosition)
    {
        Vector3 lookDirection = new Vector3(targetPosition.x, kimGoo.position.y, targetPosition.z);
        kimGoo.LookAt(lookDirection);
    }
}
