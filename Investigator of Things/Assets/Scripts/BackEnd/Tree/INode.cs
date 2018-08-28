using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class INode<Type> : ScriptableObject
{
    protected int key;

    protected Type m_data;

    protected INode<Type> m_parent;

    protected Dictionary<int,INode<Type>> m_children = new Dictionary<int, INode<Type>>();

    protected bool m_valid = false;

    public INode(INode<Type> _parent = null)
    {
        m_parent = _parent;
        m_data = default(Type);
        m_valid = false;
    }

    public INode(Type _data, INode<Type> _parent = null)
    {
        m_parent = _parent;
        m_data = _data;
        m_valid = true;
    }

    public bool IsValid
    {
        get
        {
            return m_valid;
        }

        set
        {
            m_valid = value;
        }
    }

    public Type Data
    {
        get
        {
            return m_data;
        }

        set
        {
            m_data = value;
        }
    }

    protected virtual void Awake()
    {

    }

    public INode<Type> GetChild(int _key)
    {
        if (!m_children.ContainsKey(_key))
            return null;
        return m_children[_key];
    }

    public INode<Type> GetParent()
    {
        return m_parent;
    }

    public int SetChild(INode<Type> _child)
    {
        if (m_children.ContainsValue(_child))
            return _child.key;

        m_children.Add(m_children.Count + 1, _child);

        return m_children.Count;
    }

    public int GetKey()
    {
        return key;
    }

    public int GetChildNo()
    {
        return m_children.Count;
    }

    public int[] GetChilds()
    {
        return m_children.Keys.ToArray();
    }
}
