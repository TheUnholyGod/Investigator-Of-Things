using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;

public class Typing : InteractableObject {

    [SerializeField]
    TextMeshPro text;

    [SerializeField]
    string password;

    [SerializeField]
    GameObject textObject;

    Vector3 defaultScale;
    Vector3 defaultPos;
    Quaternion defaultRotation;

    bool enableTyping = false;

	// Use this for initialization
	void Start ()
    {
        m_dialogtree.MoveDown(1);
        m_dialogtree.Current.DelegatePointer.Function.AddListener(this.EnableTypingCam);

        defaultPos = textObject.transform.position;
        defaultRotation = textObject.transform.rotation;
        defaultScale = textObject.transform.localScale;
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
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    enableTyping = false;
                    CameraManager.GetInstance().mainCamera.gameObject.SetActive(true);
                    CameraManager.GetInstance().computerCamera.gameObject.SetActive(false);

                    textObject.transform.position = defaultPos;
                    textObject.transform.rotation = defaultRotation;
                    textObject.transform.localScale = defaultScale;
                    return;
                }
                else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    Debug.Log(password.Equals(text.text));
                }

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

                        textObject.transform.position = defaultPos;
                        textObject.transform.rotation = defaultRotation;
                        textObject.transform.localScale = defaultScale;
                    }
                }
            }
        }
    }

    void EnableTypingCam()
    {
        CameraManager.GetInstance().computerCamera.gameObject.SetActive(true);
        CameraManager.GetInstance().mainCamera.gameObject.SetActive(false);
        textObject.transform.position = CameraManager.GetInstance().computerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f,10.0f));
        textObject.transform.rotation = Quaternion.identity;
        textObject.transform.localScale = new Vector3(1, 1, 1);

        enableTyping = true;
    }
}
