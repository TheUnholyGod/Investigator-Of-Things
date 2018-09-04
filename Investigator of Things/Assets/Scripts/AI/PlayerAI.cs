using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : AI {

    [SerializeField]
    Animator playeranimator;

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
}
