using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Ray target = CameraManager.GetInstance().mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(transform.position, target.direction, out hit);
            if (!hit.point.Equals(Vector3.zero))
                Player.GetInstance().SetTarget(hit.point);
        }

	}

}
