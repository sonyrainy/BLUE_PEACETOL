using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : MonoBehaviour
{
    public GameObject trailObj; 
    public GameObject effectObj; 
    public GameObject meshObj; 
    public Rigidbody Rb; 

    [HideInInspector]
    public bool StartCount = false; 

    private bool hasExploded = false;

    void FixedUpdate()
    {
        if (StartCount && !hasExploded)
        {
            StartCoroutine(Explosion());
        }
    }

    public void SelectExit()
    {
        StartCount = true;
    }

    IEnumerator Explosion()
    {
        hasExploded = true;
        trailObj.SetActive(true); 
        yield return new WaitForSeconds(3.0f); 
        Rb.velocity = Vector3.zero; 
        Rb.angularVelocity = Vector3.zero; 
        effectObj.SetActive(true);
        meshObj.SetActive(false); 

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5, LayerMask.GetMask("JongnoPS"));
        bool jongnoHit = false;

        foreach (Collider hitCollider in hitColliders)
        {
            Debug.Log("감지된 오브젝트: " + hitCollider.name);
            JongnoPS jongnoComponent = hitCollider.GetComponent<JongnoPS>();
            if (jongnoComponent != null)
            {
                jongnoComponent.HiyByGrenade();
                jongnoHit = true;
            }
        }

        if (!jongnoHit)
        {
            Debug.Log("종로경찰서 없음.");
        }

        Destroy(gameObject, 2); 
    }
}
