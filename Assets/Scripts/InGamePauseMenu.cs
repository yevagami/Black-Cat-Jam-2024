using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGamePauseMenu : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pauseMenu;
    public GameObject result;
    public GameObject playerTrail;
    public TextMeshProUGUI resultMessage;
    public bool isPaused = false;
    

    public void Pause() {
        isPaused = true;
        Time.timeScale = 0.0f;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
        playerTrail.SetActive(false);
    }

    public void Resume() { 
        isPaused = false;
        Time.timeScale = 1.0f;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        playerTrail.SetActive(true);
    }

    public void Quit() {
        Time.timeScale = 1.0f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void ResultScreen(float score) {
        Time.timeScale = 0.0f;
        result.SetActive(true);
        resultMessage.text = string.Format("GAME OVER\n Total Points: {0}", score);
    }


}
