using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawerSlider : MonoBehaviour
{
    public Transform drawerBody; 
    public Transform handle;   
    public float minSlideDistance = 0f;  
    public float maxSlideDistance = 0.5f;
    public float slideSpeed = 5f; 
    private Vector3 initialHandlePosition;
    private Vector3 initialDrawerPosition;
    private XRGrabInteractable grabInteractable;
    private bool isSliding = false;

    void Start()
    {
        initialHandlePosition = handle.localPosition;
        initialDrawerPosition = drawerBody.localPosition;

        grabInteractable = handle.GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener((interactor) => OnGrabbed());
        grabInteractable.selectExited.AddListener((interactor) => OnReleased());
    }

    void Update()
    {
        if (isSliding)
        {
            // 목표 위치
            float zOffset = handle.localPosition.z - initialHandlePosition.z;
            float targetZ = Mathf.Clamp(initialDrawerPosition.z + zOffset, minSlideDistance, maxSlideDistance);
            Vector3 targetPosition = new Vector3(initialDrawerPosition.x, initialDrawerPosition.y, targetZ);

            // Lerp로 서랍 위치 이동
            drawerBody.localPosition = Vector3.Lerp(drawerBody.localPosition, targetPosition, Time.deltaTime * slideSpeed);
        }
    }

    private void OnGrabbed()
    {
        isSliding = true;
    }

    private void OnReleased()
    {
        isSliding = false;
        handle.localPosition = initialHandlePosition;
    }
}
