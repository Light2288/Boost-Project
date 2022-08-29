using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    [SerializeField] private float mainThrust = 1000f;
    [SerializeField] private float rotationThrust = 15f;
    [SerializeField] private AudioClip engineThrust;
    [SerializeField] private ParticleSystem rightSideRocketParticleSystem;
    [SerializeField] private ParticleSystem leftSideRocketParticleSystem;
    [SerializeField] private ParticleSystem mainRocketParticleSystem;
    
    private Rigidbody _rigidbody;
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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        _rigidbody.AddRelativeForce(CalculateThrust(Vector3.up, mainThrust));
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(engineThrust);
        }

        PlayRocketParticle(mainRocketParticleSystem);
    }
    
    private void StopThrusting()
    {
        _audioSource.Stop();
        mainRocketParticleSystem.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(Vector3.forward);
            PlayRocketParticle(rightSideRocketParticleSystem);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(Vector3.back);
            PlayRocketParticle(leftSideRocketParticleSystem);
        }
        else
        {
            leftSideRocketParticleSystem.Stop();
            rightSideRocketParticleSystem.Stop();
        }
    }

    private void ApplyRotation(Vector3 direction)
    {
        _rigidbody.freezeRotation = true;
        transform.Rotate(CalculateThrust(direction, rotationThrust));
        _rigidbody.freezeRotation = false;
    }
    
    private void PlayRocketParticle(ParticleSystem ps)
    {
        if (!ps.isPlaying)
        {
            ps.Play();
        }
    }

    private Vector3 CalculateThrust(Vector3 vector3, float thrust)
    {
         return vector3 * (thrust * Time.deltaTime);
    }
}
