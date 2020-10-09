using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollectThings : MonoBehaviour
{

    public Text gemmesText;
    private int score;
    private Rigidbody2D myRigidbody;

    void Start() {
        score = 0;
        SetGemmesText();
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("PickUp"))
        {
            myRigidbody.gameObject.GetComponent<PlayerSoundManager>().playCollectSound();
            other.gameObject.SetActive(false);
            score++;
            string SceneName = "HighScore"+SceneManager.GetActiveScene().name;
            if (score > PlayerPrefs.GetInt(SceneName))
            {
                PlayerPrefs.SetInt(SceneName, score);
            }
            SetGemmesText();
        }

    }

    void SetGemmesText() {
        gemmesText.text = score.ToString() + "/ 15";
    }
}
