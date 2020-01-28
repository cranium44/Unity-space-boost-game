
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
    [SerializeField] AudioClip endJingle;
    [SerializeField] AudioClip deathExplosion;
    [SerializeField] ParticleSystem fire;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] ParticleSystem fireworks;

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
        // int current = SceneManager.GetActiveScene()
        switch (collision.gameObject.tag)
        {
            case "Start":
            case "Friendly":
                print("ok");
                break;
            case "Finish":
                audioSource.Stop();
                audioSource.PlayOneShot(endJingle);
                fireworks.Play();
                SceneManager.LoadScene(1);
                break;
            default:
                SceneManager.LoadScene(0);
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
                fire.Play();
                
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
