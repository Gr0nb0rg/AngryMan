using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    //Public vars
    [Header("Score variables")]
    [Range(1.0f, 100000)]
    public float m_DistToScore = 100.0f;
    [Range(1, 100000)]
    public int m_ScoreAmount = 10;

    //Component vars
    private GameOverseer m_Overseer;

    //Distance vars
    private float m_CurrentDist = 0.0f;
    private Vector3 m_CurrentPos = Vector3.zero;

	void Start()
    {
        m_Overseer = GetComponent<GameOverseer>();
        if (!m_Overseer)
        {
            Debug.Log("Score keeper cannot find game-overseer!");
            enabled = false;
            return;
        }

        m_CurrentPos = transform.position;
	}
	
	void Update()
    {
        m_CurrentDist = Vector3.Distance(transform.position, m_CurrentPos);
        if (m_CurrentDist >= m_DistToScore)
        {
            m_Overseer.IncreaseScore(m_ScoreAmount);
            m_CurrentPos = transform.position;
        }
	}
}
