using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomAreas/CustomArea")]
public class CustomArea : ScriptableObject {
    [Header("Assign squares per lane")]
    public List<GameObject> m_middleLane;
    public List<GameObject> m_LeftLane;
    public List<GameObject> m_rightLane;

    public GameObject InstantiateArea(Vector3 firstMidPos)
    {
        if ((m_middleLane.Count == m_rightLane.Count) && (m_middleLane.Count == m_LeftLane.Count))
        {
            GameObject resultingPrefab = new GameObject();
            resultingPrefab.name = this.name;
            resultingPrefab.transform.position = firstMidPos;

            for (int i = 0; i < m_middleLane.Count; i++)
            {
                if(m_LeftLane[i] != null)
                {
                    Vector3 leftSquarePos = new Vector3(-GameSettings.m_distanceBetweenSquares, firstMidPos.y + i * GameSettings.m_squareHeight, 0);
                    GameObject leftSquare = InstantiateSquare(m_LeftLane[i], leftSquarePos, resultingPrefab.transform);
                }

                if(m_middleLane[i] != null)
                {
                    Vector3 midSquarePos = new Vector3(0, firstMidPos.y + i * GameSettings.m_squareHeight, 0);
                    GameObject midSquare = InstantiateSquare(m_middleLane[i], midSquarePos, resultingPrefab.transform);
                }

                if(m_rightLane[i] != null)
                {
                    Vector3 rightSquarePos = new Vector3(GameSettings.m_distanceBetweenSquares, firstMidPos.y + i * GameSettings.m_squareHeight, 0);
                    GameObject rightSquare = InstantiateSquare(m_rightLane[i], rightSquarePos, resultingPrefab.transform);
                }
            }
            return resultingPrefab;
        }
        else
        {
            Debug.Log("Lanes are not completed");
            return null;
        }
    }
    private GameObject InstantiateSquare(GameObject square, Vector3 pos, Transform parent)
    {
        return Instantiate(square, pos, GameSettings.m_squareRotation, parent);
    }
}
