using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSystem : MonoBehaviour {
    [SerializeField]
    GameObject[] m_waypoints;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject GetNextWaypoint(GameObject _curr)
    {
        GameObject ret = null;
        bool getnext = false;
        foreach(GameObject go in m_waypoints)
        {
            if (getnext)
            {
                ret = go;
                break;
            }
            if(go == _curr)
            {
                getnext = true;
            }
        }
        if (!ret)
            ret = m_waypoints[0];
        return ret;
    }
}
