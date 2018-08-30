using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : Singleton<CameraManager>
{
    List<Camera> cameraList;
    public Camera mainCamera;
    public bool cameraTransition;

    [SerializeField]
    Text CameraNo;

    // Use this for initialization
    void Start()
    {
        cameraList = new List<Camera>(Camera.allCameras);

        cameraTransition = false;

        mainCamera = Camera.main;

        for (int i = 0; i < cameraList.Count; ++i)
        {
            if (mainCamera != cameraList[i])
                cameraList[i].gameObject.SetActive(false);
        }

    }

    public void CheckIfInView(GameObject player)
    {
        RaycastHit hit;

        Vector3 direction = (-mainCamera.transform.position - player.transform.position).normalized;

        float distance = float.MaxValue;
        Camera closestCam = mainCamera;

        foreach (Camera cam in cameraList)
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
