using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITree<Type> : ScriptableObject{

    [SerializeField]
    protected INode<Type> m_root;

    protected INode<Type> m_current;

    public Type Current
    {
        get
        {
            return m_current.Data;
        }

        set
        {
            m_current.Data = value;
        }
    }

    protected virtual void Awake()
    {

    }
        public ITree()
    {
        m_current = m_root = null;
    }

    public ITree(Type _rootdata)
    {
        m_current = m_root = new INode<Type>(_rootdata);
    }

    public bool MoveUp()
    {
        INode<Type> _next = m_current.GetParent();
        if (null == _next)
            return false;
        m_current = _next;
        return true;
    }

    public bool MoveDown(int _key)
    {
        INode<Type> _next = m_current.GetChild(_key);
        if (null == _next)
            return false;
        m_current = _next;
        return true;
    }

    public int Add(Type _data)
    {
        if(m_root == null)
        {
            m_root = new INode<Type>(_data);
            m_current = m_root;
            return 0;

        }
        INode<Type> node = new INode<Type>(_data);
        m_current.SetChild(node);
        return node.GetKey();
    }

    public int[] GetPaths()
    {
        return m_current.GetChilds();
    }

    public bool MoveToRoot()
    {
        m_current = m_root;
        return true;
    }
}
