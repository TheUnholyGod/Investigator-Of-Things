using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAI : AI {

    [SerializeField]
    GameObject m_bullet;

    float cooldowntimer = 0;
    float cooldownreset = 2;

    public override void Roam()
    {
        base.Roam();
        if ((transform.position - m_target.transform.position).magnitude < 15)
            m_StateManager.Currstate = State.CHASE;
    }

    public override void Chase()
    {
        base.Chase();

        if ((transform.position - m_target.transform.position).magnitude > 15)// || (transform.position - m_waypointSystem.transform.position).magnitude < 10)
            m_StateManager.Currstate = State.ROAM;
        else if ((transform.position - m_target.transform.position).magnitude < 7.5)// || (transform.position - m_waypointSystem.transform.position).magnitude < 10)
            m_StateManager.Currstate = State.ATTACK;

    }

    public override void Attack()
    {
        base.Attack();

        cooldowntimer += Time.deltaTime;

        if (cooldowntimer > cooldownreset)
        {
            cooldowntimer = 0;

            Vector3 attackDir = (m_target.transform.position - transform.position).normalized;

            GameObject bullet = Instantiate(m_bullet, transform.position + (attackDir * 1.75f), Quaternion.identity);
            Bullet b = bullet.GetComponent<Bullet>();
            b.gameObject.tag = "Enemy";

            b.Direction = attackDir;
            b.Direction.Set(b.Direction.x, transform.position.y, b.Direction.z);
        }

        if ((transform.position - m_target.transform.position).magnitude > 7.5)
            m_StateManager.Currstate = State.CHASE;
    }
}
