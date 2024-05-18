using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LemonTriggerInteractor : MonoBehaviour
{


    public Collider triggerZone; // Public variable for the trigger zone
    public AudioSource audioSource;
    public AudioClip squeezeSound;
    public AudioClip waitMilkReactSpeech;
    public WarmingMilk wm;

    private XRGrabInteractable grabInteractable;
    private bool isInTriggerZone = false; // To track if the object is inside the trigger zone



    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.activated.AddListener(OnTriggerPressed);
        }
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.activated.RemoveListener(OnTriggerPressed);
        }
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
        if (wm.GetMilkHeat())
        {
            audioSource.PlayOneShot(squeezeSound);
            audioSource.PlayOneShot(waitMilkReactSpeech);
        }

    }

}
