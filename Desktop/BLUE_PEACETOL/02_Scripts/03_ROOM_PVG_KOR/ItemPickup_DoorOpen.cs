using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class ItemPickup : MonoBehaviour
{
    public GameObject door;
    public GameObject currentGameFlow;
    public GameObject nextGameFlow;
    public float openAngle = 90f; 
    public float openDuration = 2f; 
    private XRGrabInteractable grabInteractable;
    private Quaternion initialRotation; 
    private bool isDoorOpen = false;  

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnItemGrabbed);

        if (door != null)
        {
            initialRotation = door.transform.localRotation;
        }
    }

    private void OnItemGrabbed(SelectEnterEventArgs args)
    {
        if (!isDoorOpen)
        {
            StartCoroutine(OpenDoor());
        }

        // 현재 GameFlow 비활성화
        if (currentGameFlow != null)
        {
            currentGameFlow.SetActive(false);
        }

        // 다음 GameFlow 활성화
        if (nextGameFlow != null)
        {
            nextGameFlow.SetActive(true);
        }
    }

    private IEnumerator OpenDoor()
    {
        isDoorOpen = true;
        float elapsedTime = 0f;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0, openAngle, 0);

        while (elapsedTime < openDuration)
        {
            door.transform.localRotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / openDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        door.transform.localRotation = targetRotation;
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnItemGrabbed);
    }
}
