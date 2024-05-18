using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class MaterialGrab : MonoBehaviour
{
    
    public GameObject arrowUI; // Reference to the GameObject to be activated
    public GameObject panel;
    public AudioSource audioSource;
    public AudioClip bringSpeech;
    public AudioClip notBringSpeech;

    private XRGrabInteractable grabInteractable;
    private string message;
    private static bool isGatherComplete= false;

    private void OnEnable()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }
    }

    private void OnDisable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
            grabInteractable.selectExited.RemoveListener(OnReleased);
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Check if the grabbed object has the tag "Material"

        if (!isGatherComplete)
        {
            if (args.interactableObject.transform.CompareTag("Material"))
            {

                if (args.interactorObject is XRSocketInteractor)
                {
                    Debug.Log("Object is being placed in a socket, arrowUI will not be activated.");
                    return;
                }

                if (arrowUI != null)
                {
                    arrowUI.SetActive(true);
                }

                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }

                message = "Kindly take the " + name + " and put it on the table in the middle of the laboratory.";
                audioSource.PlayOneShot(bringSpeech);
                ShowPanelWithMessage(message);
            }
            else
            {
                audioSource.PlayOneShot(notBringSpeech);
                ShowPanelWithMessage("These are not the materials or items required for the experiment.");
            }
        }
        
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        // Check if the released object has the tag "Material"
        if (args.interactableObject.transform.CompareTag("Material"))
        {
            if (arrowUI != null)
            {
                arrowUI.SetActive(false);
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

    public void CompleteGather()
    {
        isGatherComplete = true;
    }
}
