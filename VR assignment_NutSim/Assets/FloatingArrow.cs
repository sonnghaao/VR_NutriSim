using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingArrow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float floatSpeed = 1.0f; // Speed of floating motion
    public float floatAmplitude = 0.5f; // Amplitude of floating motion

    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position of the arrow
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Make the arrow float up and down
        float newY = initialPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.localPosition = new Vector3(initialPosition.x, newY, initialPosition.z);

        // Make the arrow face the player
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // Keep the arrow horizontal
        transform.rotation = Quaternion.LookRotation(direction);
    }
}