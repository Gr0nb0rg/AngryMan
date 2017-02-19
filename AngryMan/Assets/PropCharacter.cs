using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropCharacter : MonoBehaviour {

    // Components
    private Rigidbody m_rigidbody;
    private Collider m_collider;

    public Rigidbody[] m_rigidbodies;
    public Collider[] m_colliders;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_collider = GetComponent<Collider>();

        m_colliders = GetComponentsInChildren<Collider>();
        m_rigidbodies = GetComponentsInChildren<Rigidbody>();

        DisableRagdoll();
    }

    void Start () {

    }

    void DisableRagdoll()
    {
        for (int i = 0; i < m_rigidbodies.Length; i++)
        {
            if (m_rigidbodies[i].transform != this.transform)
            {
                m_rigidbodies[i].isKinematic = true;
                m_colliders[i].enabled = false;
            }
            else
            {
                m_rigidbodies[i].isKinematic = true;
                m_colliders[i].enabled = true;
            }
        }
    }

    public void EnableRagdoll(Vector3 initialVelocity)
    {
        m_collider.enabled = false;
        //m_rigidbody.isKinematic = true;
        for (int i = 0; i < m_rigidbodies.Length; i++)
        {
            if (m_rigidbodies[i].transform != this.transform)
            {
                m_rigidbodies[i].isKinematic = false;
                m_colliders[i].enabled = true;
                m_rigidbodies[i].velocity = initialVelocity*0.5f;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        EnableRagdoll(col.relativeVelocity);
    }
}
