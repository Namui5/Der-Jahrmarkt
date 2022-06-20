using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
   
    public void StartButton()
    {
        SceneManager.LoadScene("Jahrmarkt_v5");
    }
}
