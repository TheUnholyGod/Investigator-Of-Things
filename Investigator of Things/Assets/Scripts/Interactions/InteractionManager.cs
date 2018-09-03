using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionManager : Singleton<InteractionManager> {

    public GameObject dragged;

    [SerializeField]
    public custom_cursor custom_Cursor;

    Dictionary<string, UnityEvent> eventslib = new Dictionary<string, UnityEvent>();

	// Use this for initialization
	void Start () {
        UnityEvent e = new UnityEvent();
        e.AddListener(() => { Debug.Log("Interact"); });
        eventslib.Add("Cube", e);		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckForFunction()
    {
        GameObject raycasted = custom_Cursor.GetRayCastObject();
        if (raycasted != null)
        {
            eventslib["Cube"].Invoke();
        }
        dragged = null;
    }
}
