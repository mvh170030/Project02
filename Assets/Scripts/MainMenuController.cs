using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    
    [SerializeField] Text _highScoreTextView;

    void Start()
    {
        // load hs score display
        int highScore = PlayerPrefs.GetInt("HighScore");
        _highScoreTextView.text = highScore.ToString();
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        int highScore = PlayerPrefs.GetInt("HighScore");
        _highScoreTextView.text = highScore.ToString();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
