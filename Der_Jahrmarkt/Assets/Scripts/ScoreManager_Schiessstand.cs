using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager_Schiessstand : MonoBehaviour
{
 	public static ScoreManager_Schiessstand instance;
	
	public Text scoreText;
	public Text highscoreText;
	
	int score = 0;
	int highscore = 0;
	
     private void Awake()
 	{
        instance = this;
     }
	
    void Start()
    {
		highscore = PlayerPrefs.GetInt("highscore", 0);
		
        scoreText.text = "Score: " + score.ToString();
		highscoreText.text = "Highscore: " + highscore.ToString();
    }

    public void AddPoint(int addScore)
    {
        score += addScore;
	 	scoreText.text = "Score: " + score.ToString();
		
		if (highscore < score)
			PlayerPrefs.SetInt("highscore", score);
    }
}
