﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatamineTest : InteractableObject {

	// Use this for initialization
	void Start () {
        m_dialogtree.MoveDown(4);
        m_dialogtree.MoveDown(1);
        m_dialogtree.Current.DelegatePointer = DelegatePointer.CreateInstance<DelegatePointer>();
        m_dialogtree.Current.DelegatePointer.Function = new UnityEngine.Events.UnityEvent();
        m_dialogtree.Current.DelegatePointer.Function.AddListener(() => 
        {
            UnityEngine.SceneManagement.Scene topdown = UnityEngine.SceneManagement.SceneManager.GetSceneByName("TopDownShooter");
            UnityEngine.SceneManagement.SceneManager.LoadScene("TopDownShooter", UnityEngine.SceneManagement.LoadSceneMode.Additive);
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(topdown);
            //foreach()
        }
        );
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
