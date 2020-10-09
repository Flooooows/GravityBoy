using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    public Text highScoreTextLvl1;
    public Text highScoreTextLvl2;
    public Text highScoreTextLvl3;
    public Text highScoreTextLvl4;
    public Text highScoreTextLvl5;

    private int scoreLevel1;
    private int scoreLevel2;
    private int scoreLevel3;
    private int scoreLevel4;
    private int scoreLevel5;

    void Awake()
    {
        scoreLevel1 = PlayerPrefs.GetInt("HighScoreLevel1", 0);
        scoreLevel2 = PlayerPrefs.GetInt("HighScoreLevel2", 0);
        scoreLevel3 = PlayerPrefs.GetInt("HighScoreLevel3", 0);
        scoreLevel4 = PlayerPrefs.GetInt("HighScoreLevel4", 0);
        scoreLevel5 = PlayerPrefs.GetInt("HighScoreLevel5", 0);
        SetHighScoreText();
    }
    

    void SetHighScoreText()
    {
        highScoreTextLvl1.text = scoreLevel1.ToString() + "/15";
        highScoreTextLvl2.text = scoreLevel2.ToString() + "/15";
        highScoreTextLvl3.text = scoreLevel3.ToString() + "/15";
        highScoreTextLvl4.text = scoreLevel4.ToString() + "/15";
        highScoreTextLvl5.text = scoreLevel5.ToString() + "/15";
    }
}
