using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dataminer : MonoBehaviour {
    [SerializeField]
    float m_health = 20;
    float playerspeed = 20.0f;
    bool cooldown = false;
    float cooldowntimer = 0;
    float cooldownreset = 0.15f;
    [SerializeField]
    GameObject m_camera;
    [SerializeField]
    GameObject m_bullet;
    [SerializeField]
    public Transporter transporter;

    public bool intransport = false;
    public bool istransportmoving = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateRotation();
        if(Input.GetMouseButton(0) && !cooldown)
        {
            Shoot();
            cooldown = true;
        }
        else if(cooldown && cooldowntimer > cooldownreset)
        {
            cooldown = false;
            cooldowntimer = 0;
        }
        else if (cooldown)
        {
            cooldowntimer += Time.deltaTime;
        }
        m_camera.transform.position = new Vector3(transform.position.x, m_camera.transform.position.y, transform.position.z);

    }

    public void Movement()
    {
        if (Input.GetKey(KeyCode.A))
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - playerspeed * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
        else if (Input.GetKey(KeyCode.D))
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + playerspeed * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
        if (Input.GetKey(KeyCode.W))
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + playerspeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.S))
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - playerspeed * Time.deltaTime);
    }

    public void Shoot()
    {
        Vector3 Direction = (-transform.position + Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(m_camera.transform.position.y - transform.position.y)))).normalized;
        Direction.Set(Direction.x, 0, Direction.z);
        GameObject newb = Instantiate(m_bullet, transform.position + (Direction * 1.75f), Quaternion.identity);
        Bullet b = newb.GetComponent<Bullet>();

        b.Direction = Direction;
        b.Direction.Set(b.Direction.x, transform.position.y, b.Direction.z);
        b.transform.rotation = transform.rotation;
    }

    public void UpdateRotation()
    {
        Vector3 worldpos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(m_camera.transform.position.y - transform.position.y)));
        transform.LookAt(new Vector3(worldpos.x, transform.position.y, worldpos.z));
        if(!intransport)
            Movement();
        else if(intransport && !istransportmoving)
        {
            if(Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.D))
            {
                transporter.m_stateManager.Currstate = InteractableGameObject.State.AWAIT_INTERACT;
                intransport = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<Damage>() == null)
                return;
            TakeDamage(other.gameObject.GetComponent<Damage>().DamageVal);
            other.enabled = false;
        }
    }

    public void TakeDamage(int damage)
    {
        this.m_health -= damage;
    }
}
