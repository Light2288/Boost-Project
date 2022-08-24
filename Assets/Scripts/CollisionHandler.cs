using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private Movements _movements;
    [SerializeField] private float loadDelay = 2f;
    void Start()
    {
        _movements = GetComponent<Movements>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
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
        _movements.enabled = false;
        Invoke(nameof(LoadNextLevel), loadDelay);
    }

    private void StartCrashSequence()
    {
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
