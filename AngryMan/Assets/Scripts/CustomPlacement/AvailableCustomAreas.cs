using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomAreas/AvailableAreas")]
public class AvailableCustomAreas : ScriptableObject {
    public List<CustomArea> m_customAreas;

    GameObject GetRandomCustomArea(Vector3 atThisPos)
    {
        int area = Random.Range(0, m_customAreas.Count - 1);
        return m_customAreas[area].InstantiateArea(atThisPos);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
