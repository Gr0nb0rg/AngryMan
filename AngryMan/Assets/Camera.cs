using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public Transform m_player;
    public float m_offset;
    public float m_lerpSpeed;
	
	void Update () {
        transform.position = new Vector3(transform.position.x, m_player.position.y+ m_offset, transform.position.z);
        //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,m_player.position.y + m_offset, transform.position.z), m_lerpSpeed * Time.deltaTime);
	}
}
