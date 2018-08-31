using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;

public class Typing : InteractableObject {

    [SerializeField]
    TextMeshPro text;

    [SerializeField]
    GameObject textMesh;

    Vector3 defaultPos;
    Quaternion defaultRotation;

    bool enableTyping = false;

	// Use this for initialization
	void Start ()
    {
        m_dialogtree.MoveDown(1);
        m_dialogtree.Current.DelegatePointer.Function.AddListener(this.EnableTypingCam);
        text = textMesh.GetComponent<TextMeshPro>();

        defaultPos = textMesh.transform.position;
        defaultRotation = text.transform.rotation;
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

        if (enableTyping)
        {

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
                    else if ((characters == (char)8 && text.text.Length <= 0) || characters == (char)27)
                    {
                        enableTyping = false;
                        CameraManager.GetInstance().mainCamera.gameObject.SetActive(true);
                        CameraManager.GetInstance().computerCamera.gameObject.SetActive(false);

                        textMesh.transform.position = defaultPos;
                        textMesh.transform.rotation = defaultRotation;
                    }
                }
            }
        }
    }

    void EnableTypingCam()
    {
        CameraManager.GetInstance().computerCamera.gameObject.SetActive(true);
        CameraManager.GetInstance().mainCamera.gameObject.SetActive(false);
        textMesh.transform.position = CameraManager.GetInstance().computerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f,10.0f));
        textMesh.transform.rotation = Quaternion.identity;

        enableTyping = true;
    }
}
