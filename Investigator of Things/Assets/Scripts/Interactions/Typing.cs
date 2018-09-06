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
    public bool canType = true;

    string wrongMessage = "Wrong Password";

	// Use this for initialization
	void Start ()
    {
        m_dialogtree.MoveDown(3);
        m_dialogtree.Current.DelegatePointer.Function.AddListener(this.EnableTypingCam);

        defaultPos = textObject.transform.position;
        defaultRotation = textObject.transform.rotation;
        defaultScale = textObject.transform.localScale;
	}

    // Update is called once per frame
    void Update () {

        if (enableTyping)
        {
            if (Input.anyKeyDown)
            {
                if (wrongMessage.Equals(text.text))
                    text.text = "";

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    enableTyping = false;
                    CameraManager.GetInstance().mainCamera.gameObject.SetActive(true);
                    CameraManager.GetInstance().computerCamera.gameObject.SetActive(false);
                    text.fontSize = 1.0f;
                    textObject.transform.localScale = new Vector3(1, 1, 1);

                    textObject.transform.position = defaultPos;
                    textObject.transform.rotation = defaultRotation;
                    textObject.transform.localScale = defaultScale;
                    return;
                }
                else if (!canType)
                    return;
                else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    if (password.Equals(text.text))
                    {
                        m_dialogtree = Resources.Load<DialogTree>("Dialog/Computer/ComputerAfterPowerTree");

                        text.text = "Welcome back Daniel";

                        m_dialogtree.MoveToRoot();
                        m_dialogtree.MoveDown(4);
                        m_dialogtree.Current.DelegatePointer.Function.AddListener(() =>
                        {
                            GameObject droomba = GameObject.Find("Droomba");
                            if (!droomba.GetComponent<DroombaAI>().isCleaning)
                                droomba.GetComponent<DroombaAI>().enabled = true;
                        });
                        canType = false;
                    }
                    else
                    {
                        text.text = wrongMessage;
                    }
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
        textObject.transform.localScale = new Vector3(10, 10, 10);

        enableTyping = true;
    }
}
