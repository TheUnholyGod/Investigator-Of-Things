using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {
    [SerializeField]
    DialogTree m_dialogtree;

    [SerializeField]
    DialogManager DialogManager;

    private void OnMouseDown()
    {
        DialogManager.DialogTree = m_dialogtree;
        DialogManager.TriggerDialog();
    }
}
