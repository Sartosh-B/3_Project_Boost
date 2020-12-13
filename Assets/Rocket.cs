﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathClip;
    [SerializeField] AudioClip levelFinished;

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem deathClipParticle;
    [SerializeField] ParticleSystem levelFinishedParticle;

    AudioSource audioData;
    Rigidbody rigidBody;
    

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioData = GetComponent<AudioSource>();      
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
        RespondToRotateInput();
        RespondToThrustInput();
        }       
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; } // ignore collisions when dead

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("ok");
                break;
            case "Finish":
                StartSuccesSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }
    private void StartDeathSequence()
    {
        state = State.Dying;
        audioData.Stop();
        audioData.PlayOneShot(deathClip);
        deathClipParticle.Play();
        Invoke("LoadFirstLevel", 1f);
    }
    private void StartSuccesSequence()
    {
        state = State.Transcending;
        audioData.Stop();
        audioData.PlayOneShot(levelFinished);
        levelFinishedParticle.Play();
        Invoke("LoadNextLevel", 1f);
    }
    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }
    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space)) // cam thrust while rotating
        {
            ApplyThrust();            
        }      
        else
        {
            audioData.Stop();
            mainEngineParticle.Stop();
        }
    }
    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        if (!audioData.isPlaying)
        {
            audioData.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticle.isPlaying)
        {
            mainEngineParticle.Play();
        }
    }
    private void RespondToRotateInput()
    {             
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }        
    }

    
}
