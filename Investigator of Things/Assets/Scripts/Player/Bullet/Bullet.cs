﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    float lifetime = 2.5f;
    float timer = 0;
    Vector3 m_direction;
    public Vector3 Direction
    {
        get
        {
            return m_direction;
        }

        set
        {
            m_direction = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
		if(timer < lifetime)
        {
            transform.position += Direction * 25 * Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }

        Destroy(this.gameObject);
    }
}
