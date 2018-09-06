using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroombaAI : AI {

    public GameObject cleaningWaypoint;

    public bool isCleaning = false;

    public bool isImmobilized = false;

    [SerializeField]
    private GameObject startingPoint;

    Vector3 defaultPos;
    Quaternion defaultRotation;

    InteractableObject interactable;

    private void Start()
    {
        cleaningWaypoint.SetActive(false);
        defaultPos = transform.position;
        defaultRotation = transform.rotation;
        interactable = this.GetComponent<InteractableObject>();
        interactable.Dialogtree.MoveDown(4);

        this.enabled = false;

        this.transform.position = defaultPos;
        this.transform.rotation = defaultRotation;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (nextWaypoint.Equals(startingPoint))
        {
            this.enabled = false;
            transform.position = defaultPos;
            transform.rotation = defaultRotation;
        }

        base.OnTriggerEnter(other);

        if (!nextWaypoint.activeSelf)
            nextWaypoint = m_waypointSystem.GetNextWaypoint(nextWaypoint);

        if (other.gameObject.Equals(cleaningWaypoint))
        {
            this.enabled = false;
            nextWaypoint = null;
            isCleaning = true;
        }
    }
}
