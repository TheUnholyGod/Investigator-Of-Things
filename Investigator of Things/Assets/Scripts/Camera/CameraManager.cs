using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : Singleton<CameraManager>
{
    List<Camera> perspectiveCameraList;
    public Camera mainCamera;
    public Camera computerCamera;
    public bool cameraTransition;

    [SerializeField]
    Text CameraNo;

    // Use this for initialization
    void Start()
    {
        perspectiveCameraList = new List<Camera>(Camera.allCameras);

        cameraTransition = false;

        for (int i = 0; i < perspectiveCameraList.Count; ++i)
        {
            if (perspectiveCameraList[i].orthographic)
                computerCamera = perspectiveCameraList[i];
            else if (mainCamera == null)
                mainCamera = perspectiveCameraList[i];

            if (mainCamera != perspectiveCameraList[i])
                perspectiveCameraList[i].gameObject.SetActive(false);
        }

        perspectiveCameraList.Remove(computerCamera);
    }

    public void CheckIfInView(GameObject player)
    {
        if (computerCamera.gameObject.activeSelf)
        {
        CameraNo.text = computerCamera.gameObject.name.ToUpper();
            return;
        }

        RaycastHit hit;

        Vector3 direction = (-mainCamera.transform.position - player.transform.position).normalized;

        float distance = float.MaxValue;
        Camera closestCam = mainCamera;

        foreach (Camera cam in perspectiveCameraList)
        {
            direction = player.transform.position - cam.transform.position;

            Debug.DrawRay(cam.transform.position, direction, Color.red);

            Vector3 viewPos = cam.WorldToViewportPoint(player.transform.position);

            if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1 || viewPos.z < 0)
                continue;   

            if (Physics.Raycast(cam.transform.position, direction, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    //Debug.Log("Hit" + cam.gameObject.name);
                    if (hit.distance < distance)
                    {
                        distance = hit.distance;
                        if (closestCam != null)
                            closestCam.gameObject.SetActive(false);
                        closestCam = cam;
                    }
                }
            }
        }

        if (closestCam != mainCamera)
            cameraTransition = true;

        CameraNo.text = closestCam.gameObject.name.ToUpper();

        closestCam.gameObject.SetActive(true);
        mainCamera = closestCam;
    }
}
