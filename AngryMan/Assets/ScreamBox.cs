using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamBox : MonoBehaviour {
    private PlayerController m_playerController;
    public List<Rigidbody> m_activeScreamTargets;

    void Start () {
        m_playerController = FindObjectOfType<PlayerController>();
        m_activeScreamTargets = new List<Rigidbody>();

    }
	
    public List<Rigidbody> ActiveScreamTargets { get { return m_activeScreamTargets; } }

    void OnTriggerEnter(Collider col)
    {
        if ((col.gameObject.tag == "Pushable") || (col.gameObject.tag == "PushableActivator"))
        {
            if (!m_activeScreamTargets.Contains(col.GetComponent<Rigidbody>()))
            {
                m_activeScreamTargets.Add(col.GetComponent<Rigidbody>());
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if((col.gameObject.tag == "Pushable") || (col.gameObject.tag == "PushableActivator"))
        {
            if (m_activeScreamTargets.Contains(col.GetComponent<Rigidbody>()))
            {
                m_activeScreamTargets.Remove(col.GetComponent<Rigidbody>());
            }
        }
    }
}
