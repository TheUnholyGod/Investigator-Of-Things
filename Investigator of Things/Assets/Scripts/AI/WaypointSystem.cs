using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSystem : MonoBehaviour {

    [SerializeField]
    bool loop = true;

    [SerializeField]
    GameObject[] m_waypoints;

    [SerializeField]
    GameObject m_spawn;
    float cooldowntimer = 0;
    float cooldownreset = 5;

    [SerializeField]
    int m_spawnno;

    [SerializeField]
    int m_maxspawnno;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (m_spawnno >= m_maxspawnno)
            return;
        cooldowntimer += Time.deltaTime;
        if(cooldowntimer > cooldownreset)
        {
            GameObject go = Instantiate(m_spawn, transform.position, Quaternion.identity);
            
            AI ai = go.GetComponent<AI>();
            if (ai != null)
            {
                ai.NextWaypoint = m_waypoints[0];
                ai.WaypointSystem = this;
            }
            else
            {
                Transporter t = go.GetComponent<Transporter>();
                t.NextWaypoint = m_waypoints[0];
                t.WaypointSystem = this;
            }
            m_spawnno++;
            cooldowntimer = 0;
        }
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
        if (!ret && !loop)
            ret = m_waypoints[0];
        return ret;
    }
}
