using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class TextTrigger : MonoBehaviour
{
    //Public vars
    [Header("Text variables")]
    public string m_Prompt;
    [Range(0.1f, 10f)]
    public float m_PromptTime = 2.0f;

    //Component vars
    private Text m_Text;

    //Timer vars
    private bool m_IsActive = false;
    private float m_PromptTimer = 0.0f;

    void Awake()
    {
        m_Text = GetComponentInChildren<Text>();
        if (!m_Text)
        {
            Debug.Log("Text trigger could no find its text component!");
            enabled = false;
            return;
        }

        m_Text.text = m_Prompt;
        m_Text.gameObject.SetActive(false);
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //    ActivateText();

        if (m_IsActive)
        {
            m_PromptTimer += Time.deltaTime;
            if (m_PromptTimer >= m_PromptTime)
            {
                m_PromptTimer = 0.0f;
                m_IsActive = false;
                m_Text.gameObject.SetActive(false);
            }
        }
    }

    void ActivateText()
    {
        m_Text.gameObject.SetActive(true);
        m_IsActive = true;
    }

    void OnTriggerEnter(Collider col)
    {
        ActivateText();
    }
}
