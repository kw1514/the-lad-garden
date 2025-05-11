using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SaveLoadSystem;

public class Player : MonoBehaviour, ISaveable
{
    // [SerializeField] GameObject labMenu;
    // [SerializeField] GameObject inventoryUI;
    // private GameObject player;
    [SerializeField] private UI_Inventory uiInventory;

    private static Vector3 respawnPoint;

    [SerializeField] GameObject eIndicator;
    [SerializeField] public GameObject startupInfo;
    [SerializeField] public bool startupInfoOpen;

    public int jaxNum;
    public int fruitNum;
    int topHatNum = 0;
    int partyHatNum = 0;
    int pirateHatNum = 0;
    bool isLoaded;
    [SerializeField] public TextMeshProUGUI jaxCounter;
    [SerializeField] public TextMeshProUGUI fruitCounter;

    FirstPersonController playerMovement;
    public Inventory inventory;
    UI ui;
    LadInventory ladInventory;
    AudioManager audioPlayer;
    LevelManager levelManager;

    void Start()
    {
        // player = this.gameObject;
        playerMovement = FindObjectOfType<FirstPersonController>();
        ui = FindObjectOfType<UI>();
        ladInventory = FindObjectOfType<LadInventory>();
        audioPlayer = FindObjectOfType<AudioManager>();
        levelManager = FindObjectOfType<LevelManager>();


        respawnPoint = transform.position;
        Debug.Log(respawnPoint);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerMovement.GetComponent<FirstPersonController>().enabled = false;
        startupInfo.SetActive(true);
        ui.miniMapCanvas.SetActive(false);
        startupInfoOpen = true;
        // labMenu.SetActive(false);

        inventory = new Inventory(UseItem);
        uiInventory.SetInventory(inventory);
        ui.SetLabInventory(inventory);
        eIndicator.SetActive(false);

        isLoaded = false;
    }

   
    void Update()
    {
        CheckForInteractions();

        if(startupInfoOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (!startupInfoOpen)
        {
            // Cursor.lockState = CursorLockMode.Locked;
            // Cursor.visible = false;
            if (isLoaded)
            {
                // Debug.Log("update " + isLoaded);
                // check if they are 0
                if (fruitNum > 0)
                {
                    inventory.AddItem(new Item { itemType = Item.ItemType.LadFruit, amount = fruitNum});
                }

                if (jaxNum > 0)
                {
                    inventory.AddItem(new Item { itemType = Item.ItemType.Jax, amount = jaxNum});
                }

                if (partyHatNum > 0)
                {
                    inventory.AddItem(new Item { itemType = Item.ItemType.PartyHat, amount = partyHatNum});
                }

                if (pirateHatNum > 0)
                {
                    inventory.AddItem(new Item { itemType = Item.ItemType.PirateHat, amount = pirateHatNum});
                }

                if (topHatNum > 0)
                {
                    inventory.AddItem(new Item { itemType = Item.ItemType.TopHat, amount = topHatNum});
                }

                // inventory.AddItem(new Item { itemType = Item.ItemType.LadFruit, amount = fruitNum});
                // inventory.AddItem(new Item { itemType = Item.ItemType.Jax, amount = jaxNum});
                // inventory.AddItem(new Item { itemType = Item.ItemType.PartyHat, amount = partyHatNum});
                // inventory.AddItem(new Item { itemType = Item.ItemType.PirateHat, amount = pirateHatNum});
                // inventory.AddItem(new Item { itemType = Item.ItemType.TopHat, amount = topHatNum});
                isLoaded = false;
                uiInventory.RefreshInventoryItems();
            }
        }

        // if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && startupInfoOpen)
        // {
        //     startupInfo.SetActive(false);
        //     startupInfoOpen = false;
        //     playerMovement.GetComponent<FirstPersonController>().enabled = true;
        // }

        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemType == Item.ItemType.LadFruit)
            {
                fruitNum = item.amount;
                // Debug.Log("in game fruit " + ladFruitAmount);
            }
            else if (item.itemType == Item.ItemType.Jax)
            {
                jaxNum = item.amount;
                // Debug.Log("in game jax " + jaxAmount);
            }
            else if (item.itemType == Item.ItemType.PirateHat)
            {
                pirateHatNum = item.amount;
            }
            else if (item.itemType == Item.ItemType.PartyHat)
            {
                partyHatNum = item.amount;
            }
            else if (item.itemType == Item.ItemType.TopHat)
            {
                topHatNum = item.amount;
            }
        }
        jaxCounter.SetText(jaxNum.ToString());
        fruitCounter.SetText(fruitNum.ToString());
    }

    private void UseItem(Item item)
    {
        switch (item.itemType) 
        {
            case Item.ItemType.LadFruit:
            // do something
            inventory.RemoveItem(item);
            break;
            case Item.ItemType.Jax:
            // do something
            inventory.RemoveItem(item);
            break;
        }
    }

    private void CheckForInteractions()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitData;
        Physics.Raycast(ray, out hitData);

        if (Input.GetKeyDown(KeyCode.E) && ui.isLadDetailOpen)
        {
            ui.CloseLadDetail();
        }

        if(hitData.collider.gameObject.tag == "Lab") {
            eIndicator.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E) && !ui.isLabMenuOpen && !ui.isLadDetailOpen)
            {
                eIndicator.SetActive(false);
                ui.OpenLab();
            }
            else if(Input.GetKeyDown(KeyCode.E) && ui.isLabMenuOpen && !ui.isLabCreationMenuOpen)
            {
                eIndicator.SetActive(false);
                ui.CloseLab();
            }
        }
        else if (hitData.collider.gameObject.tag == "UncollectedLad")
        {
            eIndicator.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                hitData.collider.gameObject.tag = "Lad";
                ladInventory.RefreshLadInventoryItems();
            }
        
        }
        else if(hitData.collider.gameObject.tag == "LadTree") {
            eIndicator.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E) && !ui.isTreeMenuOpen)
            {
                eIndicator.SetActive(false);
                ui.OpenTree();
            }
            else if(Input.GetKeyDown(KeyCode.E) && ui.isTreeMenuOpen)
            {
                eIndicator.SetActive(false);
                ui.CloseTree();
            }
        }
        else if(hitData.collider.gameObject.tag == "HatShop") {
            eIndicator.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E) && !ui.isHatMenuOpen)
            {
                eIndicator.SetActive(false);
                ui.OpenHat();
            }
            else if(Input.GetKeyDown(KeyCode.E) && ui.isHatMenuOpen)
            {
                eIndicator.SetActive(false);
                ui.CloseHat();
            }
        }
        else
        {
            eIndicator.SetActive(false);
        }
    }

    protected void OnTriggerEnter(Collider other) 
    {
        // add other tags here
        if (other.gameObject.tag == "Jax")
        {
            inventory.AddItem(new Item { itemType = Item.ItemType.Jax});
            // jaxNum++;
            // jaxCounter.SetText(jaxNum.ToString());
            audioPlayer.PlayPickupClip();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "LadFruit")
        {
            inventory.AddItem(new Item { itemType = Item.ItemType.LadFruit});
            // fruitNum++;
            // fruitCounter.SetText(fruitNum.ToString());
            audioPlayer.PlayPickupClip();
            Destroy(other.gameObject);
        }

        else if (other.gameObject.tag == "FallDetector")
        {
            Debug.Log("Fall");
            // playerMovement.GetComponent<FirstPersonController>().enabled = false;
            // transform.position = respawnPoint;
            // playerMovement.GetComponent<FirstPersonController>().enabled = true;
            // levelManager.LoadGame();
            transform.Translate(respawnPoint.x - transform.position.x, respawnPoint.y - transform.position.y, respawnPoint.z - transform.position.z, Space.World);
            // public void Translate(float x, float y, float z); 
            Debug.Log(respawnPoint);
        }
    }

    void setJax(int num)
    {
        jaxNum = num;
        jaxCounter.SetText(num.ToString());
    }

    void setFruit(int num)
    {
        fruitNum = num;
        fruitCounter.SetText(num.ToString());
    }

    void setParty(int num)
    {
        partyHatNum = num;
    }

    void setPirate(int num)
    {
        pirateHatNum = num;
    }

    void setTop(int num)
    {
        topHatNum = num;
    }

    [System.Serializable]
    struct PlayerData
    {
        public int jaxNum;
        public int fruitNum;
        public int partyHatNum;
        public int pirateHatNum;
        public int topHatNum;
    }

    public bool NeedsToBeSaved()
    {
        return true;
    }

    public bool NeedsReinstantiation()
    {
        return false;
    }

    public object SaveState()
    {
        return new PlayerData()
        {
            jaxNum = jaxNum,
            fruitNum = fruitNum,
            partyHatNum = partyHatNum,
            pirateHatNum = pirateHatNum,
            topHatNum = topHatNum
        };
    }

    public void LoadState(object state)
    {
        PlayerData data = (PlayerData)state;
        jaxNum = data.jaxNum;
        fruitNum = data.fruitNum;
        partyHatNum = data.partyHatNum;
        pirateHatNum = data.pirateHatNum;
        topHatNum = data.topHatNum;

        setJax(jaxNum);
        setFruit(fruitNum);
        setParty(partyHatNum);
        setPirate(pirateHatNum);
        setTop(topHatNum);
        // Debug.Log("load jax " + jaxNum);
        // Debug.Log("load fruit " + fruitNum);

        isLoaded = true;
    }

    public void PostInstantiation(object state)
    {
        PlayerData data = (PlayerData)state;
    }

    public void GotAddedAsChild(GameObject obj, GameObject hisParent)
    {
        
    }
}
