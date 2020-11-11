using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // pontuação 
    public Text lastScore;
    public Text menuBestScore;

    private static string BEST_SCORE = "BEST_SCORE";
    private static string LAST_SCORE = "LAST_SCORE";

    public void Start()
    {
        lastScore.text = PlayerPrefs.GetInt(LAST_SCORE).ToString();
        menuBestScore.text = PlayerPrefs.GetInt(BEST_SCORE).ToString();
    }

}