using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGameObject : MonoBehaviour {

    public enum State
    {
        IDLE,
        AWAIT_INTERACT,
        INTERACT,
        ACTIVATE,
    }

    [SerializeField]
    protected GameObject m_player;

    [SerializeField]
    Material defaultmat;

    [SerializeField]
    Material activatedmat;

    public StateManager<State> m_stateManager;

    void Awake()
    {
        m_stateManager = new StateManager<State>();
        m_stateManager.Currstate = State.IDLE;
        m_stateManager.CurrstateReqUpdate = true;
        m_stateManager.AddUpdateFunction(State.IDLE, Idle);
        m_stateManager.AddUpdateFunction(State.AWAIT_INTERACT, AwaitInteract);
        m_stateManager.AddUpdateFunction(State.INTERACT, Interact);
        m_stateManager.AddUpdateFunction(State.ACTIVATE, Activate);
        m_stateManager.AddFunction(new StateDoubleKey<State>(State.IDLE, State.AWAIT_INTERACT).GetHashCode(), IdleToAwaitInteraction);
        m_stateManager.AddFunction(new StateDoubleKey<State>(State.AWAIT_INTERACT, State.IDLE).GetHashCode(), AwaitInteractionToIdle);
        m_stateManager.AddFunction(new StateDoubleKey<State>(State.AWAIT_INTERACT, State.INTERACT).GetHashCode(), AwaitInteractionToInteraction);
        m_stateManager.AddFunction(new StateDoubleKey<State>(State.INTERACT, State.AWAIT_INTERACT).GetHashCode(), InteractionToAwaitInteraction);
        m_stateManager.AddFunction(new StateDoubleKey<State>(State.INTERACT, State.ACTIVATE).GetHashCode(), InteractionToActivate);
        m_stateManager.AddFunction(new StateDoubleKey<State>(State.ACTIVATE, State.INTERACT).GetHashCode(), ActivateToInteraction);

        m_player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<MeshRenderer>().material = defaultmat;


    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_stateManager.Update();
	}

    public virtual void Idle()
    {
        if ((m_player.transform.position - transform.position).magnitude <= 5)
            m_stateManager.Currstate = State.AWAIT_INTERACT;
    }

    public virtual void AwaitInteract()
    {
        if ((m_player.transform.position - transform.position).magnitude > 5)
            m_stateManager.Currstate = State.IDLE;

        if (Input.GetKeyDown(KeyCode.E))
            m_stateManager.Currstate = State.INTERACT;
    }

    public virtual void Interact()
    {

    }

    public virtual void Activate()
    {

    }

    public virtual void IdleToAwaitInteraction()
    {

    }

    public virtual void AwaitInteractionToIdle()
    {

    }

    public virtual void AwaitInteractionToInteraction()
    {

    }

    public virtual void InteractionToAwaitInteraction()
    {

    }

    public virtual void InteractionToActivate()
    {
        GetComponent<MeshRenderer>().material = activatedmat;

    }

    public virtual void ActivateToInteraction()
    {
        GetComponent<MeshRenderer>().material = defaultmat;
    }
}
