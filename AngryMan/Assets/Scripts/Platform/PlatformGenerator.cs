using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformGenerator : MonoBehaviour
{
    //Public vars
    [Header("Spawnable objects")]
    public List<GameObject> m_PlatformPrefabs = new List<GameObject>();

    //Spawning variables
    [Header("Spawning variables")]
    public List<float> m_Lanes = new List<float>();
    [Range(1f, 100f)]
    public float m_YIncrease = 5.0f;

    [Header("Initial spawn platforms")]
    [Range(0, 100)]
    public int m_InitNumber = 10;

    //Spawns
    private GameObject m_Clone;
    private List<Vector3> m_Poslist = new List<Vector3>();
    private int m_LastSpawnIndex = 0;
    private int m_PlatformID = -1;
    public List<int> m_Lane1ID = new List<int>();
    public List<int> m_Lane2ID = new List<int>();
    public List<int> m_Lane3ID = new List<int>();
    public List<List<int>> m_Identifiers = new List<List<int>>();

    //Platform counters
    private int m_TotalPlatNum = 1;
    private int m_CurPlatNum = 1;

	void Start()
    {
        m_Lane1ID.Add(-1);
        m_Lane2ID.Add(-1);
        m_Lane3ID.Add(-1);

        m_Identifiers.Add(m_Lane1ID);
        m_Identifiers.Add(m_Lane2ID);
        m_Identifiers.Add(m_Lane3ID);

        //Add first base platform
        m_Poslist.Add(Vector3.zero);
        SpawnRandomPlatform();

        GeneratePlatforms(m_InitNumber);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            GeneratePlatforms(5);

        //Generate more platforms if current number is low
        if (m_CurPlatNum <= 4)
        {
            GeneratePlatforms(5);
        }
	}

    void GeneratePlatforms(int num)
    {
        //Select platform to generate - get its ID
        //Find a suitable lane to spawn it in, spawn it
        //Assign last ID to corresponding lane

        int temp = m_TotalPlatNum + num;

        for (int i = m_TotalPlatNum; i < temp; i++)
        {
            m_TotalPlatNum++;
            m_CurPlatNum++;

            SpawnRandomPlatform();
        }
    }

    public void RemoveLastPosition()
    {
        m_CurPlatNum--;
    }

    void SpawnRandomPlatform()
    {
        int random = Random.Range(0, m_PlatformPrefabs.Count);
        m_Clone = (GameObject)Instantiate(m_PlatformPrefabs[random], Vector3.zero, Quaternion.identity);

        if (m_Clone.GetComponent<PlatformIdentifier>())
            m_PlatformID = m_Clone.GetComponent<PlatformIdentifier>().m_ID;

        //Get lane
        List<float> temp = new List<float>();
        for (int i = 0; i < m_Lanes.Count; i++)
        {
            if (m_Identifiers[i][m_Identifiers[i].Count - 1] != m_PlatformID)
                temp.Add(m_Lanes[i]);

            //if (i != m_LastSpawnIndex)
            //    temp.Add(m_Lanes[i]);
        }

        m_LastSpawnIndex = -1;

        int randomLane = Random.Range(0, temp.Count);
        float y = m_Poslist[m_Poslist.Count - 1].y;
        m_Poslist.Add(new Vector3(temp[randomLane], y + m_YIncrease, 0.0f));
        m_Clone.transform.position = m_Poslist[m_Poslist.Count - 1];
        for (int i = 0; i < m_Lanes.Count; i++)
        {
            if (m_Lanes[i] == temp[randomLane])
            {
                m_LastSpawnIndex = i;
                m_Identifiers[i].Add(m_PlatformID);
            }
            else
            {
                m_Identifiers[i].Add(-1);
            }
        }

        //string output = "";
        //for (int i = 0; i < m_Identifiers[0].Count; i++)
        //{
        //    for (int j = 0; j  < m_Identifiers.Count; j++)
        //    {
        //        output += m_Identifiers[j][i].ToString() + " ";
        //    }
        //    Debug.Log(output);
        //    output = "";
        //}

        //for (int i = 0; i < m_Identifiers.Count; i++)
        //{

        //    for (int j = 0; j < m_Identifiers[i].Count; j++)
        //    {
        //        Debug.Log(m_Identifiers[i][j]);
        //    }
        //}
    }
}
