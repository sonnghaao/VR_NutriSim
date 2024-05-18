using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BunsenBurner : MonoBehaviour
{
    public TaskUIManager taskUIManager;

    private XRGrabInteractable grabInteractable;
    private GameObject fireParticles;
    public GameObject milkSocket;

    private bool isProceed;
    private bool isBurnerOn;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(x => ToggleBurner());

        // Find the Fire Particles object in the children
        fireParticles = transform.Find("Fire Particles").gameObject;
        isProceed = false;
        isBurnerOn = false;
    }

    public void ToggleBurner()
    {
        if (fireParticles != null)
        {
            if (isBurnerOn)
            {
                // Turn off the burner
                fireParticles.SetActive(false);
                milkSocket.SetActive(false);
            }
            else
            {
                // Turn on the burner
                fireParticles.SetActive(true);
                milkSocket.SetActive(true);
                if (!isProceed)
                {
                    taskUIManager.TaskIndexInc();
                    isProceed = true;
                }
            }
            isBurnerOn = !isBurnerOn; // Toggle the state
        }
    }
}
