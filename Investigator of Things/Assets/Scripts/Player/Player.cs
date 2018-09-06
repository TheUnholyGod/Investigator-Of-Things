using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player> {

    Vector3 target;
    Vector3 prevPos;
    CharacterController character;
    public float speed = 1.0f;

    [SerializeField]
    Animator animator;

    [SerializeField]
    InteractionManager interactionManager;

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


        if (moveDirection.magnitude > 1 && interactionManager.Interaction == InteractionManager.Interactions.None)
        {
            transform.LookAt(target);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            moveDirection.Normalize();
            //moveDirection = transform.TransformDirection(moveDirection.normalized);
            moveDirection *= speed;
            prevPos = transform.position;
            character.Move(moveDirection);
            animator.SetBool("iswalking", true);
            if (character.velocity.magnitude < 1)
                animator.SetBool("iswalking", false);
        }
        

        CameraManager.GetInstance().CheckIfInView(gameObject);
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
        this.target.y = 0;
    }

    public void Inspect()
    {
        animator.SetBool("inspect", true);
    }

    public void Take()
    {
        animator.SetBool("take", true);
    }
}
