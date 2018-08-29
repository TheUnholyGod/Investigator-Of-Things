using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "NewDialogComponent", menuName = "Dialog/Dialog")]
public class DialogNode : INode<Dialog> {

    [SerializeField]
    public string m_dialogdata = "";

    [SerializeField]
    DialogNode[] m_allchildren;

    List<DialogNode> childconverter = new List<DialogNode>();

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

    public void AddChild(DialogNode _node)
    {
        childconverter.Add(_node);
    }

    public void ConvertAll()
    {
        m_allchildren = childconverter.ToArray();
    }

}
