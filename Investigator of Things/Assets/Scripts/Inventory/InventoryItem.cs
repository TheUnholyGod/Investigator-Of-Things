using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerDownHandler {

    public string itemName;
    public bool isUI = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Debug.isDebugBuild)
        {
            if (Input.GetKeyDown(KeyCode.Z) && !isUI)
                InventoryManager.GetInstance().AddItem(this);
        }
	}
    
    public void RemoveItem()
    {
        Destroy(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isUI)
            return;

    }
}
