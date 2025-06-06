using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;

    public List<Item> itemList;
    private Action<Item> useItemAction;

    public Inventory(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();

        // AddItem(new Item { itemType = Item.ItemType.LadFruit, amount = 1});
        //     AddItem(new Item { itemType = Item.ItemType.LadFruit, amount = 1});
        //     AddItem(new Item { itemType = Item.ItemType.LadFruit, amount = 1});
        //     AddItem(new Item { itemType = Item.ItemType.LadFruit, amount = 1});
        // RemoveItem(new Item { itemType = Item.ItemType.LadFruit });
        // RemoveItem(new Item { itemType = Item.ItemType.Jax });
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    if (inventoryItem.amount == 0)
                    {
                        inventoryItem.amount = 1;
                    }
                    // inventoryItem.amount += item.amount;
                    inventoryItem.amount++;
                    itemAlreadyInInventory = true;
                }
            }

            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool RemoveItem(Item item)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    // if (inventoryItem.amount == 0)
                    // {
                    //     inventoryItem.amount = 1;
                    // }
                    // inventoryItem.amount += item.amount;
                    inventoryItem.amount--;
                    itemInInventory = inventoryItem;
                }
            }

            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(itemInInventory);
                OnItemListChanged?.Invoke(this, EventArgs.Empty);
                return true;
            }
            return false;
        }
        else
        {
            Item itemToRemove = itemList.Find(x => x.itemType == item.itemType);
            if (itemToRemove != null)
            {
                itemList.Remove(itemToRemove);
                OnItemListChanged?.Invoke(this, EventArgs.Empty);
                return true;
            }
            return false;
        }
    }

    public void UseItem(Item item)
    {
        useItemAction(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public bool HasAvailableHat(Item.ItemType hatType)
    {
        foreach (Item item in itemList)
        {
            if (item.itemType == hatType)
            {
                return true;
            }
        }
        return false;
    }
}
