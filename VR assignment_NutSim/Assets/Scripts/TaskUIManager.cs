using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskUIManager : MonoBehaviour
{
    public GameObject taskPanel;
    public GameObject panel;
    public AudioSource audioSource;
    public AudioClip startMsg1;
    public AudioClip startMsg2;
    public AudioClip welcomeSpeech;
    public AudioClip gatherSpeech;
    public AudioClip bunsenSpeech;


    private int taskIndex;
    private string taskMsg;

    private bool hasShownStartGuide;
    private bool hasShownWelcomeMessage;
    private bool hasShownBunsenMessage;

    void Start()
    {
        taskIndex = 0;
        taskMsg = string.Empty;
        hasShownWelcomeMessage = false;
        hasShownBunsenMessage = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(!hasShownStartGuide)
        {
            StartCoroutine(ShowPanelWithMessage2("Hi, you can move around using thumbstick on left-controller and turn using thumbstick on the rightside. ", 6f, "Also, you can grab objects with the grip buttons and use them with trigger buttons."));
            hasShownStartGuide=true;
        }

        switch(taskIndex)
        {
           case 0: taskMsg = "Access into the laboratory."; 
                break;
           case 1: if (!hasShownWelcomeMessage)
                {
                    taskMsg = "Gathering the required materials ";
                    StartCoroutine(ShowPanelWithMessage("Welcome to Experiment Turn Milk into Cheese", 6f, "Please proceed to the black table at the back to gather the required materials."));
                    hasShownWelcomeMessage = true;
                }
                break;
            case 2:
                if (!hasShownBunsenMessage)
                {
                    taskMsg = "Turn on the Bunsen burner";
                    audioSource.PlayOneShot(bunsenSpeech);
                    hasShownBunsenMessage = true;
                }
                break;
        }

        ShowTaskPanelWithMessage(taskMsg);
    }

    IEnumerator ShowPanelWithMessage(string message, float delay, string nextMessage)
    {
        panel.SetActive(true);
        TMP_Text textMeshPro = panel.GetComponentInChildren<TMP_Text>();
        audioSource.PlayOneShot(welcomeSpeech);
        if (textMeshPro != null)
        {
            textMeshPro.text = message;
        }

        yield return new WaitForSeconds(delay);

        audioSource.PlayOneShot(gatherSpeech);
        panel.SetActive(true);
        if (textMeshPro != null)
        {
            textMeshPro.text = nextMessage;
        }

    }

    IEnumerator ShowPanelWithMessage2(string message, float delay, string nextMessage)
    {
        panel.SetActive(true);
        audioSource.PlayOneShot(startMsg1);
        TMP_Text textMeshPro = panel.GetComponentInChildren<TMP_Text>();
        if (textMeshPro != null)
        {
            textMeshPro.text = message;
        }

        yield return new WaitForSeconds(delay);

        
        panel.SetActive(true);
        audioSource.PlayOneShot(startMsg2);
        if (textMeshPro != null)
        {
            textMeshPro.text = nextMessage;
        }

    }


    void ShowTaskPanelWithMessage(string message)
    {
        TMP_Text textMeshPro = taskPanel.GetComponentInChildren<TMP_Text>();
        if (textMeshPro != null)
        {
            textMeshPro.text = message;
        }
    }

    public void TaskIndexInc()
    {
        taskIndex++;
    }
}
