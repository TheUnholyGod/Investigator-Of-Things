using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionManager : Singleton<InteractionManager> {

    [SerializeField]
    protected DialogManager DialogManager;

    public enum Interactions
    {
        Look,
        Inspect,
        Pickup,
        Use,
        ViewLogs,
        None,
    }

    private Interactions interaction = Interactions.None;

    public GameObject dragged;
    GameObject raycasted;


    [SerializeField]
    public custom_cursor custom_Cursor;

    [SerializeField]
    TMPro.TextMeshProUGUI textbox;

    string text = "", prevtext = "";

    Dictionary<string, UnityEvent> eventslib = new Dictionary<string, UnityEvent>();

    public Interactions Interaction
    {
        get
        {
            return interaction;
        }

        set
        {
            interaction = value;
        }
    }

    // Use this for initialization
    void Start () {
        UnityEvent e = new UnityEvent();
        e.AddListener(() => { Debug.Log("Interact"); });
        eventslib.Add("Cube", e);

        e = new UnityEvent();
        e.AddListener(() => {
            InventoryItem item = dragged.GetComponent<InventoryItem>();
            //dragged = null;
            InventoryManager.GetInstance().RemoveItem(item);
            item.itemName = "item_drinkingGlass-full";
            InventoryManager.GetInstance().AddItem(item);

            AudioManager.GetInstance().PlayAudio(raycasted.name);
            });
        eventslib.Add("Fill", e);

        e = new UnityEvent();
        e.AddListener(() =>
        {
            InventoryManager.GetInstance().RemoveItem(dragged.GetComponent<InventoryItem>());
            raycasted.transform.GetChild(0).gameObject.SetActive(true);

            AudioManager.GetInstance().PlayAudio(raycasted.name);

            GameObject obj = GameObject.Find("Droomba");
            obj.GetComponent<DroombaAI>().cleaningWaypoint.SetActive(true);
        });
        eventslib.Add("Spill", e);

        e = new UnityEvent();
        e.AddListener(() =>
        {
            InventoryManager.GetInstance().RemoveItem(dragged.GetComponent<InventoryItem>());

            raycasted.GetComponent<DroombaAI>().isImmobilized = true;
            AudioManager.GetInstance().PlayAudio(raycasted.name);
            AudioManager.GetInstance().PlayAudio(raycasted.GetComponent<DroombaAI>().cleaningWaypoint.name);
        });
        eventslib.Add("Stab", e);
	}

    // Update is called once per frame
    void Update() {
        text = "";
        raycasted = custom_Cursor.GetRayCastObject();
        if (interaction != Interactions.None)
        {
            text = interaction.ToString() + " ";
            if (Input.GetMouseButton(1))
            {
                interaction = Interactions.None;
                custom_Cursor.SetCursorTexture(null);
            }
        }
        if (raycasted)
        {
            if (raycasted.gameObject.CompareTag("Interactable") || raycasted.gameObject.CompareTag("HighlightParent"))
                text += raycasted.name;
        }
        if (prevtext != text)
        {
            prevtext = text;
            textbox.text = text;
        }
    }

    public void CheckForFunction()
    {
        raycasted = custom_Cursor.GetRayCastObject();

        if (interaction != Interactions.None)
        {
            if (raycasted.GetComponent<InteractableObject>() != null)
            {
                DialogManager.DialogTree = raycasted.GetComponent<InteractableObject>().Dialogtree;
                DialogManager.TriggerDialog(new int[] { (int)(interaction) });
            }

        }
        else
        {
            if (raycasted != null)
            { 
                if (dragged != null && dragged.name.Contains("Glass") && raycasted.name.Contains("Faucet"))
                    eventslib["Fill"].Invoke();
                else if (dragged != null && dragged.name.Contains("full") && raycasted.name.Contains("Table"))
                    eventslib["Spill"].Invoke();
                else if (dragged != null && dragged.name.Contains("knife") && raycasted.name.Contains("Droomba"))
                    eventslib["Stab"].Invoke();
                else
                    eventslib["Cube"].Invoke();
            }
            dragged = null;
        }
    }

    public void HandleInteractions()
    {

    }

    public void SetInteractions(Interactions _interaction)
    {
        interaction = _interaction;
    }

    public void SetLook()
    {
        SetInteractions(Interactions.Look);
    }

    public void SetInspect()
    {
        SetInteractions(Interactions.Inspect);
    }

    public void SetPickUp()
    {
        SetInteractions(Interactions.Pickup);
    }

    public void SetUse()
    {
        SetInteractions(Interactions.Use);
    }

    public void SetLogs()
    {
        SetInteractions(Interactions.ViewLogs);
    }
}
