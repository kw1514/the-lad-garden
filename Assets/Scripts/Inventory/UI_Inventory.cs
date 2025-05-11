using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;
using SaveLoadSystem;

public class UI_Inventory : MonoBehaviour//, ISaveable
{
    private Inventory inventory;
    SaveLoadNamespace saveSystem;
    UI ui;

    [SerializeField] Transform itemSlotContainer;
    [SerializeField] Transform itemSlotTemplate;

    int jaxAmount;
    int ladFruitAmount;


    private void Start()
    {
        // itemSlotContainer = transform.Find("itemSlotContainer");
        // itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        // ui = FindObjectOfType<UI>();
        // isInventoryOpen = false;
        saveSystem = FindObjectOfType<SaveLoadNamespace>();
        ui = FindObjectOfType<UI>();

        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemType == Item.ItemType.LadFruit)
            {
                ladFruitAmount = item.amount;
                // saveSystem.Save();

                // Debug.Log("in game fruit " + ladFruitAmount);
            }
            else if (item.itemType == Item.ItemType.Jax)
            {
                jaxAmount = item.amount;
                // saveSystem.Save();
                // Debug.Log("in game jax " + jaxAmount);
            }
        }
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Tab))
        // {
        //     if (!isInventoryOpen)
        //     {
        //         ui.OpenInventory();
        //         isInventoryOpen = true;
        //     }
        //     else if (isInventoryOpen)
        //     {
        //         ui.CloseInventory();
        //         isInventoryOpen = false;
        //     }
        // }
    }


    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    public List<Item> GetInventory()
    {
        return inventory.GetItemList();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemType == Item.ItemType.LadFruit)
            {
                ladFruitAmount = item.amount;
                // Debug.Log("in game fruit " + ladFruitAmount);
            }
            else if (item.itemType == Item.ItemType.Jax)
            {
                jaxAmount = item.amount;
                // Debug.Log("in game jax " + jaxAmount);
            }
        }
        saveSystem = FindObjectOfType<SaveLoadNamespace>();
        Debug.Log(saveSystem.GetFullSavePath());
        // saveSystem.Save();
    }

    private void ResetInventory(int jax, int fruit)
    {
        Inventory refreshInventory;
        Player player;
        player = FindObjectOfType<Player>();
        refreshInventory = player.inventory;

        if (refreshInventory != null)
        {
            SetInventory(refreshInventory);
            refreshInventory.AddItem(new Item { itemType = Item.ItemType.LadFruit, amount = fruit });
            refreshInventory.AddItem(new Item { itemType = Item.ItemType.Jax, amount = jax });
            RefreshInventoryItems();
        }

        // inventory.AddItem(new Item { itemType = Item.ItemType.LadFruit, amount = fruit});
        // inventory.AddItem(new Item { itemType = Item.ItemType.Jax, amount = jax});
        // RefreshInventoryItems();
    }

    public void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        float x = -2.47f;
        float y = 2.55f;
        float itemSlotCellSize = 16f;

        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI amountText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                amountText.SetText(item.amount.ToString());
            }
            else
            {
                amountText.SetText("");
            }

            x++;
            if (x > 3)
            {
                x = -2.47f;
                y = y + -0.85f;
            }
        }
    }

    // [System.Serializable]
    // struct InventoryData
    // {
    //     public int jaxAmount;
    //     public int ladFruitAmount;
    // }

    // public bool NeedsToBeSaved()
    // {
    //     return true;
    // }

    // public bool NeedsReinstantiation()
    // {
    //     return false;
    // }

    // public object SaveState()
    // {
    //     return new InventoryData()
    //     {
    //         jaxAmount = jaxAmount,
    //         ladFruitAmount = ladFruitAmount
    //     };
    // }

    // public void LoadState(object state)
    // {
    //     InventoryData data = (InventoryData)state;
    //     jaxAmount = data.jaxAmount;
    //     ladFruitAmount = data.ladFruitAmount;

    //     Debug.Log(ladFruitAmount);

    //     // inventory.AddItem(new Item { itemType = Item.ItemType.LadFruit, amount = ladFruitAmount });
    //     // inventory.AddItem(new Item { itemType = Item.ItemType.Jax, amount = jaxAmount });
    //     // RefreshInventoryItems();
    // }

    // public void PostInstantiation(object state)
    // {
    //     InventoryData data = (InventoryData)state;
    // }

    // public void GotAddedAsChild(GameObject obj, GameObject hisParent)
    // {
    // }
}
