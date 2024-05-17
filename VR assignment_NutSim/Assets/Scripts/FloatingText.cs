using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public Camera mainCamera;
    public float distanceFromCamera = 2.0f;

    void Update()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (mainCamera != null)
        {
            Vector3 cameraForward = mainCamera.transform.forward;
            Vector3 newPosition = mainCamera.transform.position + cameraForward * distanceFromCamera;
            transform.position = newPosition;

            transform.LookAt(mainCamera.transform);
            transform.Rotate(0, 180, 0); // To ensure the text is not mirrored
        }
    }
}