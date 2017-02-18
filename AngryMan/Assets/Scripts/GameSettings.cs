using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject {
    public static float m_distanceBetweenSquares = 10;

    [Header("Local Spawn positions for props in a square")]
    [HideInInspector]
    public static Vector3 m_spawnPropMiddlePos = Vector3.zero;
    [HideInInspector]
    public static Vector3 m_spawnPropLeftPos = new Vector3(-(m_distanceBetweenSquares/2), 0, 0);
    [HideInInspector]
    public static Vector3 m_spawnPropRightPos = new Vector3((m_distanceBetweenSquares / 2), 0, 0);
    [HideInInspector]
    public static Vector3 m_spawnPropFrontPos = new Vector3(0, 0, (m_distanceBetweenSquares / 2));

    public static Quaternion m_spawnPropMiddleRot = Quaternion.Euler(-90, 0,0);
    public static Quaternion m_spawnPropLeftRot = Quaternion.Euler(-90, 0, 90);
    public static Quaternion m_spawnPropRightRot = Quaternion.Euler(-90, 0, 90);
    public static Quaternion m_spawnPropFrontRot = Quaternion.Euler(-90, 0, 0);

    private const float m_default_distanceBetweenSquares = 10;

    //private static GameSettings m_instance;
    //public static GameSettings Instance{
    //    get
    //    {
    //        m_instance = null;
    //        Debug.Log("GETTING"); Debug.Log(FindObjectOfType<GameSettings>().name);
    //        if (!m_instance)
    //            m_instance = FindObjectOfType<GameSettings>();
    //        if (!m_instance)
    //            m_instance = CreateDefaultSettings();
    //        return m_instance;
    //    }
    //}

    //private static GameSettings CreateDefaultSettings()
    //{
    //    Debug.Log("CREATED");
    //    GameSettings retVal = new GameSettings();
    //    retVal.m_distanceBetweenSquares = m_default_distanceBetweenSquares;

    //    retVal.m_spawnPropMiddlePos = Vector3.zero;
    //    retVal.m_spawnPropFrontPos = new Vector3(0, 0, m_default_distanceBetweenSquares/2);
    //    retVal.m_spawnPropRightPos = new Vector3(-m_default_distanceBetweenSquares / 2, 0, 0);
    //    retVal.m_spawnPropLeftPos = new Vector3(m_default_distanceBetweenSquares / 2, 0, 0);
    //    return retVal;
    //} 
}
