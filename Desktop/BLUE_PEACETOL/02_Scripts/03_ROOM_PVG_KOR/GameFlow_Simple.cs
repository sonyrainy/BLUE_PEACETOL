using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public bool deactivateInsteadOfDestroy = true;
    public GameObject nextGameFlow;

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트 체크
        if (other.CompareTag("PLAYER"))
        {
            if (deactivateInsteadOfDestroy)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }

            // nextGameFlow가 설정되어 있으면 활성화하기
            if (nextGameFlow != null)
            {
                nextGameFlow.SetActive(true);
            }
        }
    }
}
