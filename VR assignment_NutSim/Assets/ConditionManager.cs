using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConditionManager : MonoBehaviour
{
    private int itemCount;
    public ButtonPushOpenDoor otherScript;
    public AudioClip gratzSound;
    public AudioSource audioSource;
    public GameObject panel;

    private bool isReady;

    void Start()
    {
        itemCount = 3;
        isReady = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReady)
        {
            if (itemCount == 0)
            {
                isReady = true;
                StartCoroutine(DelayBeforeAudio());
            }
        }
        
    }

    IEnumerator DelayBeforeAudio()
    {
        yield return new WaitForSeconds(6f); 
        audioSource.PlayOneShot(gratzSound);
        ShowPanelWithMessage("You're all set to step into the lab.");
        otherScript.SetBooleanValue(true);
        
    }

    void ShowPanelWithMessage(string message)
    {
        panel.SetActive(true);
        TMP_Text textMeshPro = panel.GetComponentInChildren<TMP_Text>();
        if (textMeshPro != null)
        {
            textMeshPro.text = message;
        }
    }

    public void putOn()
    {
        itemCount--;
    }

    
}
