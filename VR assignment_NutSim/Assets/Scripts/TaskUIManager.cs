using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public AudioClip warmMilkSpeech;
    public AudioClip setStrainerSpeech;
    public ScreenFader screenFader;


    private static int taskIndex;
    private string taskMsg;

    private bool hasShownStartGuide;
    private bool hasShownWelcomeMessage;
    private bool hasShownBunsenMessage;
    private bool hasShownMilkOnMessage;
    private bool hasShownSetStrainerMsg;
    private bool isBackMenu;
    private int gatherCount;
    private int safetyEquipCount;

    void Start()
    {
        taskIndex = 0;
        taskMsg = string.Empty;
        hasShownStartGuide = false;
        hasShownWelcomeMessage = false;
        hasShownBunsenMessage = false;
        hasShownMilkOnMessage = false;
        hasShownSetStrainerMsg = false;
        isBackMenu = false;
        gatherCount = 0;
        safetyEquipCount = 0;
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
           case 0: taskMsg = "Access into the laboratory.(" + safetyEquipCount + "/3)"; 
                break;
           case 1:
                taskMsg = "Gathering the required materials.(" + gatherCount + "/5)";
                if (!hasShownWelcomeMessage)
                {
                    
                    StartCoroutine(ShowPanelWithMessage("Welcome to Experiment Turn Milk into Cheese", 6f, "Please proceed to the black table at the back to gather the required materials and tools."));
                    hasShownWelcomeMessage = true;
                }
                break;
            case 2:
                taskMsg = "Turn on the Bunsen burner";
                if (!hasShownBunsenMessage)
                {
                    
                    audioSource.PlayOneShot(bunsenSpeech);
                    hasShownBunsenMessage = true;
                }
                break;
            case 3:
                taskMsg = "Warm up the milk.";
                if (!hasShownMilkOnMessage)
                {
                    audioSource.PlayOneShot(warmMilkSpeech);
                    hasShownMilkOnMessage=true;
                }
                break;
            case 4:
                taskMsg = "Squeeze lemon into milk";
                break;
            case 5:
                if (!hasShownSetStrainerMsg)
                {
                    taskMsg = "Set the strainer over a glass bowl.";
                    audioSource.PlayOneShot(setStrainerSpeech);
                    ShowPanelWithMessage3("The milk should curdle now. Next, place the strainer over the glass bowl.");
                    hasShownSetStrainerMsg = true;
                }
                break;
            case 6:
                taskMsg = "Pour the curds into strainer";
                break;
            case 7:
                taskMsg = "Pour the cheese into a clean Bowl";
                break;
            case 8:
                if (!isBackMenu)
                {
                    taskMsg = "Congratulation!";
                    StartCoroutine(BackToMainMenu());
                    isBackMenu = true;
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

    IEnumerator BackToMainMenu()
    {
        yield return new WaitForSeconds(12f);
        yield return StartCoroutine(screenFader.FadeIn());
        SceneManager.LoadScene("1 Start Scene");
    }

    void ShowPanelWithMessage3(string message)
    {
        panel.SetActive(true);
        TMP_Text textMeshPro = panel.GetComponentInChildren<TMP_Text>();
        if (textMeshPro != null)
        {
            textMeshPro.text = message;
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

    public void TaskIndexDec()
    {
        taskIndex--;
    }

    public void GatherCountInc()
    {
        gatherCount++;
    }

    public void GatherCountDec()
    {
        gatherCount--;
    }

    public void SafetyEquipCountInc()
    {
        safetyEquipCount++;
    }

}
