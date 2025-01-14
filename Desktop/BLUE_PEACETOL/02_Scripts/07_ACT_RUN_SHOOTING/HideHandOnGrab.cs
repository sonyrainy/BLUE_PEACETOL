using UnityEngine;

// 손을 통해 총을 잡으면 손이 보이지 않도록 설정
public class HideHandOnGunGrab : MonoBehaviour
{
    public GameObject handModel;

    void OnTriggerEnter(Collider other)
    {
        // GUN_1 또는 GUN_2 태그를 가진 오브젝트와 충돌했을 때 손이 보이지 않도록 설정
        if (other.CompareTag("GUN_1") || other.CompareTag("GUN_2"))
        {
            MeshRenderer handMeshRenderer = handModel.GetComponent<MeshRenderer>();
            if (handMeshRenderer != null)
            {
                handMeshRenderer.enabled = false;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // GUN_1 또는 GUN_2 태그를 가진 오브젝트와 충돌이 끝났을 때 손이 보이도록 설정
        if (other.CompareTag("GUN_1") || other.CompareTag("GUN_2"))
        {
            MeshRenderer handMeshRenderer = handModel.GetComponent<MeshRenderer>();
            if (handMeshRenderer != null)
            {
                handMeshRenderer.enabled = true;
            }
        }
    }
}
