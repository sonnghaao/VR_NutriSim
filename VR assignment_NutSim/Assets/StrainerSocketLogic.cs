using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StrainerSocketLogic : MonoBehaviour
{
    private XRSocketInteractor socketInteractor;
    private bool isSetUp;

    public AudioSource audioSource;
    public AudioClip putCurdSpeech;
    public LemonTriggerInteractor lemonTrigger;
    public TaskUIManager taskUIManager;

    void Start()
    {
        // Get the XRSocketInteractor component attached to this GameObject
        socketInteractor = GetComponent<XRSocketInteractor>();

        // Register event listeners for select entered event
        socketInteractor.selectEntered.AddListener(OnSelectEntered);
        isSetUp = false;
    }

    private void OnDestroy()
    {
        // Unregister event listener
        socketInteractor.selectEntered.RemoveListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // When an object is placed in the socket, execute the desired logic
        ExecuteLogic();
    }

    private void ExecuteLogic()
    {
        // Your logic goes here
        if (lemonTrigger.GetSqueeze())
        {
            if (!isSetUp)
            {
                audioSource.PlayOneShot(putCurdSpeech);
                taskUIManager.TaskIndexInc();
                isSetUp = true;
            }
            
        }
        
    }
    
    public bool GetSetUp()
    {
        return isSetUp;
    }
}
