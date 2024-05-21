using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class WarmingMilk : MonoBehaviour
{
    private XRSocketInteractor socketInteractor;
    private Coroutine warmUpCoroutine;
    private bool isWarmedUp;
    private bool isPlayedWarmSpeech;
    private bool isOverHeat;

    public GameObject worldSpaceCanvas; // Reference to the world space canvas
    public TMP_Text countdownText; // Reference to the TextMeshPro text component
    public AudioSource audioSource;
    public AudioClip warmMilkSpeech;
    public AudioClip warmFinSpeech;
    public AudioClip beepSound;
    public AudioClip overHeatSpeech;
    public TaskUIManager taskUIManager;
    public GameObject milk;
    public GameObject stove;
    public Transform milkPos;
    public Transform stovePos;
    public BunsenBurner bunsenBurner;
    public ScreenFader screenFader;


    void Start()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();

        // Register event listeners for select entered and exited events
        socketInteractor.selectEntered.AddListener(OnSelectEntered);
        socketInteractor.selectExited.AddListener(OnSelectExited);

        isWarmedUp = false;
        isPlayedWarmSpeech = false;
        isOverHeat = false;

        // Initially hide the world space canvas
        if (worldSpaceCanvas != null)
        {
            worldSpaceCanvas.SetActive(false);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // When an object is placed in the socket, disable the XRGrabInteractable on the parent
        if (!isPlayedWarmSpeech)
        {
            audioSource.PlayOneShot(beepSound);
            audioSource.PlayOneShot(warmMilkSpeech);
            isPlayedWarmSpeech=true;
        }

        XRGrabInteractable grabInteractable = GetParentGrabInteractable();
        if (grabInteractable != null)
        {
            grabInteractable.enabled = false;
        }

        // Start the warm-up coroutine
        if (!isWarmedUp)
        {
            if (warmUpCoroutine == null)
            {

                warmUpCoroutine = StartCoroutine(WarmUpRoutine(args.interactableObject));
            }
        }
        
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        // When an object is removed from the socket, re-enable the XRGrabInteractable on the parent
        XRGrabInteractable grabInteractable = GetParentGrabInteractable();
        if (grabInteractable != null)
        {
            grabInteractable.enabled = true;
        }

        // Stop the warm-up coroutine
        if (warmUpCoroutine != null)
        {
            StopCoroutine(warmUpCoroutine);
            warmUpCoroutine = null;
        }

        // Hide the world space canvas
        if (worldSpaceCanvas != null)
        {
            worldSpaceCanvas.SetActive(false);
        }
    }

    private IEnumerator WarmUpRoutine(IXRSelectInteractable interactableObject)
    {
        // Show the world space canvas
        if (worldSpaceCanvas != null)
        {
            worldSpaceCanvas.SetActive(true);
        }

        float countdown = 15f;
        while (countdown > 0)
        {
            countdown -= Time.deltaTime;

            // Update the countdown timer UI
            if (countdownText != null)
            {
                countdownText.text = "Warming up: " + Mathf.Ceil(countdown).ToString() + "s";
            }

            yield return null;
        }

        // Check if the object is still in the socket
        if (socketInteractor.selectTarget == interactableObject)
        {
            isWarmedUp = true;
            ProceedWithNextLogic();
            countdown = 10f;
            while (countdown > 0)
            {
                countdown -= Time.deltaTime;

                if (countdownText != null)
                {
                    countdownText.text = "Remove count down: " + Mathf.Ceil(countdown).ToString() + "s";
                }

                yield return null;
            }

        }

        // Hide the world space canvas
        if (worldSpaceCanvas != null)
        {
            worldSpaceCanvas.SetActive(false);
        }

        warmUpCoroutine = null;
    }

    private void ProceedWithNextLogic()
    {
        // Add your logic here
        audioSource.PlayOneShot(warmFinSpeech);
        taskUIManager.TaskIndexInc();
        StartCoroutine(ReminderCoroutine());

    }

    private IEnumerator ReminderCoroutine()
    {
        // Wait for 10 seconds
        yield return new WaitForSeconds(10f);

        // Check if the milk is still in the socket
        if (socketInteractor.selectTarget != null)
        {
            // Execute the reminder logic
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.PlayOneShot(overHeatSpeech);
            isOverHeat = true;
            Debug.Log("OverHeat");
            taskUIManager.TaskIndexDec();
            StartCoroutine(SetBackOverHeatBool());
            // Add any additional logic here
        }

      
    }

    private IEnumerator SetBackOverHeatBool()
    {
        yield return StartCoroutine(screenFader.FadeIn());
        yield return new WaitForSeconds(2f);
        Debug.Log("reset Bool");
        isOverHeat = false;
        isWarmedUp = false;
        isPlayedWarmSpeech = false;
        milk.transform.position =milkPos.position;
        stove.transform.position = stovePos.position;
        yield return StartCoroutine(screenFader.FadeOut());
        bunsenBurner.ToggleBurner();
    }

    private XRGrabInteractable GetParentGrabInteractable()
    {
        // Find the XRGrabInteractable component in the parent
        Transform parentTransform = transform.parent;
        if (parentTransform != null)
        {
            return parentTransform.GetComponent<XRGrabInteractable>();
        }
        return null;
    }

    private void OnDestroy()
    {
        // Unregister event listeners
        socketInteractor.selectEntered.RemoveListener(OnSelectEntered);
        socketInteractor.selectExited.RemoveListener(OnSelectExited);
    }

    public bool GetMilkHeat()
    {
        return isWarmedUp;
    }
}
