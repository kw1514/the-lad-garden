using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Sprite jaxSprite;
    public Sprite ladFruitSprite;
    public Sprite ladSprite;
    public Sprite ladButtonDefault;
    public Sprite pirateHatSprite;
    public Sprite partyHatSprite;
    public Sprite topHatSprite;
}
