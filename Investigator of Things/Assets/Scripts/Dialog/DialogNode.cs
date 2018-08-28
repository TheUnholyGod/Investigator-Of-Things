using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "NewDialogComponent", menuName = "Dialog/Dialog")]
public class DialogNode : INode<Dialog> {

    [SerializeField]
    string m_dialogdata = "";

    [SerializeField]
    DialogNode[] m_allchildren;

    private void OnEnable()
    {
        m_data = new Dialog();
        m_data.DialogText = m_dialogdata;

        int count = 0;
        foreach (DialogNode i in m_allchildren)
        {
            m_children.Add(count, i);
            ++count;
        }
    }

}
