using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : AI {

    [SerializeField]
    Animator playeranimator;

    public Animator Playeranimator
    {
        get
        {
            return playeranimator;
        }

        set
        {
            playeranimator = value;
        }
    }

    // Use this for initialization
    void Start () {
            playeranimator.SetBool("iswalking", true);

    }

    // Update is called once per frame
    void Update () {
        base.Update();
        CameraManager.GetInstance().CheckIfInView(gameObject);
        if (GetComponent<CharacterController>().velocity != Vector3.zero)
        {
            playeranimator.SetBool("iswalking", true);
        }
        else
            ;// playeranimator.SetBool("iswalking", false);

    }

    public override void Roam()
    {
        base.Roam();
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        playeranimator.SetBool("iswalking", true);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (nextWaypoint == null)
        {
            playeranimator.SetBool("iswalking", false);
            GetComponent<Player>().enabled = true;
            enabled = false;
        }

    }
}
