using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider))]
public class SoundTrigger : MonoBehaviour {
    [SerializeField]
    private List<AudioClip> m_clipPool;
    private AudioSource m_source;
    private Collider m_collider;

    void Awake()
    {
        m_source = GetComponent<AudioSource>();
        m_collider = GetComponent<Collider>();
        //m_collider.radius = 13;
    }

    void Start ()
    {

	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(m_clipPool.Count > 0)
            {
                m_source.clip = m_clipPool[Random.Range(0, m_clipPool.Count-1)];
                m_source.Play();
            }
        }
    }
}