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
    public TaskUIManager taskUIManager;
    public GameObject milk;

    private XRGrabInteractable grabInteractable;
    private bool isInTriggerZone = false; // To track if the object is inside the trigger zone
    private bool hasSqueeze;



    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.activated.AddListener(OnTriggerPressed);
        }
        hasSqueeze = false;
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
            if (!hasSqueeze)
            {
                hasSqueeze=true;
                audioSource.PlayOneShot(squeezeSound);
                audioSource.PlayOneShot(waitMilkReactSpeech);
                StartCoroutine(DelayedExecution());
            }
            
        }

    }

    private IEnumerator DelayedExecution()
    {
        Debug.Log("Starting 10 second delay.");
        yield return new WaitForSeconds(10f);
        taskUIManager.TaskIndexInc();

        milk.transform.Find("milk liquid").gameObject.SetActive(false);
        milk.transform.Find("AfterWarm").gameObject.SetActive(true);
    }

    public bool GetSqueeze()
    {
        return hasSqueeze;
    }
}
