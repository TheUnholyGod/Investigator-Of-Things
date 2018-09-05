using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endmanager : MonoBehaviour {

    [SerializeField]
    DialogTree dialogTree;

    [SerializeField]
    DialogManager dialogManager;

    [SerializeField]
    TalkingAnimaton boss;

    [SerializeField]
    TalkingAnimaton player;

    [SerializeField]
    GameObject Enable;
    [SerializeField]
    GameObject Disable;

    bool triggered = false;

    // Use this for initialization
    void Start () {

        UnityEngine.Events.UnityEvent ue = new UnityEngine.Events.UnityEvent();
        ue.AddListener(() => 
        {
            Debug.Log(1);
            boss.talking = false;
            player.talking = true;
        });
        UnityEngine.Events.UnityEvent ue2 = new UnityEngine.Events.UnityEvent();
        ue2.AddListener(() => 
        {
            Debug.Log(2);
            boss.talking = true;
            player.talking = false;
        });

        for (int i = 0; i < 2; ++i)
        {
            dialogTree.Current.DelegatePointer = DelegatePointer.CreateInstance<DelegatePointer>();
            dialogTree.Current.DelegatePointer.Function = ue;
            dialogTree.MoveDown(0);
            dialogTree.Current.DelegatePointer = DelegatePointer.CreateInstance<DelegatePointer>();
            dialogTree.Current.DelegatePointer.Function = ue2;
            dialogTree.MoveDown(0);
        }
        dialogTree.MoveDown(0);
        for (int i = 0; i < 2; ++i)
        {
            dialogTree.Current.DelegatePointer = DelegatePointer.CreateInstance<DelegatePointer>();
            dialogTree.Current.DelegatePointer.Function = ue;
            dialogTree.MoveDown(0);
            dialogTree.Current.DelegatePointer = DelegatePointer.CreateInstance<DelegatePointer>();
            dialogTree.Current.DelegatePointer.Function = ue2;
            dialogTree.MoveDown(0);
        }
        dialogTree.Current.DelegatePointer = DelegatePointer.CreateInstance<DelegatePointer>();
        dialogTree.Current.DelegatePointer.Function = ue;
        dialogTree.MoveDown(0);
        //dialogTree.MoveDown(0);

        for (int i = 0; i < 2; ++i)
        {
            dialogTree.Current.DelegatePointer = DelegatePointer.CreateInstance<DelegatePointer>();
            dialogTree.Current.DelegatePointer.Function = ue;
            dialogTree.MoveDown(0);
            dialogTree.Current.DelegatePointer = DelegatePointer.CreateInstance<DelegatePointer>();
            dialogTree.Current.DelegatePointer.Function = ue2;
            dialogTree.MoveDown(0);
        }
        dialogTree.Current.DelegatePointer = DelegatePointer.CreateInstance<DelegatePointer>();
        dialogTree.Current.DelegatePointer.Function = ue;
        dialogTree.MoveDown(0);
        dialogTree.Current.DelegatePointer = DelegatePointer.CreateInstance<DelegatePointer>();
        dialogTree.Current.DelegatePointer.Function = new UnityEngine.Events.UnityEvent();
        dialogTree.Current.DelegatePointer.Function.AddListener(() => { Enable.SetActive(true); Disable.SetActive(false); });
    }

    // Update is called once per frame
    void Update () {
		if(Input.anyKey && !triggered)
        {
            dialogManager.DialogTree = dialogTree;
            dialogManager.TriggerDialog();
            triggered = true;
        }
	}
}
