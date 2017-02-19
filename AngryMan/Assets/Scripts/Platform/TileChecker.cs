using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TileChecker : MonoBehaviour
{
    //Component vars
    private TileGenerator m_Generator;
    private Transform m_Move;

    void Start()
    {
        m_Generator = FindObjectOfType<TileGenerator>();
        if (!m_Generator)
        {
            Debug.Log("Tile checker could not find tile generator!");
            enabled = false;
            return;
        }

        m_Move = transform.parent.transform;
    }

    void Update()
    {
        //if (m_Move)
        //{
        //    if (Input.GetKey(KeyCode.W))
        //        m_Move.Translate(0, 10, 0);
        //    else if (Input.GetKey(KeyCode.S))
        //        m_Move.Translate(0, -10, 0);
        //}
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "GeneratedChunk")
        {
            Destroy(col.gameObject);
            m_Generator.CreateLevel();
        }
    }
}
