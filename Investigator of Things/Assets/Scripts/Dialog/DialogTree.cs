using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "NewDialogTree", menuName ="Dialog/DialogSequence")]
public class DialogTree : ITree<Dialog> {
    [SerializeField]
    DialogNode m_root1;

    private void OnEnable()
    {
        base.m_root = m_current = m_root1;
    }
}
