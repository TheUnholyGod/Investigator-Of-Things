using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;

public class Typing : MonoBehaviour {

    TextMeshPro text;

	// Use this for initialization
	void Start () {
        text = GetComponent<TextMeshPro>();
	}

    private void OnGUI()
    {
        //Event e = Event.current;

        //if (e.isKey)
        //{
        //    if (e.keyCode <= KeyCode.Z && e.keyCode >= KeyCode.A)
        //        text.text += e.keyCode.ToString();
        //    else if (e.keyCode >= KeyCode.Alpha0 && e.keyCode <= KeyCode.Alpha9)
        //        text.text += e.keyCode - KeyCode.Alpha0;
        //    else if (e.keyCode >= KeyCode.Keypad0 && e.keyCode <= KeyCode.Keypad9)
        //        text.text += e.keyCode - KeyCode.Keypad0;
        //    else if (e.keyCode == KeyCode.Backspace)
        //        text.text.Remove(text.text.Length - 1);
        //}
    }

    // Update is called once per frame
    void Update () {

        if (Input.anyKeyDown)
        {
            foreach (char characters in Input.inputString)
            {
                if (char.IsDigit(characters))
                    text.text += characters;
                else if (char.IsLetter(characters))
                    text.text += characters;
                else if (characters == (char)8 && text.text.Length > 0)
                    text.text = text.text.Remove(text.text.Length - 1);
            }
        }
    }
}
