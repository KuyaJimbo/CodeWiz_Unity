using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    // score float
    public float score;
    // reference of text ui legacy version
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // update the score text
        scoreText.text = "Score: " + score;
    }

    public void AddScore(float points)
    {
        score += points;
    }


}
