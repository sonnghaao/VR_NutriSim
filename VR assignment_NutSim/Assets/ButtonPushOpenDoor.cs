using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; 

public class ButtonPushOpenDoor : MonoBehaviour
{
    public AudioClip errorSound;
    public AudioClip openDoorSound;
    public AudioClip notReadyYetSound;
    public AudioSource audioSource;
    public GameObject panel; 
    bool isEquipReady = false;

    void Start()
    {
        GetComponent<XRSimpleInteractable>().selectEntered.AddListener(x => ToggleDoorOpen());
        panel.SetActive(false); // make sure the panel is invisible at begining
    }

    public void ToggleDoorOpen()
    {
        if (!isEquipReady)
        {
            audioSource.PlayOneShot(errorSound);
            audioSource.PlayOneShot(notReadyYetSound);
            ShowPanelWithMessage("You are not ready yet. Please proceed to put on your safety equipment.");
        }
        else
        {
            audioSource.PlayOneShot(openDoorSound);
        }
    }

    void ShowPanelWithMessage(string message)
    {
        panel.SetActive(true);
        TMP_Text textMeshPro = panel.GetComponentInChildren<TMP_Text>();
        if (textMeshPro != null)
        {
            textMeshPro.text = message;
        }
        StartCoroutine(HidePanelAfterDelay(6f)); 
    }

    IEnumerator HidePanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}