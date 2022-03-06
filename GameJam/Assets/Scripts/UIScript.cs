using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    AudioSource audioSource;
    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
    public void PlayGame() {
        SceneManager.LoadScene("basicScene");
    }

    public void MainMenu() {
        SceneManager.LoadScene("Menu");
    }
    
    public void QuitGame() {
        Application.Quit();
    }
}
