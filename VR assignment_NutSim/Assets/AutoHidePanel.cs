using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHidePanel : MonoBehaviour
{
    void OnEnable()
    {
        // Automatically hide panel after 6 seconds
        Invoke("HidePanel", 5f);
    }

    void HidePanel()
    {
        gameObject.SetActive(false);
    }
}