using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] GameObject explotionParticles;

    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
        explotionParticles.SetActive(true);
        Invoke("ReloadLevel", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(1);
    }
}
