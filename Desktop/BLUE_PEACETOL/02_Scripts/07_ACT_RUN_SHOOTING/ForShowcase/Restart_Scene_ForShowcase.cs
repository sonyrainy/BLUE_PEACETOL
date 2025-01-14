using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Restart_Scene_ForShowcase : MonoBehaviour
{
    public Transform initialPosition;

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 PLAYER 태그 갖는지 확인
        if (other.CompareTag("PLAYER"))
        {
             SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
}