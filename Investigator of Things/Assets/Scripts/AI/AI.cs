﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {


    protected enum State
    {
        ROAM,
        CHASE,
        ATTACK,
    }

    [SerializeField]
    float speed = 5;

    protected GameObject m_target;

    protected StateManager<State> m_StateManager;

    [SerializeField]
    protected GameObject nextWaypoint;

    [SerializeField]
    protected WaypointSystem m_waypointSystem;

    public  WaypointSystem WaypointSystem
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

    protected void Awake()
    {
        m_target = GameObject.FindGameObjectWithTag("Player");
        m_StateManager = new StateManager<State>();
        m_StateManager.AddUpdateFunction(State.ROAM, Roam);
        m_StateManager.AddUpdateFunction(State.CHASE, Chase);
        m_StateManager.AddUpdateFunction(State.ATTACK, Attack);
        m_StateManager.Currstate = State.ROAM;
        m_StateManager.CurrstateReqUpdate = true;
    }

    // Update is called once per frame
    protected void Update () {
        m_StateManager.Update();
	}

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (m_StateManager.Currstate == State.ROAM)
        {
            if (!nextWaypoint || nextWaypoint.GetComponent<Collider>() != other)
                return;

            nextWaypoint = m_waypointSystem.GetNextWaypoint(nextWaypoint);

        }
    }

    public virtual void Roam()
    {
        transform.position += (-transform.position + nextWaypoint.transform.position).normalized * speed * Time.deltaTime;
        transform.LookAt(nextWaypoint.transform.position);
    }

    public virtual void Chase()
    {
        transform.position += (-transform.position + m_target.transform.position).normalized * speed * Time.deltaTime;
        transform.LookAt(m_target.transform.position);

    }

    public virtual void Attack()
    {

    }
}
