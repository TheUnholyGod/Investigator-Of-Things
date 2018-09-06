using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineArea : InteractableGameObject {

    [SerializeField]
    float cooldownreset = 10.0f;
    float cooldowntimer = 0.0f;
    public bool complete = false;

    public override void Idle()
    {
        base.Idle();
        Debug.Log(transform.position);
        Debug.Log(m_player.transform.position);

    }

    public override void AwaitInteract()
    {
        base.AwaitInteract();
    }

    public override void Interact()
    {
        base.Interact();
        if (Input.GetKey(KeyCode.E))
        {
            m_stateManager.Currstate = State.ACTIVATE;
        }
    }

    public override void Activate()
    {
        base.Activate();
        cooldowntimer += Time.deltaTime;
        if (cooldownreset < cooldowntimer)
        {
            complete = true;
            //Debug.Log("WIN");
        }
    }

    public override void IdleToAwaitInteraction()
    {
        base.IdleToAwaitInteraction();
    }

    public override void AwaitInteractionToIdle()
    {
        base.AwaitInteractionToIdle();

    }

    public override void AwaitInteractionToInteraction()
    {
        base.AwaitInteractionToInteraction();
    }

    public override void InteractionToAwaitInteraction()
    {
        base.InteractionToAwaitInteraction();
    }

    public override void InteractionToActivate()
    {
        base.InteractionToActivate();
    }

    public override void ActivateToInteraction()
    {
        base.ActivateToInteraction();
    }
}
