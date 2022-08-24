using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private float mainThrust = 1000f;
    [SerializeField] private float rotationThrust = 15f;
    private AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddRelativeForce(CalculateThrust(Vector3.up, mainThrust));
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
        else
        {
            _audioSource.Stop();
        }
    }
    
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(Vector3.back);;
        }
    }

    private void ApplyRotation(Vector3 direction)
    {
        _rigidbody.freezeRotation = true;
        transform.Rotate(CalculateThrust(direction, rotationThrust));
        _rigidbody.freezeRotation = false;
    }

    private Vector3 CalculateThrust(Vector3 vector3, float thrust)
    {
         return vector3 * (thrust * Time.deltaTime);
    }
}
