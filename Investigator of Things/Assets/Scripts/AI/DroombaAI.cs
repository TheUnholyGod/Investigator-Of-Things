using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroombaAI : AI {

    public GameObject cleaningWaypoint;

    public bool isCleaning = false;

    public bool isImmobilized = false;
    private bool isStarted = false;

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
        isStarted = true;
    }

    private void OnEnable()
    {
        if (isStarted)
            AudioManager.GetInstance().PlayAudio(gameObject.name);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (nextWaypoint.Equals(startingPoint))
        {
            AudioManager.GetInstance().StopAudio(gameObject.name);
            this.enabled = false;
            transform.position = defaultPos;
            transform.rotation = defaultRotation;
        }

        base.OnTriggerEnter(other);

        if (!nextWaypoint.activeSelf)
            nextWaypoint = m_waypointSystem.GetNextWaypoint(nextWaypoint);

        if (other.gameObject.Equals(cleaningWaypoint))
        {
            AudioManager.GetInstance().StopAudio(gameObject.name);
            this.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sounds/DROOMBA DED");
            this.GetComponent<AudioSource>().loop = false;
            this.enabled = false;
            nextWaypoint = null;
            isCleaning = true;
        }
    }
}
