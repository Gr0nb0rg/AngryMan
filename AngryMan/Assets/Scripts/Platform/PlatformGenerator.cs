using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct LaneData
{
    private int m_ID;
    private bool m_Open;
    public LaneData(bool open, int id)
    {
        m_Open = open;
        m_ID = id;
    }

    public bool GetOpen()
    {
        return m_Open;
    }

    public int GetId()
    {
        return m_ID;
    }
}

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
    public List<List<LaneData>> m_LaneInformation = new List<List<LaneData>>();

    [Header("Initial spawn platforms")]
    [Range(0, 100)]
    public int m_InitNumber = 10;

    //Spawns
    private GameObject m_Clone;
    private List<Vector3> m_Poslist = new List<Vector3>();
    private int m_BlockedIndex = -1;
    private List<LaneData> m_Lane1Data = new List<LaneData>();
    private List<LaneData> m_Lane2Data = new List<LaneData>();
    private List<LaneData> m_Lane3Data = new List<LaneData>();
    private int m_PlatformID = -1;

    //Platform counters
    private int m_TotalPlatNum = 1;
    private int m_CurPlatNum = 1;

	void Start()
    {
        m_Lane1Data.Add(new LaneData(true, -1));
        m_Lane2Data.Add(new LaneData(true, -1));
        m_Lane3Data.Add(new LaneData(true, -1));
        m_LaneInformation.Add(m_Lane1Data);
        m_LaneInformation.Add(m_Lane2Data);
        m_LaneInformation.Add(m_Lane3Data);

        //Add first base platform
        m_Poslist.Add(new Vector3(GetLane(), 0.0f, 0.0f));
        SpawnRandomPlatform(m_Poslist[0]);

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

            m_Poslist.Add(new Vector3(GetLane(), m_Poslist[i - 1].y + m_YIncrease, 0.0f));

            SpawnRandomPlatform(m_Poslist[i]);
        }
    }

    public void RemoveLastPosition()
    {
        m_CurPlatNum--;
    }

    float GetLane()
    {
        List<float> temp = new List<float>();
        for (int i = 0; i < m_Lanes.Count; i++)
        {
            if (i != m_BlockedIndex)
                temp.Add(m_Lanes[i]);
        }

        m_BlockedIndex = -1;

        int random = Random.Range(0, temp.Count);
        for (int i = 0; i < m_Lanes.Count; i++)
        {
            if (m_Lanes[i] == temp[random])
            {
                m_BlockedIndex = i;
                break;
            }
        }

        return temp[random];
    }

    void SpawnRandomPlatform(Vector3 position)
    {
        int random = Random.Range(0, m_PlatformPrefabs.Count);
        m_Clone = (GameObject)Instantiate(m_PlatformPrefabs[random], position, Quaternion.identity);

        if (m_Clone.GetComponent<PlatformIdentifier>())
            m_PlatformID = m_Clone.GetComponent<PlatformIdentifier>().m_ID;

        for (int i = 0; i < m_LaneInformation.Count; i++)
        {
            //if (m_LaneInformation[i][m_LaneInformation[i].Count].GetId == 0)
        }
    }
}
