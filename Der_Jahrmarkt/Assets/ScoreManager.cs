using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public Text scoreText;
    public GameObject dose1;
    public GameObject dose2;
    public GameObject dose3;
    public GameObject dose4;
    public GameObject dose5;
    public GameObject dose6;
    public GameObject[] dosen;

    public bool dose1_gezaehlt = false;
    public bool dose2_gezaehlt = false;
    public bool dose3_gezaehlt = false;
    public bool dose4_gezaehlt = false;
    public bool dose5_gezaehlt = false;
    public bool dose6_gezaehlt = false;

    int score = 0;
    int highScore = 6;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
        dose1.transform.Rotate(2, 2, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if(score <= highScore)
        {
            AddPoint();
        }

    }

    public void AddPoint()
    {
        /* for(int i = 5; i <= 0; i++)
         {
             if(dosen[i].transform.position.x != 0 && dosen[i].transform.position.y != 0)
             {

                 score++;
                 scoreText.text = "Score: " + score.ToString();
             }
         } */
        if (dose1.transform.rotation.x != 0 && dose1.transform.rotation.y != 0 && score < highScore && dose1_gezaehlt == false)
        {
            score++;
            scoreText.text = "Score: " + score.ToString();
            dose1_gezaehlt = true;
        }

        if (dose2.transform.rotation.x != 0 && dose2.transform.rotation.y != 0 && score < highScore && dose2_gezaehlt == false)
        {
            score++;
            scoreText.text = "Score: " + score.ToString();
            dose2_gezaehlt = true;
        }

        if (dose3.transform.rotation.x != 0 && dose3.transform.rotation.y != 0 && score < highScore && dose3_gezaehlt == false)
        {
            score++;
            scoreText.text = "Score: " + score.ToString();
            dose3_gezaehlt = true;
        }

        if (dose4.transform.rotation.x != 0 && dose4.transform.rotation.y != 0 && score < highScore && dose4_gezaehlt == false)
        {
            score++;
            scoreText.text = "Score: " + score.ToString();
            dose4_gezaehlt = true;
        }

        if (dose5.transform.rotation.x != 0 && dose5.transform.rotation.y != 0 && score < highScore && dose5_gezaehlt == false)
        {
            score++;
            scoreText.text = "Score: " + score.ToString();
            dose5_gezaehlt = true;
        }

        if (dose6.transform.rotation.x != 0 && dose6.transform.rotation.y != 0 && score < highScore && dose6_gezaehlt == false)
        {
            score++;
            scoreText.text = "Score: " + score.ToString();
            dose6_gezaehlt = true;
        } 
    }
}
