using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomAreas/AvailableAreas")]
public class AvailableCustomAreas : ScriptableObject {
    public List<CustomArea> m_customAreas;

    int CreateRandomCustomArea(Vector3 atThisPos)
    {
        int area = Random.Range(0, m_customAreas.Count - 1);
        return m_customAreas[area].InstantiateArea(atThisPos);
    }
}
