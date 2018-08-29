using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DialogMakerWindow : EditorWindow {

    class Node
    {
        public string data;
        public int childs;
        public int prevchilds;
        public bool dropdown;
        public bool toggle;
        public string key;
        public string name;
        public int index;
        public Node parent;
        public List<Node> children = new List<Node>();
        public Node(string _key)
        {
            key = _key;
            data = "";
            parent = null;
            childs = 0;
            dropdown = true;
            toggle = true;
            index = 0;
        }
    }
    string Location = "Resources/Dialog/";
    string Name = "";
    string rootKey = "0:0:0";
    Vector2 Scrollpos = Vector2.zero;
    Node Root = null;
    DialogNode EndNode;

    delegate void Basicfunc();

   [MenuItem("Window/Dialog Window")]
    public static void ShowWindow()
    {
        DialogMakerWindow dmw = GetWindow<DialogMakerWindow>("Dialog Maker");
        dmw.Init();
    }

    private void Init()
    {
        Root = new Node(rootKey);
        if (EndNode == null)
        {
            foreach (string s in AssetDatabase.FindAssets("END"))
            {
                string pth = AssetDatabase.GUIDToAssetPath(s);
                EndNode = AssetDatabase.LoadAssetAtPath<DialogNode>(pth);
                if (EndNode != null)
                    break;
            }
        }
    }

    private void OnGUI()
    {
        ScrollField(() =>
        {
            //Name Field
            HorizontalField(() =>
            {
                EditorGUILayout.LabelField("Name", GUILayout.MaxWidth(60));
                Name = EditorGUILayout.TextArea(Name, GUILayout.MaxWidth(275));
            });

            //Location Field
            HorizontalField(() =>
            {
                EditorGUILayout.LabelField("File Path", GUILayout.MaxWidth(60));
                Location = EditorGUILayout.TextArea(Location, GUILayout.MaxWidth(275));
            });

            Root.dropdown = EditorGUILayout.Foldout(Root.dropdown, new GUIContent("Root"));
            if (Root.dropdown)
            {
                HorizontalField(() =>
                {
                    EditorGUILayout.LabelField("Name", GUILayout.MaxWidth(60));
                    Root.name = EditorGUILayout.TextArea(Root.name, GUILayout.MaxWidth(275));
                });
                HorizontalField(() =>
                {
                    EditorGUILayout.LabelField("Data", GUILayout.MaxWidth(60));
                    Root.data = EditorGUILayout.TextArea(Root.data, GUILayout.MaxWidth(275));
                });
                HorizontalField(() =>
                {
                    EditorGUILayout.LabelField("Children", GUILayout.MaxWidth(60));
                    Root.childs = EditorGUILayout.IntField(Root.childs, GUILayout.MaxWidth(275));
                    if (Root.childs < Root.prevchilds)
                    {
                        Root.children.RemoveRange(Root.childs, Root.prevchilds - Root.childs);
                    }
                });
                for(int i = 0;i< Root.childs;++i)
                {
                    RecursiveGUI(i, 1, Root);
                }
            }

            if(GUILayout.Button("Generate"))
            {
                //Create Dialog Tree
                CreateTree();
            }
        });
    }

    void RecursiveGUI(int index,int depth,Node parent)
    {
        string key = +depth + ":" + parent.index + ":" + index;
        if(null == parent.children.Find( i=> i.key == key))
        {
            Node newnode = new Node(key);
            newnode.index = index;
            newnode.parent = parent;
            parent.children.Add(newnode);
            //m_nodes.Add(key, newnode);
        }
        Node node = parent.children.Find(i => i.key == key);
        node.dropdown = EditorGUILayout.Foldout(node.dropdown, new GUIContent("Child " + key));
        if (node.dropdown)
        {
            HorizontalField(() =>
            {
                EditorGUILayout.LabelField("Name", GUILayout.MaxWidth(60));
                node.name = EditorGUILayout.TextArea(node.name, GUILayout.MaxWidth(275));
            });
            HorizontalField(() =>
            {
                EditorGUILayout.LabelField("Data", GUILayout.MaxWidth(60));
                node.data = EditorGUILayout.TextArea(node.data, GUILayout.MaxWidth(275));
            });
            HorizontalField(() =>
            {
                EditorGUILayout.LabelField("Children", GUILayout.MaxWidth(60));
                node.childs = EditorGUILayout.IntField(node.childs, GUILayout.MaxWidth(275));
                if (node.childs < node.prevchilds)
                {
                    node.children.RemoveRange(node.childs, node.prevchilds - node.childs);
                }
            });
            for (int i = 0; i < node.childs; ++i)
            {
                RecursiveGUI(i, depth + 1, node);
            }
        }
    }

    void VerticalField(Basicfunc _func)
    {
        EditorGUILayout.BeginVertical();
        _func();
        EditorGUILayout.EndVertical();
    }

    void ScrollField(Basicfunc _func)
    {
        Scrollpos = EditorGUILayout.BeginScrollView(Scrollpos);
        _func();
        EditorGUILayout.EndScrollView();
    }

    void HorizontalField(Basicfunc _func)
    {
        EditorGUILayout.BeginHorizontal();
        _func();
        EditorGUILayout.EndHorizontal();
    }

    void CreateTree()
    {
        DialogTree dt = DialogTree.CreateInstance(Name);
        Dictionary<string, DialogNode> keyValuePairs = new Dictionary<string, DialogNode>();
        AssetDatabase.CreateAsset(dt, "Assets/" + Location + Name + ".asset");
        AssetDatabase.SaveAssets();
        dt.m_root1 = RecursiveCreation(Root);
    }

    DialogNode NodeToDialogNode(Node _node)
    {
        DialogNode dn = CreateInstance<DialogNode>();
        dn.m_dialogdata = _node.data;
        return dn;
    }

    DialogNode RecursiveCreation(Node _node)
    {
        DialogNode dn = NodeToDialogNode(_node);
        AssetDatabase.CreateAsset(dn, "Assets/" + Location + _node.name + ".asset");
        AssetDatabase.SaveAssets();
        if (_node.children.Count == 0)
            dn.AddChild(EndNode);
        else
        {
            foreach (Node n in _node.children)
            {
                dn.AddChild(RecursiveCreation(n));
            }
        }
        dn.ConvertAll();
        return dn;
    }
}
