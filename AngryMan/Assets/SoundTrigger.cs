using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CapsuleCollider))]
public class SoundTrigger : MonoBehaviour {
    [SerializeField]
    private List<AudioClip> m_clipPool;
    private AudioSource m_source;
    private CapsuleCollider m_collider;

    void Start ()
    {
        m_source = GetComponent<AudioSource>();
        m_collider = GetComponent<CapsuleCollider>();
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