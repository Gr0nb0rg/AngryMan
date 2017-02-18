using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileGenerator : MonoBehaviour
{
    //Public vars
    [Header("Spawnable objects")]
    public GameObject m_LevelPiecePrefab;
    public GameObject m_ChunkPrefab;
    public List<GameObject> m_TilePrefabs = new List<GameObject>();

    //Spawning variables
    [Header("Spawning variables")]
    public List<float> m_Lanes = new List<float>();
    [Range(1f, 10000)]
    public float m_YIncrease = 5.0f;
    [Range(1, 10)]
    public int m_EmptyLaneCounter = 3;
    public bool m_RerollSame = false;
    [Range(1, 10)]
    public int m_RerollTimes = 1;

    [Header("Initial spawn platforms")]
    [Range(0, 100)]
    public int m_InitNumber = 10;

    //Spawns
    private GameObject m_Clone;
    private Vector3 m_CurrentPosition = Vector3.zero;
    private int m_LastSpawnIndex = 0;
    private int m_PlatformID = 0;
    private List<int> m_TileIDs = new List<int>();
    private List<int> m_Lane1ID = new List<int>();
    private List<int> m_Lane2ID = new List<int>();
    private List<int> m_Lane3ID = new List<int>();
    private List<List<int>> m_Identifiers = new List<List<int>>();
    private List<int> m_LaneCounters = new List<int>();
    private bool m_OverridenIndex = false;
    private int m_PreviousRandom = -1;
    private bool m_HasRerolled = false;
    private int m_CurrentReroll = 0;
    private int m_CurrentHeight = 0;

    //Platform counters
    private int m_TotalTileNum = 1;
    private int m_CurTileNum = 1;
    private int m_OutPutNum = 0;

    //Floor & walls
    private int m_WallCounter = 0;

	void Start()
    {
        m_Identifiers.Add(m_Lane1ID);
        m_Identifiers.Add(m_Lane2ID);
        m_Identifiers.Add(m_Lane3ID);

        for (int i = 0; i < 3; i++)
        {
            m_LaneCounters.Add(0);
            m_TileIDs.Add(0);
        }

        //SpawnSingleTile();

        GenerateTiles(m_InitNumber);
        GenerateLevel(m_InitNumber);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GenerateTiles(5);
            GenerateLevel(5);
        }

        //Generate more platforms if current number is low
        if (m_CurTileNum <= 4)
        {
            GenerateTiles(5);
        }
	}

    void GenerateLevel(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject floor = (GameObject)Instantiate(m_LevelPiecePrefab, new Vector3(0, m_WallCounter * m_YIncrease, 0.0f), Quaternion.identity);

            m_WallCounter++;
        }
    }

    void GenerateTiles(int num)
    {
        //Select platform to generate - get its ID
        //Find a suitable lane to spawn it in, spawn it
        //Assign last ID to corresponding lane

        int temp = m_TotalTileNum + num;

        for (int i = m_TotalTileNum; i < temp; i++)
        {
            m_TotalTileNum++;
            m_CurTileNum++;

            SpawnSingleTile();
            //SpawnRow();
        }

        //OutputTiles();
    }

    void OutputTiles()
    {
        string output = "";
        for (int i = m_OutPutNum; i < m_Identifiers[0].Count; i++)
        {
            for (int j = 0; j < m_Identifiers.Count; j++)
            {
                output += m_Identifiers[j][i].ToString() + " ";
            }
            Debug.Log(output);
            output = "";
        }
        m_OutPutNum = m_Identifiers[0].Count;
        Debug.Log("End of generation");
    }

    public void RemoveLastPosition()
    {
        m_CurTileNum--;
    }

    void SpawnRow()
    {
        //Random how many per row
        //Spawn with restrictions
        for (int i = 0; i < 3; i++)
        {
            m_Identifiers[i].Add(0);
        }

        List<float> temp = new List<float>();
        for (int j = 0; j < m_Lanes.Count; j++)
        {
            if (m_Identifiers[j][m_Identifiers[j].Count - 2] != m_TileIDs[j] || m_Identifiers[j][m_Identifiers[j].Count - 2] == 0)
                temp.Add(m_Lanes[j]);
            //if (m_Identifiers[j].Count > 1)
            //{
            //}
            //else
            //    temp.Add(m_Lanes[j]);
        }

        for (int i = 0; i < temp.Count; i++)
        {
            int random = Random.Range(1, 3);
            for (int j = 0; j < random; j++)
            {
                int randomLane = Random.Range(0, temp.Count);
                int randomSpawn = Random.Range(0, m_TilePrefabs.Count);
                int id = m_TilePrefabs[randomSpawn].GetComponent<TileIdentifier>().m_ID;

                if (m_Identifiers[randomLane][m_Identifiers[randomLane].Count - 2] != id && m_Identifiers[randomLane][m_Identifiers[randomLane].Count - 1] == 0)
                {
                    m_Identifiers[randomLane][m_Identifiers[randomLane].Count - 1] = id;
                    m_TileIDs[randomLane] = id;
                    m_Clone = (GameObject)Instantiate(m_TilePrefabs[randomSpawn], new Vector3(m_Lanes[randomLane], m_CurrentHeight * m_YIncrease), Quaternion.identity);
                    temp.RemoveAt(randomLane);
                }
            }

            for (int j = 0; j < m_Lanes.Count; j++)
            {

            }

        }

        m_CurrentHeight++;
    }

    void SpawnSingleTile()
    {
        m_Clone = (GameObject)Instantiate(m_TilePrefabs[RollPrefab()], Vector3.zero, Quaternion.identity);

        if (m_Clone.GetComponent<TileIdentifier>())
            m_PlatformID = m_Clone.GetComponent<TileIdentifier>().m_ID;

        m_HasRerolled = false;
        m_CurrentReroll = 0;

        //Get lane
        List<float> temp = new List<float>();
        for (int i = 0; i < m_Lanes.Count; i++)
        {
            if (m_Identifiers[i].Count > 0)
            {
                if (m_Identifiers[i][m_Identifiers[i].Count - 1] != m_PlatformID)
                    temp.Add(m_Lanes[i]);
            }
            else
                temp.Add(m_Lanes[i]);
        }

        m_LastSpawnIndex = -1;

        int randomLane = Random.Range(0, temp.Count);

        m_OverridenIndex = false;
        for (int i = 0; i < m_LaneCounters.Count; i++)
        {
            if (m_LaneCounters[i] >= m_EmptyLaneCounter)
            {
                randomLane = i;
                m_OverridenIndex = true;
                break;
            }
        }

        float y = m_CurrentPosition.y + m_YIncrease;
        float x = 0;
        if (!m_OverridenIndex)
            x = temp[randomLane];
        else
            x = m_Lanes[randomLane];

        //m_Poslist.Add(new Vector3(x, y + m_YIncrease, 0.0f));
        m_CurrentPosition = new Vector3(x, y, -5f);
        m_Clone.transform.position = m_CurrentPosition;

        for (int i = 0; i < m_Lanes.Count; i++)
        {
            if (m_Lanes[i] == x)
            {
                m_LastSpawnIndex = i;
                m_LaneCounters[i] = 0;
                m_Identifiers[i].Add(m_PlatformID);
                m_TileIDs[i] = m_PlatformID;
            }
            else
            {
                m_LaneCounters[i]++;
                m_Identifiers[i].Add(0);
                m_TileIDs[i] = 0;
            }
        }
        m_CurrentHeight++;
    }

    int RollPrefab()
    {
        int random = Random.Range(0, m_TilePrefabs.Count);

        if (m_RerollSame)
        {
            if (random == m_PreviousRandom && !m_HasRerolled)
            {
                m_CurrentReroll++;
                //Debug.Log("Rerolled!" + " " + random + " " + m_PreviousRandom + " " + m_CurrentReroll);
                if (m_CurrentReroll >= m_RerollTimes)
                    m_HasRerolled = true;
                return RollPrefab();
            }
        }

        m_PreviousRandom = random;

        return random; 
    }
}
