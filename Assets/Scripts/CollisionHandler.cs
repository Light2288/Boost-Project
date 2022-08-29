using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float loadDelay = 2f;
    [SerializeField] private AudioClip explosion;
    [SerializeField] private AudioClip success;
    [SerializeField] private ParticleSystem successParticleSystem;
    [SerializeField] private ParticleSystem crashParticleSystem;

    private Movements _movements;
    private AudioSource _audioSource;

    private bool _isTransitioning = false;
    private bool _collisionDisabled = false;
    
    void Start()
    {
        _movements = GetComponent<Movements>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            _collisionDisabled = !_collisionDisabled;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(_isTransitioning || _collisionDisabled) { return; }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        _isTransitioning = true;
        _audioSource.Stop();
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(success);
        }

        if (!successParticleSystem.isPlaying && !crashParticleSystem.isPlaying)
        {
            successParticleSystem.Play();
        }
        _movements.enabled = false;
        Invoke(nameof(LoadNextLevel), loadDelay);
    }

    private void StartCrashSequence()
    {
        _isTransitioning = true;
        _audioSource.Stop();
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(explosion);
        }
        if (!successParticleSystem.isPlaying && !crashParticleSystem.isPlaying)
        {
            crashParticleSystem.Play();
        }
        _movements.enabled = false;
        Invoke(nameof(ReloadLevel), loadDelay);
    }

    private void ReloadLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void LoadNextLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var lastSceneIndex = SceneManager.sceneCountInBuildSettings;
        var sceneIndexToLoad = currentSceneIndex == lastSceneIndex - 1 ? 0: currentSceneIndex + 1;
        SceneManager.LoadScene(sceneIndexToLoad);

    }
}
