using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogManager : Singleton<LogManager> {

    public GameObject inventoryTab;
    Dictionary<string, List<LogItem>> inventory;

    // Use this for initialization
    void Start()
    {
        inventory = new Dictionary<string, List<LogItem>>();

        GameObject[] objects = Resources.LoadAll<GameObject>("LogItem");

        foreach (GameObject obj in objects)
        {
            GameObject go = Instantiate(obj, obj.transform.position, obj.transform.rotation);

            go.GetComponent<LogItem>().SetDialogueManager();
        }
    }

    public void AddItem(LogItem item)
    {
        if (!inventory.ContainsKey(item.logName))
        {
            if (inventory.Count >= 5)
                return;

            GameObject obj = new GameObject();
            obj.transform.parent = inventoryTab.transform.GetChild(inventory.Count);

            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = new Vector3(1, 1, 1);

            TextMeshProUGUI text = obj.AddComponent<TextMeshProUGUI>();
            obj.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0);
            obj.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0);
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -15.0f);

            text.alignment = TextAlignmentOptions.Midline;

            //obj = new GameObject();
            //obj.transform.parent = inventoryTab.transform.GetChild(inventory.Count);

            //obj.transform.localPosition = Vector3.zero;
            //obj.transform.localScale = new Vector3(1, 1, 1);

            List<LogItem> list = new List<LogItem>();

            //Image img = obj.AddComponent<Image>();
            //img.sprite = Resources.Load<Sprite>("Icons/" + item.itemName) as Sprite;

            //obj.AddComponent<InteractableObject>();
            //InventoryItem newItem = obj.AddComponent<InventoryItem>();
            //newItem.itemName = item.itemName;
            //newItem.isUI = true;

            //list.Add(newItem);

            obj = Instantiate(Resources.Load<GameObject>("Items/UIItem"));
            obj.transform.SetParent(inventoryTab.transform.GetChild(inventory.Count));

            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

            obj.GetComponent<LogItem>().Copy(item);
            obj.GetComponent<LogItem>().enabled = true; ;
            obj.GetComponent<LogItem>().isItem = false;

            obj.name = "UI " + item.gameObject.name;

            list.Add(obj.GetComponent<LogItem>());

            text.text = list.Count.ToString();
            inventory.Add(item.logName, list);

            Destroy(obj.GetComponent<InventoryItem>());
        }

        Destroy(item.gameObject);
    }
}
