using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager> {

    List<Camera> cameraList;
    public Camera mainCamera;

    Dictionary<Camera, Bounds> grid = new Dictionary<Camera, Bounds>();

	// Use this for initialization
	void Start () {
        cameraList = new List<Camera>(Camera.allCameras);

        mainCamera = Camera.main;

        for (int i =0; i < cameraList.Count; ++i)
        {
            if (mainCamera != cameraList[i])
                cameraList[i].gameObject.SetActive(false);

            Bounds bounds = new Bounds();
            Vector3 dir = cameraList[i].transform.forward;
            dir.y = 0;

            if (dir.x < 0)
                dir.x = -0.5f;
            else
                dir.x = 0.5f;

            if (dir.z < 0)
                dir.z = -0.5f;
            else
                dir.z = 0.5f;

            bounds.size = new Vector3(250f, 100f, 250f);
            bounds.center = cameraList[i].transform.position + (dir * bounds.size.x);

            grid.Add(cameraList[i], bounds);
        }

    }

    public void CheckIfInView(Vector3 pos)
    {
        if (!grid[mainCamera].Contains(pos))
        {
            mainCamera.gameObject.SetActive(false);
            foreach (Camera camera in cameraList)
            {
                if (camera == mainCamera)
                    continue;

                if (grid[camera].Contains(pos))
                {
                    mainCamera = camera;
                    mainCamera.gameObject.SetActive(true);
                    break;
                }
            }
        }
    }

}
