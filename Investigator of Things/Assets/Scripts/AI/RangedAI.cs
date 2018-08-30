using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAI : AI {

    public override void Roam()
    {
        base.Roam();
        if ((transform.position - m_target.transform.position).magnitude < 10)
            m_StateManager.Currstate = State.CHASE;
    }

    public override void Chase()
    {
        base.Chase();

        if ((transform.position - m_target.transform.position).magnitude > 20)// || (transform.position - m_waypointSystem.transform.position).magnitude < 10)
            m_StateManager.Currstate = State.ROAM;
    }

    public override void Attack()
    {
        base.Attack();
    }
}
