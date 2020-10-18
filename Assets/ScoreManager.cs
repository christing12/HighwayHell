using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float Score;
    public TextMeshProUGUI score;
    private float bonus;
    void Start()
    {
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        score.SetText(Score.ToString());
    }

    public void AddScore(float bonus)
    {
        Score += bonus;
    }

}
