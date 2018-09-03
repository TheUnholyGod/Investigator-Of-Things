using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionManager : Singleton<InteractionManager> {

    [SerializeField]
    protected DialogManager DialogManager;

    public enum Interactions
    {
        Look,
        Inspect,
        Pickup,
        Use,
        ViewLogs,
        None,
    }

    private Interactions interaction;

    public GameObject dragged;

    [SerializeField]
    public custom_cursor custom_Cursor;

    Dictionary<string, UnityEvent> eventslib = new Dictionary<string, UnityEvent>();

    public Interactions Interaction
    {
        get
        {
            return interaction;
        }

        set
        {
            interaction = value;
        }
    }

    // Use this for initialization
    void Start () {
        UnityEvent e = new UnityEvent();
        e.AddListener(() => { Debug.Log("Interact"); });
        eventslib.Add("Cube", e);		
	}
	
	// Update is called once per frame
	void Update () {
		if(interaction!= Interactions.None)
        {
            if(Input.GetMouseButton(1))
            {
                interaction = Interactions.None;
                custom_Cursor.SetCursorTexture(null);
            }
        }
	}

    public void CheckForFunction()
    {
        if (interaction != Interactions.None)
        {
            GameObject raycasted = custom_Cursor.GetRayCastObject();
            if (raycasted.GetComponent<InteractableObject>() != null)
            {
                DialogManager.DialogTree = raycasted.GetComponent<InteractableObject>().Dialogtree;
                DialogManager.TriggerDialog(new int[] { (int)(interaction) });
            }
        }
        else
        {
            GameObject raycasted = custom_Cursor.GetRayCastObject();
            if (raycasted != null)
            {
                eventslib["Cube"].Invoke();
            }
            dragged = null;
        }
    }

    public void HandleInteractions()
    {

    }

    public void SetInteractions(Interactions _interaction)
    {
        interaction = _interaction;
    }

    public void SetLook()
    {
        SetInteractions(Interactions.Look);
    }

    public void SetInspect()
    {
        SetInteractions(Interactions.Inspect);
    }

    public void SetPickUp()
    {
        SetInteractions(Interactions.Pickup);
    }

    public void SetUse()
    {
        SetInteractions(Interactions.Use);
    }

    public void SetLogs()
    {
        SetInteractions(Interactions.ViewLogs);
    }
}
