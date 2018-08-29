using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InventoryItem : InteractableObject,IPointerClickHandler{

    public string itemName;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Debug.isDebugBuild)
        {
            if (Input.GetKeyDown(KeyCode.Z))
                InventoryManager.GetInstance().AddItem(this);
        }
	}
    
    public void Copy(InventoryItem item)
    {
        itemName = item.itemName;
        m_dialogtree = item.m_dialogtree;
        DialogManager = item.DialogManager;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnMouseDown();
    }

    public void PickUp()
    {
        InventoryManager.GetInstance().AddItem(this);
    }
}
