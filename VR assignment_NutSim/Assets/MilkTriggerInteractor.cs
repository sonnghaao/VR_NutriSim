using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MilkTriggerInteractor : MonoBehaviour
{
    public Collider triggerZone; 
    public AudioSource audioSource;
    public AudioClip pourSound;
    public AudioClip intoWhiteBowlSpeech;
    public TaskUIManager taskUIManager;
    public GameObject strainer;
    public GameObject milk;
    public StrainerSocketLogic strainerSocketLogic;

    private XRGrabInteractable grabInteractable;
    private bool isInTriggerZone = false;
    private bool hasPour;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.activated.AddListener(OnTriggerPressed);
        }
        hasPour = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object is entering the specified trigger zone
        if (other == triggerZone)
        {
            isInTriggerZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object is exiting the specified trigger zone
        if (other == triggerZone)
        {
            isInTriggerZone = false;
        }
    }

    private void OnTriggerPressed(ActivateEventArgs args)
    {
        // Check if the trigger button is pressed and the object is inside the trigger zone
        if (isInTriggerZone)
        {
            ExecuteLogic();
        }
    }

    private void ExecuteLogic()
    {
        if (strainerSocketLogic.GetSetUp())
        {
            if (!hasPour)
            {
                audioSource.PlayOneShot(pourSound);
                audioSource.PlayOneShot(intoWhiteBowlSpeech);
                strainer.transform.Find("curdle").gameObject.SetActive(true);
                milk.transform.Find("AfterWarm").gameObject.SetActive(false);
                taskUIManager.TaskIndexInc();
                hasPour=true;
            }
        }
        

    }

    public bool GetPour()
    {
        return hasPour;
    }
}
