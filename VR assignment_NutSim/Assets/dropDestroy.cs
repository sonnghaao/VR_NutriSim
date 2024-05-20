using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropDestroy : MonoBehaviour
{
    public float destroyAfterSeconds = 1.5f; // Time in seconds before the object is destroyed

    void Start()
    {
        // Destroy the game object after the specified time
        Destroy(gameObject, destroyAfterSeconds);
    }
}
