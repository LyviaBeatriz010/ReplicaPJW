using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int Score;
    public Text scoreText;

    public static GameController instance;
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    
    public void UpdateScore(int value)
    {
        Score += value;
        scoreText.text = Score.ToString();
    }
}
