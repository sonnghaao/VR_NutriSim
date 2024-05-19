using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class SafetyEquipCheck : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip equipSound;
    public AudioClip wrongSound;
    public AudioClip wrongSpeech;
    public GameObject panel;
    private string message;
    public ConditionManager cdManager;
    public TaskUIManager taskUIManager;

    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(x=> CheckEquip());
        panel.SetActive(false); // make sure the panel is invisible at begining
    }

    public void CheckEquip()
    {
        if (CompareTag("SafetyEquip"))
        {
            Destroy(gameObject);
            audioSource.PlayOneShot(equipSound);
            message = "You have put on " + name +".";
            taskUIManager.SafetyEquipCountInc();
            ShowPanelWithMessage(message);
            cdManager.putOn();
        }
        else
        {
            audioSource.PlayOneShot(wrongSpeech);
            audioSource.PlayOneShot(wrongSound);
            ShowPanelWithMessage("This is not appropriate safety equipment for the experiment.");
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

   


    
}
