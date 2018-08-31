using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : InteractableGameObject {
    [SerializeField]
    GameObject nextWaypoint;

    [SerializeField]
    WaypointSystem m_waypointSystem;

    public GameObject NextWaypoint
    {
        get
        {
            return nextWaypoint;
        }

        set
        {
            nextWaypoint = value;
        }
    }

    public WaypointSystem WaypointSystem
    {
        get
        {
            return m_waypointSystem;
        }

        set
        {
            m_waypointSystem = value;
        }
    }

    public override void Idle()
    {
        base.Idle();
    }

    public override void AwaitInteract()
    {
        base.AwaitInteract();
    }

    public override void Interact()
    {
        base.Interact();
        if(Input.GetKey(KeyCode.E))
        {
            NextWaypoint = WaypointSystem.GetNextWaypoint(NextWaypoint);
            m_stateManager.Currstate = State.ACTIVATE;
        }
    }

    public override void Activate()
    {
        base.Activate();
        transform.position += (-transform.position + nextWaypoint.transform.position).normalized * 5 * Time.deltaTime;
        m_player.transform.position = transform.position;
        transform.LookAt(nextWaypoint.transform.position);
    }

    public override void IdleToAwaitInteraction()
    {
        base.IdleToAwaitInteraction();
    }

    public override void AwaitInteractionToIdle()
    {
        base.AwaitInteractionToIdle();

    }

    public override void AwaitInteractionToInteraction()
    {
        base.AwaitInteractionToInteraction();
        m_player.transform.position = transform.position;
        m_player.GetComponent<Dataminer>().intransport = true;
        m_player.GetComponent<Dataminer>().transporter = this;
    }

    public override void InteractionToAwaitInteraction()
    {
        base.InteractionToAwaitInteraction();
    }

    public override void InteractionToActivate()
    {
        base.InteractionToActivate();
        m_player.GetComponent<Dataminer>().istransportmoving = true;
    }

    public override void ActivateToInteraction()
    {
        base.ActivateToInteraction();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_stateManager.Currstate == State.ACTIVATE)
        {
            if (NextWaypoint.GetComponent<Collider>() != other)
                return;
            m_stateManager.Currstate = State.INTERACT;
            m_player.GetComponent<Dataminer>().istransportmoving = false;
        }
    }
}
