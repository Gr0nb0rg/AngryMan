using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ImpactSound : MonoBehaviour {
    [SerializeField]
    private bool m_stopAmbienceOnCollision;
    [SerializeField]
    private List<AudioClip> m_clipPool;
    private AudioSource m_source;

    private bool m_hasImpacted = false;

	void Start () {
        m_source = GetComponent<AudioSource>();
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" && !m_hasImpacted)
        {
            if (m_clipPool.Count > 0)
            {
                if (m_source.isPlaying)
                    m_source.Stop();
                m_source.clip = m_clipPool[Random.Range(0, m_clipPool.Count - 1)];
                m_source.Play();
            }
            m_hasImpacted = true;
        }
    }
}
