using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InventoryItem : InteractableObject,IPointerClickHandler, IDragHandler, IEndDragHandler{

    public string itemName;

    public bool isItem = true;

	// Use this for initialization
	void Start () {
        m_dialogtree.MoveDown(2);
        m_dialogtree.Current.DelegatePointer.Function.AddListener(this.PickUp);
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
        if (isItem)
            InventoryManager.GetInstance().AddItem(this);
    }

    public void SetDialogueManager()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Dialogue");
        DialogManager = temp.GetComponent<DialogManager>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        InteractionManager.GetInstance().dragged = this.gameObject;

        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        InteractionManager.GetInstance().CheckForFunction();
        
        transform.localPosition = Vector3.zero;
    }
}
