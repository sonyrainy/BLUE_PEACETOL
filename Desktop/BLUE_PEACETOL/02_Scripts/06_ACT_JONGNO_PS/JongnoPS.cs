using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JongnoPS : MonoBehaviour
{
    public bool destroyed = false; 
    public GameObject ClearUI; 
    public GameObject JongloUI; 
    public GameObject EscapeUI;
    public GameObject FxObj;

    void Start()
    {
        FxObj.SetActive(false);
    }

    public void HiyByGrenade()
    {
        destroyed = true; 
        ClearUI.SetActive(true); 
        JongloUI.SetActive(false); 
        EscapeUI.SetActive(true);
        FxObj.SetActive(true);
    }
}
