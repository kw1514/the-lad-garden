using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        LadFruit,
        Jax,
        Lad,
        PirateHat,
        TopHat,
        PartyHat
    }

    public ItemType itemType;
    public int amount = 1;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Jax: return ItemAssets.Instance.jaxSprite;
            case ItemType.LadFruit: return ItemAssets.Instance.ladFruitSprite;
            case ItemType.Lad:    return ItemAssets.Instance.ladSprite;
            case ItemType.PirateHat:  return ItemAssets.Instance.pirateHatSprite;
            case ItemType.PartyHat:   return ItemAssets.Instance.partyHatSprite;
            case ItemType.TopHat:     return ItemAssets.Instance.topHatSprite;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Jax:
            case ItemType.LadFruit:
            case ItemType.Lad:
                return true;
            case ItemType.PirateHat:
            case ItemType.TopHat:
            case ItemType.PartyHat:
                return false;
        }
    }
}
