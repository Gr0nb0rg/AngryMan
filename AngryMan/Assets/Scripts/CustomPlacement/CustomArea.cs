using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomArea : ScriptableObject {
    [Header("Assign squares per lane")]
    public List<GameObject> m_middleLane;
    public List<GameObject> m_LeftLane;
    public List<GameObject> m_rightLane;

    public void InstantiateArea()
    {
        GameObject ResultingPrefab = new GameObject();
        if ((m_middleLane.Count == m_rightLane.Count) && (m_middleLane.Count == m_LeftLane.Count))
        {
            // Once per lane
            for (int i = 0; i < 3; i++)
            {
                // Once per square
                for (int j = 0; j < m_middleLane.Count; j++)
                {

                }
            }
        }
        else
            Debug.Log("Lanes are not completed");
    }
    private void InstantiateSquare()
    {

    }
}
