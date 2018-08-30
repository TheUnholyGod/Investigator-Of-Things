using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "NewDialogTree", menuName ="Dialog/DialogSequence")]
public class DialogTree : ITree<Dialog> {
    [SerializeField]
    public DialogNode m_root1;

    private void OnEnable()
    {
        base.m_root = m_current = m_root1;
    }

    public static DialogTree CreateInstance(string _name)
    {
        DialogTree ret = CreateInstance<DialogTree>();
        ret.name = _name;
        return ret;
    }

    public static DialogTree CreateInstance(DialogTree _tree)
    {
        //DialogTree ret = CreateInstance<DialogTree>();
        //ret.name = _tree.name;
        //ret.m_root1 = DialogNode.CreateInstance(_tree.m_root1);
        //return ret;
        return Instantiate(_tree);
    }
}
