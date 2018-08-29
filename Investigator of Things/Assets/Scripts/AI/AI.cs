using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    [SerializeField]
    GameObject nextWaypoint;

	// Update is called once per frame
	void Update () {
        transform.position += (transform.position - nextWaypoint.transform.position).normalized;
	}

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
