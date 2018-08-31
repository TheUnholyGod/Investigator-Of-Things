using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : AI {

    float cooldowntimer = 0;
    float cooldownreset = 2;

    public override void Roam()
    {
        base.Roam();
        if ((transform.position - m_target.transform.position).magnitude < 10)
            m_StateManager.Currstate = State.CHASE;
    }

    public override void Chase()
    {
        base.Chase();

        Vector3 dir = (m_target.transform.position - transform.position);
        transform.forward = Vector3.Lerp(transform.forward, dir.normalized, 5 * Time.deltaTime);

        if (dir.magnitude > 20)// || (transform.position - m_waypointSystem.transform.position).magnitude < 10)
            m_StateManager.Currstate = State.ROAM;
        else if (dir.magnitude < 1.5)// || (transform.position - m_waypointSystem.transform.position).magnitude < 10)
            m_StateManager.Currstate = State.ATTACK;
    }

    public override void Attack()
    {
        Vector3 dir = (m_target.transform.position - transform.position);
        transform.forward = Vector3.Lerp(transform.forward, dir.normalized, 5 * Time.deltaTime);

        if (!transform.GetChild(0).GetComponent<Collider>().enabled)
        {
            cooldowntimer += Time.deltaTime;

            if (cooldowntimer > cooldownreset)
            {
                transform.GetChild(0).GetComponent<Collider>().enabled = true;
                cooldowntimer = 0;
            }
        }

        if (dir.magnitude > 1.5)// || (transform.position - m_waypointSystem.transform.position).magnitude < 10)
            m_StateManager.Currstate = State.CHASE;
    }
}
