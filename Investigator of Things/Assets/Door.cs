using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [SerializeField]
    GameObject door;
    bool move = false;
    float prevpos;
    float movepos = 1.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if(move)
        {
            door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y, door.transform.position.z - 5*Time.deltaTime);
            if (Mathf.Abs(door.transform.position.z - prevpos) >= movepos)
                move = false;
        }
	}

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            move = true;
            prevpos = door.transform.position.z;
        }
    }
}
