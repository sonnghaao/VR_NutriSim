using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StrainerTriggerInteractor : MonoBehaviour
{
    public Collider triggerZone;
    public AudioSource audioSource;
    public AudioClip beepSound;
    public AudioClip gratzSpeech;
    public TaskUIManager taskUIManager;
    public GameObject strainer;
    public GameObject whiteBowl;
    public MilkTriggerInteractor mti;

    private XRGrabInteractable grabInteractable;
    private bool isInTriggerZone = false;
    private bool hasPut;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.activated.AddListener(OnTriggerPressed);
        }
        hasPut = false;
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
        if (mti.GetPour())
        {
            if (!hasPut)
            {
                audioSource.PlayOneShot(beepSound);
                audioSource.PlayOneShot(gratzSpeech);
                strainer.transform.Find("curdle").gameObject.SetActive(false);
                whiteBowl.transform.Find("curdle").gameObject.SetActive(true);
                hasPut = true;
                taskUIManager.TaskIndexInc();
            }
        }
        
    }

}
