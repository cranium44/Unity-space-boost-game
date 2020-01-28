﻿
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    //game configurations
    [SerializeField] float rcsThrust = 100;
    [SerializeField] float verticalThrust = 500;
    Rigidbody rigidbody;
    AudioSource audioSource;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Thrust();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("ok");
                break;
            default:
                print("dead");
                break;
        }
    }

    private void Rotate()
    {
        rigidbody.freezeRotation = true; //take manual control of rotation
        float rotationSpeed = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }
        rigidbody.freezeRotation = false; //resume physics
    }

    private void Thrust()
    {
        float rotationSpeed = verticalThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space)|| Input.GetKey(KeyCode.UpArrow))
        {
            rigidbody.AddRelativeForce(Vector3.up * rotationSpeed);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
                particleSystem.Play();
                
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
