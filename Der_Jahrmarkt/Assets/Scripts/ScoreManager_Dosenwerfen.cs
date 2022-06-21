using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScoreManager_Dosenwerfen : MonoBehaviour
{

    public Text scoreText;
    public GameObject dose1;
    public GameObject dose2;
    public GameObject dose3;
    public GameObject dose4;
    public GameObject dose5;
    public GameObject dose6;
	
    private GameObject[] dosen = new GameObject[6];
    private bool[] gezaehlt = new bool[6];

    int score = 0;
    int highScore = 6;

    // Start is called before the first frame update
    void Start()
    {
		
        scoreText.text = "Score: " + score.ToString();
		dosen[0] = dose1;
        dosen[1] = dose2;
        dosen[2] = dose3;
        dosen[3] = dose4;
        dosen[4] = dose5;
        dosen[5] = dose6;

        // dose1.transform.Rotate(2, 2, 2);
    }

    // Update is called once per frame
    void Update()
    {
		// Schleife für jede Dose
        for(int i = 0; i < 6; i++)
		{
			if(gezaehlt[i] == false)
			{
				// vergleicht den Winkel zwischen dem "Oben" der Umgebung mit dem "Oben" von einer Dose -> wenn er größer ist als 70, wird die Dose gezählt
				if(Vector3.Angle(Vector3.up, dosen[i].transform.up) > 70)
				{
					gezaehlt[i] = true;
				}
			}
		}
		
		// zählt den Score nach oben (50 Punkte pro Dose) und gibt es auf der Tafel aus
		score = gezaehlt.Count(c => c) * 50;
		scoreText.text = "Score: " + score.ToString();
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
         } 
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
        } */
    }
}
