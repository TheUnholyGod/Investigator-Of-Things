using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "NewDialogComponent", menuName = "Dialog/Dialog")]
public class DialogNode : INode<Dialog> {

    [SerializeField]
    public string m_dialogdata = "";



    List<DialogNode> childconverter = new List<DialogNode>();

    [SerializeField]
    DelegatePointer m_delegatePointer;
    [SerializeField]
    DialogNode[] m_allchildren;
    public DelegatePointer DelegatePointer
    {
        get
        {
            return m_delegatePointer;
        }

        set
        {
            m_delegatePointer = value;
        }
    }

    private void OnEnable()
    {
        if (m_data == null)
        {
            m_data = new Dialog();
            m_data.DialogText = m_dialogdata;
            m_data.DelegatePointer = m_delegatePointer;
        }
        if (m_children.Count == 0)
        {
            int count = 0;
            foreach (DialogNode i in m_allchildren)
            {
                m_children.Add(count, i);
                ++count;
            }
        }
    }

    public void AddChild(DialogNode _node)
    {
        childconverter.Add(_node);
    }

    public void ConvertAll()
    {
        m_data = CreateInstance<Dialog>();
        m_data.DialogText = m_dialogdata;
        m_data.DelegatePointer = m_delegatePointer;
        
        m_allchildren = childconverter.ToArray();
        int count = 0;
        foreach (DialogNode i in m_allchildren)
        {
            m_children.Add(count, i);
            ++count;
        }
    }

    public void Invoke()
    {
        m_data.Invoke();
    }

    public static DialogNode CreateInstance(DialogNode _node)
    {
        //DialogNode ret = CreateInstance<DialogNode>();
        //foreach(DialogNode dn in _node.m_allchildren)
        //{
        //    ret.AddChild(DialogNode.CreateInstance(dn));
        //}
        //ret.ConvertAll();
        //ret.m_data = Dialog.CreateInstance(_node.m_data);
        //return ret;
        return Instantiate(_node);
    }

}
