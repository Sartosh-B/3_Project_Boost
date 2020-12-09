using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    AudioSource audioData;
    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioData = GetComponent<AudioSource>();      
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space)) // cam thrust while rotating
        {
            rigidBody.AddRelativeForce(Vector3.up);
            if (!audioData.isPlaying)
            {
                audioData.Play();
            }
        }
        else
        {
            audioData.Stop();
        }
     
        if (Input.GetKey(KeyCode.A)) 
        {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
        }

    }
}
