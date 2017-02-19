using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverseer : MonoBehaviour {

    private int m_score = 0;

    private TextMesh m_currentScoreText;
    private GameObject m_gameoverTextObject;
    private TextMesh m_finalScoreText;
    private TextMesh m_highscoreText;

    void Awake()
    {
        m_currentScoreText = GameObject.Find("CurrentScoreText").GetComponent<TextMesh>();
        m_gameoverTextObject = GameObject.Find("GameoverObject");
        m_finalScoreText = GameObject.Find("HighscoreText").GetComponent<TextMesh>();
        m_highscoreText = GameObject.Find("YourScoreText").GetComponent<TextMesh>();



        m_currentScoreText.gameObject.SetActive(true);
        m_gameoverTextObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ReloadScene();
    }

    public void IncreaseScore(int amount)
    {
        m_score += amount;
        m_currentScoreText.text = m_score.ToString();
    }

    public void ActivateGameoverscreen()
    {
        m_currentScoreText.gameObject.SetActive(false);
        m_gameoverTextObject.SetActive(true);

        m_finalScoreText.text = m_score.ToString();
        if(m_score > PlayerPrefs.GetInt("Highscore", 0))
            PlayerPrefs.SetInt("Highscore", m_score);
        m_highscoreText.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
    }


    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }


}
