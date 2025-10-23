using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCtrl : MonoBehaviour
{
    public static ScoreCtrl instance;
    public TMP_Text txtScore;
    public TMP_Text highscoretext;
    float totalScore = 0;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("AddScoreForTime");
    }
    //Automatically adds points over time
    IEnumerator AddScoreForTime()
    {
        while (true)
        {
            //score scheme
            yield return new WaitForSeconds(2);
            //value of extra points
            DoAddScore(1);
        }
    }
    //extra credit operation 
    public void DoAddScore(float score)
    {
        totalScore += score;
        txtScore.text = totalScore.ToString();
    }

    public void SaveScore()
    {
        PlayerPrefs.SetFloat("TotalScore", totalScore);
        PlayerPrefs.Save();
    }

    void LoadScore()
    {
        totalScore = PlayerPrefs.GetFloat("TotalScore", 0);
        txtScore.text = totalScore.ToString();
    }

    public float GetForNextScene()
    {
        return totalScore;
    }
}
