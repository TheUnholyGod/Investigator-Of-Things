using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : InteractableGameObject {
    public override void Idle()
    {
        base.Idle();
    }

    public virtual void AwaitInteract()
    {
        base.AwaitInteract();
    }

    public virtual void Interact()
    {
        base.Interact();
    }

    public virtual void Activate()
    {
        base.Activate();
    }
}
