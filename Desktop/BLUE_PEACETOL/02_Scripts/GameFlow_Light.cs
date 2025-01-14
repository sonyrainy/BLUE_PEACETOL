using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow_Light : MonoBehaviour
{
 
    public bool deactivateInsteadOfDestroy = true;
    public GameObject nextGameFlow;
    public GameObject objectToDeactivateA;
    public GameObject objectToActivateSpawnArea;


    private void OnTriggerEnter(Collider other)
    {
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

            if (objectToDeactivateA != null)
            {
                objectToDeactivateA.SetActive(false);
            }


            if (objectToActivateSpawnArea != null)
            {
                objectToActivateSpawnArea.SetActive(true);
            }
            if (nextGameFlow != null)
            {
                nextGameFlow.SetActive(true);
            }
        }
    }
}