using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketObjectNameDisplay : MonoBehaviour
{
    public XRSocketInteractor socketInteractor; // Public reference to the XRSocketInteractor
    private TMP_Text tmpText; // Reference to the TextMeshPro text component

    void Start()
    {
        // Get the TextMeshPro component attached to this GameObject
        tmpText = GetComponent<TMP_Text>();

        if (socketInteractor != null)
        {
            // Register event listeners for select entered and exited events
            socketInteractor.selectEntered.AddListener(OnSelectEntered);
            socketInteractor.selectExited.AddListener(OnSelectExited);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Get the name of the object placed in the socket and update the TextMeshPro text
        string objectName = args.interactableObject.transform.gameObject.name;
        UpdateText(objectName);
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        // Clear the TextMeshPro text when the object is removed from the socket
        UpdateText(string.Empty);
    }

    private void UpdateText(string text)
    {
        if (tmpText != null)
        {
            tmpText.text = text;
        }
    }

    private void OnDestroy()
    {
        if (socketInteractor != null)
        {
            // Unregister event listeners
            socketInteractor.selectEntered.RemoveListener(OnSelectEntered);
            socketInteractor.selectExited.RemoveListener(OnSelectExited);
        }
    }
}