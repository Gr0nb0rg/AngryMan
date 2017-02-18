using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerController : MonoBehaviour {

    [Header("Settings")]
    // Settings
    [SerializeField]
    private float m_speed;
    [SerializeField]
    private float m_dashSpeed;
    [SerializeField]
    private float m_laneSwitchSpeed;

    [SerializeField]
    private int m_currentLane;

    [Space(10)]
    [SerializeField]
    private float m_distanceBetweenLanes;
    [SerializeField]
    private float m_dashDistance;

    private bool m_inAction = false;

    private Dictionary<int, Vector2> Lanes;

    private Vector2 m_leftLanePos;
    private Vector2 m_rightLanePos;
    private Vector2 m_middleLanePos;

    // Components
    private Rigidbody2D m_rigidbody;

	void Start ()
    {
        m_leftLanePos = new Vector2(-m_distanceBetweenLanes, 0);
        m_rightLanePos = new Vector2(m_distanceBetweenLanes, 0);
        m_middleLanePos = Vector2.zero;

        m_rigidbody = GetComponent<Rigidbody2D>();
        Lanes = new Dictionary<int, Vector2>();
        Lanes.Add(0, m_leftLanePos);
        Lanes.Add(1, m_middleLanePos);
        Lanes.Add(2, m_rightLanePos);
        m_rigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
        HandleInput();
        MoveForward();
    }

    void HandleInput()
    {
            if (Input.GetKeyDown(KeyCode.D) && (m_currentLane < 2) && !m_inAction)
            {
                m_inAction = true;
                print("Went Right");
                StartCoroutine(DashRight());
            }

            if (Input.GetKeyDown(KeyCode.A) && (m_currentLane > 0) && !m_inAction)
            {
                m_inAction = true;
                print("Went Left");
                StartCoroutine(DashLeft());
            }

            if(Input.GetKeyDown(KeyCode.W) && !m_inAction)
            {
                m_inAction = true;
                print("Went Up");
                StartCoroutine(DashForward());
            }
    }

    void MoveForward()
    {
        if(!m_inAction)
            m_rigidbody.velocity = Vector2.up * m_speed * Time.deltaTime;
    }

    IEnumerator DashRight()
    {
        while (true)
        {
            if (transform.position.x >= Lanes[m_currentLane].x + m_distanceBetweenLanes)
            {
                m_rigidbody.velocity = new Vector2(0, 0);
                m_currentLane++;
                transform.position = new Vector3(Lanes[m_currentLane].x, transform.position.y, transform.position.z);
                m_inAction = false;
                break;
            }
            m_rigidbody.velocity = new Vector2(1 * m_laneSwitchSpeed * Time.deltaTime, m_rigidbody.velocity.y);
            yield return null;
        }
    }

    IEnumerator DashLeft()
    {
        while (true)
        {
            if (transform.position.x <= Lanes[m_currentLane].x - m_distanceBetweenLanes)
            {
                m_rigidbody.velocity = new Vector2(0, 0);
                m_currentLane--;
                transform.position = new Vector3(Lanes[m_currentLane].x, transform.position.y, transform.position.z);
                m_inAction = false;
                break;
            }
            m_rigidbody.velocity = new Vector2(-1 * m_laneSwitchSpeed * Time.deltaTime, m_rigidbody.velocity.y);
            yield return null;
        }
    }

    IEnumerator DashForward()
    {
        Vector2 startPos = transform.position;
        Vector2 goalPos = startPos + new Vector2(0, m_dashDistance);
        while (true)
        {
            if (transform.position.y >= goalPos.y)
            {
                m_rigidbody.velocity = new Vector2(0, 0);
                transform.position = new Vector3(transform.position.x, goalPos.y, transform.position.z);
                m_inAction = false;
                break;
            }
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, m_dashSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
