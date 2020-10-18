using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float Score;
    public TextMeshProUGUI score;
    public float timeMultipler;
    private float bonus;

    public float highScore = 0;
    public TextMeshProUGUI highscoreText;

    void Start()
    {
        Score = 0;
        highScore = PlayerPrefs.GetFloat("highScore", highScore);
        highscoreText.SetText("HighScore: " + highScore.ToString("F0"));
    }

    // Update is called once per frame
    void Update()
    {
        Score += timeMultipler*Time.deltaTime; //Time alive: TimeMultipler score = 1 second alive
        score.SetText("SCORE: " + Score.ToString("F0"));
        if (Score > highScore)
        {
            highScore = Score;
            PlayerPrefs.SetFloat("highScore", highScore);
            highscoreText.SetText("High Score: " + highScore.ToString("F0"));
        }
    }

    public void AddScore(float bonus)
    {
        Score += bonus;
    }

}
