using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {
    [SerializeField]
    protected DialogTree m_dialogtree;

    [SerializeField]
    protected DialogManager DialogManager;

    protected void Awake()
    {
        if (m_dialogtree != null)
            m_dialogtree = DialogTree.CreateInstance(m_dialogtree);
    }

    protected void OnMouseDown()
    {
        DialogManager.DialogTree = m_dialogtree;
        DialogManager.TriggerDialog();
    }
}
