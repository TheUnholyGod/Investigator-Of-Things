using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberAI : AI {

    [SerializeField]
    GameObject m_explosion;

    float cooldowntimer = 0;
    float cooldownreset = 0.5f;

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
        else if ((transform.position - m_target.transform.position).magnitude < 3)// || (transform.position - m_waypointSystem.transform.position).magnitude < 10)
            m_StateManager.Currstate = State.ATTACK;
    }

    public override void Attack()
    {
        base.Attack();

        cooldowntimer += Time.deltaTime;

        if (cooldowntimer > cooldownreset)
        {
            GameObject bullet = Instantiate(m_explosion, transform.position, Quaternion.identity);
            bullet.gameObject.tag = "Enemy";
            Destroy(this.gameObject);
        }
    }
}
