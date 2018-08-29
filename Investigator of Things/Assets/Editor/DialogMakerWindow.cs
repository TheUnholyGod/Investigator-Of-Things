using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DialogMakerWindow : EditorWindow {

    class Node
    {
        public string data;
        public int childs;
        public bool dropdown;
        public bool toggle;
        public string key;
        public Node(string _key)
        {
            key = _key;
            data = "";
            childs = 0;
            dropdown = true;
            toggle = true;
        }
    }
    string Location = "";
    string Name = "";
    string rootKey = "0:0:0";



    Dictionary<string,Node> m_nodes = new Dictionary<string, Node>();

    delegate void Basicfunc();
   

   [MenuItem("Window/Dialog Window")]
    public static void ShowWindow()
    {
        DialogMakerWindow dmw = GetWindow<DialogMakerWindow>("Dialog Maker");
        dmw.Init();
    }

    private void Init()
    {
        m_nodes.Add("0:0:0", new Node("0:0:0"));
    }

    private void OnGUI()
    {
        VerticalField(() =>
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

            EditorGUILayout.Foldout(m_nodes[rootKey].dropdown, new GUIContent("Root"));
            if (m_nodes[rootKey].dropdown)
            {
                HorizontalField(() =>
                {
                    EditorGUILayout.LabelField("Data", GUILayout.MaxWidth(60));
                    m_nodes[rootKey].data = EditorGUILayout.TextArea(m_nodes[rootKey].data, GUILayout.MaxWidth(275));
                });
                HorizontalField(() =>
                {
                    EditorGUILayout.LabelField("Children", GUILayout.MaxWidth(60));
                    m_nodes[rootKey].childs = EditorGUILayout.IntField(m_nodes[rootKey].childs, GUILayout.MaxWidth(275));
                });
                for(int i = 0;i< m_nodes[rootKey].childs;++i)
                {
                    RecursiveGUI(i, 1, 0);
                }

            }

            if(GUILayout.Button("Generate"))
            {
                //Create Dialog Tree
                CreateTree();
            }
        });
    }

    void RecursiveGUI(int index,int depth,int parent)
    {
        string key = +depth + ":" + parent + ":" + index;
        if(!m_nodes.ContainsKey(key))
        {
            m_nodes.Add(key, new Node(key));
        }
        m_nodes[key].dropdown = EditorGUILayout.Foldout(m_nodes[key].dropdown, new GUIContent("Child " + key));

        HorizontalField(() =>
        {
            EditorGUILayout.LabelField("Data", GUILayout.MaxWidth(60));
            m_nodes[key].data = EditorGUILayout.TextArea(m_nodes[key].data, GUILayout.MaxWidth(275));
        });
        HorizontalField(() =>
        {
            EditorGUILayout.LabelField("Children", GUILayout.MaxWidth(60));
            m_nodes[key].childs = EditorGUILayout.IntField(m_nodes[key].childs, GUILayout.MaxWidth(275));
        });
        for (int i = 0; i < m_nodes[key].childs; ++i)
        {
            RecursiveGUI(i, ++depth, index);
        }
    }

    void VerticalField(Basicfunc _func)
    {
        EditorGUILayout.BeginVertical();
        _func();
        EditorGUILayout.EndVertical();
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
        AssetDatabase.CreateAsset(dt, "Assets/" + Location + Name + ".asset");
        AssetDatabase.SaveAssets();
        dt.m_root1 = NodeToDialogNode(m_nodes[rootKey]);
        foreach (KeyValuePair<string,Node> kvp in m_nodes)
        {

        }
    }

    DialogNode NodeToDialogNode(Node _node)
    {
        DialogNode dn = CreateInstance<DialogNode>();
        dn.m_dialogdata = _node.data;
        return dn;
    }
}
