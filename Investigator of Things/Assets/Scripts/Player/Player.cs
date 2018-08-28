using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player> {

    Vector3 target;
    Vector3 prevPos;
    CharacterController character;
    public float speed = 6.0f;

	// Use this for initialization
	void Start () {
        character = GetComponent<CharacterController>();
        target = transform.position;
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();

        Vector3 moveDirection = (target - transform.position);

        if (moveDirection.magnitude > 1)
        {
            moveDirection = transform.TransformDirection(moveDirection.normalized);
            moveDirection *= speed;
            prevPos = transform.position;
            character.Move(moveDirection);
        }

        if (transform.position.x < 0 || transform.position.x > 500 || transform.position.z < 0 || transform.position.z > 500)
            transform.position = prevPos;

        CameraManager.GetInstance().CheckIfInView(transform.position);
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
        this.target.y = 0;
    }
}
