using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; 

public class ButtonPushOpenDoor : MonoBehaviour
{
    public GameObject player;
    public GameObject targetPos;
    public AudioClip beepSound;
    public AudioClip openDoorSound;
    public AudioClip notReadyYetSound;
    public AudioSource audioSource;
    public GameObject panel;
    public ScreenFader screenFader;
    bool isEquipReady = false;
    public TaskUIManager taskUImanager;
    

    private bool isDoorOpen;

    void Start()
    {
        GetComponent<XRSimpleInteractable>().selectEntered.AddListener(x => ToggleDoorOpen());
        panel.SetActive(false); // make sure the panel is invisible at begining
        isDoorOpen = false;
    }

    public void ToggleDoorOpen()
    {
        if (!isEquipReady)
        {
            audioSource.PlayOneShot(beepSound);
            audioSource.PlayOneShot(notReadyYetSound);
            ShowPanelWithMessage("You are not ready yet. Please proceed to put on your safety equipment.");
        }
        else
        {
            if(!isDoorOpen)
            {
                audioSource.PlayOneShot(beepSound);
                isDoorOpen = true;
                StartCoroutine(PlayOpenDoorSoundAndMovePlayer());
            }
            
            
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
    }

    IEnumerator PlayOpenDoorSoundAndMovePlayer()
    {
        yield return StartCoroutine(screenFader.FadeIn());
        audioSource.PlayOneShot(openDoorSound);
        yield return new WaitForSeconds(openDoorSound.length);
        
        player.transform.position = targetPos.transform.position;
        player.transform.rotation = targetPos.transform.rotation;
        taskUImanager.TaskIndexInc();
        yield return StartCoroutine(screenFader.FadeOut());

    }

    public void SetBooleanValue(bool b)
    {
        isEquipReady = b;
    }
}