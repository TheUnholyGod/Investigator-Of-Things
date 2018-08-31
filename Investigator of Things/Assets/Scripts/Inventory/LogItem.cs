using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LogItem : InteractableObject, IPointerClickHandler {

    public string logName;
    public bool isItem = true;

    // Use this for initialization
    void Start()
    {
        m_dialogtree.MoveDown(1);   
        m_dialogtree.Current.DelegatePointer.Function.AddListener(this.PickUp);
    }

    // Update is called once per frame
    void Update()
    {
        if (Debug.isDebugBuild)
        {
            if (Input.GetKeyDown(KeyCode.C))
                LogManager.GetInstance().AddItem(this);
        }
    }

    public void Copy(LogItem item)
    {
        logName = item.logName;
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
            LogManager.GetInstance().AddItem(this);
    }

    public void SetDialogueManager()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Dialogue");
        DialogManager = temp.GetComponent<DialogManager>();
    }
}
