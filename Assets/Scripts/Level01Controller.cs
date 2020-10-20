using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView;

    int _currentScore;
    public GameObject pauseMenu;


    void Update()
    {
        // increase score
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IncreaseScore(5);
        }
        // pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }

        if (pauseMenu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void IncreaseScore(int scoreIncrease)
    {
        // increase score
        _currentScore += scoreIncrease;
        // update score display
        _currentScoreTextView.text =
            "Defeat the enemy!";
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene("Level01");
    }

    public void ExitLevel()
    {
        // compare score to highscore
        int highScore = PlayerPrefs.GetInt("HighScore");
        if (_currentScore > highScore)
        {
            // save current score as new highscore
            PlayerPrefs.SetInt("HighScore", _currentScore);
            Debug.Log("New high score: " + _currentScore);
        }
        // load new level
        SceneManager.LoadScene("MainMenu");
    }
}
