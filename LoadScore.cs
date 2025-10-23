using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadScore : MonoBehaviour
{

    public TMP_Text scoretext;
    public TMP_Text Highscortext;

    private static float highscore;
    // Start is called before the first frame update

    // Getting save from previous scene
    void Start()
    {
        Cursor.visible = true;
        float totalScore = ScoreCtrl.instance.GetForNextScene();

        scoretext.text = totalScore.ToString();

        if(totalScore > highscore)
        {
            highscore = totalScore;
        }
        Highscortext.text = highscore.ToString();
    }

    public void SavehighScore()
    {
        PlayerPrefs.SetFloat("Highscore", highscore);
        PlayerPrefs.Save();
    }
}
