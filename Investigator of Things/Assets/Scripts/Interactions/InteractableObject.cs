using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {
    [SerializeField]
    protected DialogTree m_dialogtree;

    [SerializeField]
    protected DialogManager DialogManager;

    public DialogTree Dialogtree
    {
        get
        {
            return m_dialogtree;
        }

        set
        {
            m_dialogtree = value;
        }
    }

    protected void Awake()
    {
        if (m_dialogtree != null)
            m_dialogtree = DialogTree.CreateInstance(m_dialogtree);
    }

    protected void OnMouseDown()
    {
        InteractionManager.GetInstance().CheckForFunction();
        //DialogManager.DialogTree = m_dialogtree;
        //DialogManager.TriggerDialog();
    }

    public DialogManager GetDialogManager()
    {
        return DialogManager;
    }
}
