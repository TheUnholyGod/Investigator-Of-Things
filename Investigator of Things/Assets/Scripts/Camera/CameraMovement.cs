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
            Camera cam = CameraManager.GetInstance().mainCamera;
            Vector3 viewportPoint  = cam.ScreenToViewportPoint(Input.mousePosition);
            if (viewportPoint.x < 0 || viewportPoint.y < 0 || viewportPoint.y > 1)
                return;

            Ray target = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(cam.transform.position, target.direction, out hit);
            if (!hit.point.Equals(Vector3.zero))
                Player.GetInstance().SetTarget(hit.point);
        }
    }

}
