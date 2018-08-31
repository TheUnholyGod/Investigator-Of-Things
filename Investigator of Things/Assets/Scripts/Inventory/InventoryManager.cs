using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : Singleton<InventoryManager>
{
    public GameObject inventoryTab;
    Dictionary<string, List<InventoryItem>> inventory;

	// Use this for initialization
	void Start() {
        inventory = new Dictionary<string, List<InventoryItem>>();

        GameObject[] objects = Resources.LoadAll<GameObject>("InventoryItem");

        foreach (GameObject obj in objects)
        {
            GameObject go = Instantiate(obj, obj.transform.position, obj.transform.rotation);

            go.GetComponent<InventoryItem>().SetDialogueManager();
        }

    }
	
    public void AddItem(InventoryItem item)
    {
        if (!inventory.ContainsKey(item.itemName))
        {
            if (inventory.Count >= 6)
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

            List<InventoryItem> list = new List<InventoryItem>();

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

            obj.GetComponent<InventoryItem>().Copy(item);
            obj.GetComponent<InventoryItem>().enabled = true; ;
            obj.GetComponent<InventoryItem>().isItem = false;

            obj.name = "UI " + item.gameObject.name;

            list.Add(obj.GetComponent<InventoryItem>());

            text.text = list.Count.ToString();
            inventory.Add(item.itemName, list);

            Destroy(obj.GetComponent<LogItem>());
        }
        else
        {
            inventory[item.itemName].Add(item);
            for (int i = 0; i < inventory.Count; ++i)
            {
                List<string> keys = new List<string>(inventory.Keys);
                if (item.itemName == keys[i])
                    inventoryTab.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = inventory[item.itemName].Count.ToString();
            }
        }

        Destroy(item.gameObject);
    }
}
