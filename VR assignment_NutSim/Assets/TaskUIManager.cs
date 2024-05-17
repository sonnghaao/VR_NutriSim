using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskUIManager : MonoBehaviour
{
    public GameObject taskPanel;
    private int taskIndex;
    private string taskMsg;

    void Start()
    {
        taskIndex = 0;
        taskMsg = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        switch(taskIndex)
        {
           case 0: taskMsg = "Access into the laboratory."; 
                break;
           case 1: taskMsg = "Do Experiment"; break;
        }

        ShowPanelWithMessage(taskMsg);
    }

    void ShowPanelWithMessage(string message)
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
