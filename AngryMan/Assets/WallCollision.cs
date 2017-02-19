using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour {

    PlayerController m_player;
	// Use this for initialization
	void Awake () {
        m_player = FindObjectOfType<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            if (m_player.InAction)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
