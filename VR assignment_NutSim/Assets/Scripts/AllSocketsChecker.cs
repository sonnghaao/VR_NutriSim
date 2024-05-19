using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class AllSocketsChecker : MonoBehaviour
{
    public List<XRSocketInteractor> sockets;
    public GameObject panel; // UI panel for the next step message
    public AudioSource audioSource;
    public AudioClip beepSound;
    public AudioClip gatherCompleteSpeech;
    public TaskUIManager taskUIManager;
    public MaterialGrab mg;
    public XRGrabInteractable stove;
    private string nextStepMessage= "All materials has been gathered! Lets proceed to next step."; // Message to show when all sockets are filled

    private bool isGatherComplete;

    private void Start()
    {
        isGatherComplete = false;
        foreach (var socket in sockets)
        {
            socket.selectEntered.AddListener(OnSelectEntered);
            socket.selectExited.AddListener(OnSelectExited);
        }
        panel.SetActive(false);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        CheckAllSockets();
        audioSource.PlayOneShot(beepSound);
        taskUIManager.GatherCountInc();
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        CheckAllSockets();
        taskUIManager.GatherCountDec();
    }

    private void CheckAllSockets()
    {
        if(!isGatherComplete)
        {
            foreach (var socket in sockets)
            {
                if (socket.selectTarget == null)
                {
                    panel.SetActive(false); // Hide the panel if any socket is empty
                    return;
                }
            }

            // If all sockets have objects
            ShowPanelWithMessage(nextStepMessage);
        }
        
    }

    private void ShowPanelWithMessage(string message)
    {
        panel.SetActive(true);
        isGatherComplete= true;
        audioSource.PlayOneShot(beepSound);
        audioSource.PlayOneShot(gatherCompleteSpeech);
        mg.CompleteGather();
        stove.enabled = true;
        TMP_Text textMeshPro = panel.GetComponentInChildren<TMP_Text>();
        if (textMeshPro != null)
        {
            textMeshPro.text = message;
        }
        StartCoroutine(DelayedTaskIndexIncrement(5.5f));
    }

    private IEnumerator DelayedTaskIndexIncrement(float delay)
    {
        yield return new WaitForSeconds(delay);
        taskUIManager.TaskIndexInc();
    }

}
