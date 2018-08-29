using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    [SerializeField]
    GameObject nextWaypoint;

    [SerializeField]
    WaypointSystem m_waypointSystem;

	// Update is called once per frame
	void Update () {
        transform.position += (- transform.position + nextWaypoint.transform.position).normalized * 10;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (nextWaypoint.GetComponent<Collider>() != other)
            return;

        nextWaypoint = m_waypointSystem.GetNextWaypoint(nextWaypoint);
    }
}
