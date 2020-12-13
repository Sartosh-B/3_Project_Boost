using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    AudioSource audioData;
    Rigidbody rigidBody;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioData = GetComponent<AudioSource>();      
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Thrust();
    }
    private void Thrust()
    {     
        if (Input.GetKey(KeyCode.Space)) // cam thrust while rotating
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audioData.isPlaying)
            {
                audioData.Play();
            }
        }
        else
        {
            audioData.Stop();
        }
    }
    private void Rotate()
    {
        rigidBody.freezeRotation = true; //take manual control of rotation

        
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; //resume physic control of rotation
    }

    
}
