using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

    // Settings
    [Header("Settings")]
    [Header("Initial movement speed:")]
    [SerializeField]
    private float m_speed;

    [Header("Dash distance in tiles:")]
    [Range(0, 2)]
    [SerializeField]
    private float m_dashDistance;
    [SerializeField]
    private float m_dashSpeed;

    [SerializeField]
    private float m_laneSwitchSpeed;
    [SerializeField]
    private float m_screamForce;

    private int m_currentLane = 1;
    private bool m_inAction = false;

    private Dictionary<int, Vector2> Lanes;

    private Vector2 m_leftLanePos;
    private Vector2 m_rightLanePos;
    private Vector2 m_middleLanePos;

    // Components
    private Rigidbody m_rigidbody;
    private Collider m_collider;
    private Animator m_animator;

    private Rigidbody[] m_rigidbodies;
    private Collider[] m_colliders;

    private ScreamBox m_screamBox;

    private GameOverseer m_gameOverseer;

    void Awake ()
    {
        // Scale settings from non-retarded lvls to, well.. Yeah...
        m_speed *= 100;
        m_dashSpeed *= 100;
        m_laneSwitchSpeed *= 100;
        m_screamForce *= 100;

        m_gameOverseer = FindObjectOfType<GameOverseer>();


        m_dashDistance = GameSettings.m_distanceBetweenSquares * m_dashDistance;

        m_leftLanePos = new Vector2(-GameSettings.m_distanceBetweenSquares, 0);
        m_rightLanePos = new Vector2(GameSettings.m_distanceBetweenSquares, 0);
        m_middleLanePos = Vector2.zero;

        m_screamBox = FindObjectOfType<ScreamBox>();
        m_rigidbodies = GetComponentsInChildren<Rigidbody>();
        m_colliders = GetComponentsInChildren<Collider>();

        Lanes = new Dictionary<int, Vector2>();
        Lanes.Add(0, m_leftLanePos);
        Lanes.Add(1, m_middleLanePos);
        Lanes.Add(2, m_rightLanePos);
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_collider = GetComponent<Collider>();
        DisableRagdoll();

    }

    void DisableRagdoll()
    {
        for (int i = 0; i < m_rigidbodies.Length; i++)
        {
            if(m_rigidbodies[i].transform != this.transform)
            {
                m_rigidbodies[i].isKinematic = true;
               // m_rigidbodies[i].useGravity = false;
                m_colliders[i].enabled = false;
            }
            else
            {
                m_rigidbodies[i].isKinematic = false;
                m_colliders[i].enabled = true;
            }
            //m_rigidbodies[i].velocity = Vector3.zero;
        }
    }

    void EnableRagdoll(Vector3 initialVelocity)
    {
        StopAllCoroutines();
        m_animator.enabled = false;
        m_collider.enabled = false;
        m_rigidbody.isKinematic = true;
        for (int i = 0; i < m_rigidbodies.Length; i++)
        {
            if (m_rigidbodies[i].transform != this.transform)
            {
                m_rigidbodies[i].isKinematic = false;
                m_colliders[i].enabled = true;
                m_rigidbodies[i].velocity = -initialVelocity;
                //m_rigidbodies[i].useGravity = false;

            }
            else
            {
                //m_rigidbodies[i].isKinematic = true;
                //m_rigidbodies[i].useGravity = false;
                //m_colliders[i].enabled = false;
            }

            //m_rigidbodies[i].velocity = Vector3.zero;
        }
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

            if(Input.GetKeyDown(KeyCode.Space) && !m_inAction)
            {
                m_inAction = true;
                print("BLARGH");
                StartCoroutine(Scream());
            }
    }

    void MoveForward()
    {
        if(!m_inAction)
            m_rigidbody.velocity = Vector2.up * m_speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision col)
    {
        print(col.gameObject.tag);
        if (!m_inAction)
        {
            if(col.gameObject.tag != "Pushable")
                EnableRagdoll(col.relativeVelocity);
            m_gameOverseer.ActivateGameoverscreen();
        }  
    }

    IEnumerator Scream()
    {
        // Play sound
        // Add forces 
        if(m_screamBox.m_activeScreamTargets.Count > 0)
        {
            for (int i = 0; i < m_screamBox.ActiveScreamTargets.Count; i++)
            {
                if (m_screamBox.ActiveScreamTargets[i].gameObject.tag == "PushableActivator")
                {
                    m_screamBox.ActiveScreamTargets[i].gameObject.GetComponent<PropCharacter>().EnableRagdoll(Vector3.up * m_screamForce);
                }
                else
                {
                    //m_screamBox.ActiveScreamTargets[i].velocity = Vector3.up * m_screamForce;
                }
            }
            // wait for sound to stop
            yield return new WaitForSeconds(1);
            m_inAction = false;
        }
    }

    IEnumerator DashRight()
    {
        while (true)
        {
            if (transform.position.x >= Lanes[m_currentLane].x + GameSettings.m_distanceBetweenSquares)
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
            if (transform.position.x <= Lanes[m_currentLane].x - GameSettings.m_distanceBetweenSquares)
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
