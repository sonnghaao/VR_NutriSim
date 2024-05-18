using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{

    public Collider replayZone;
    public Collider menuZone;
    public AudioSource audioSource;
    public AudioClip beepSound;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object is entering the specified trigger zone
        if (other == replayZone)
        {
            audioSource.PlayOneShot(beepSound);
            Debug.Log("replay button touch");
        }

        if(other == menuZone)
        {
            audioSource.PlayOneShot(beepSound);
            Debug.Log("menu button touch");
        }

    }

}
