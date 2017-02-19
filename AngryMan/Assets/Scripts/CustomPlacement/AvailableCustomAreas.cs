using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomAreas/AvailableAreas")]
public class AvailableCustomAreas : ScriptableObject {
    [Header("All the presets that will be used")]
    public List<CustomArea> m_customAreas;
    private int m_selectedIndex;

    // Returns the height in tiles for a random area in the list
    public int RandomizeCustomArea(){
        m_selectedIndex = Random.Range(0, m_customAreas.Count - 1);
        return m_customAreas[m_selectedIndex].m_middleLane.Count;
    }

    public GameObject CreateRandomCustomArea(Vector3 atThisPos)
    {
        return m_customAreas[m_selectedIndex].InstantiateArea(atThisPos);
    }
}
