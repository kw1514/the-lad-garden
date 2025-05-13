using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class UI : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] public GameObject labMenu;
    [SerializeField] GameObject ladTreeMenu;
    [SerializeField] GameObject labCreationMenu;
    [SerializeField] GameObject InventoryMenu;
    [SerializeField] GameObject ladInventory;
    [SerializeField] GameObject hatShop;
    [SerializeField] public GameObject miniMapCanvas;

    [Header("UI Elements")]
    [SerializeField] Slider FuelSlider;
    [SerializeField] public GameObject errorPanel;
    [SerializeField] GameObject crosshair;

    [Header("Lab Menu Stats")]
    [SerializeField] TextMeshProUGUI creativityText;
    [SerializeField] TextMeshProUGUI intelligenceText;
    [SerializeField] TextMeshProUGUI strengthText;
    [SerializeField] TextMeshProUGUI adaptabilityText;
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI birthdayText;
    [SerializeField] GameObject newRegularLadIcon;
    [SerializeField] GameObject newThomiscianLadIcon;

    [Header("Lad Detail")]
    [SerializeField] GameObject closeLadDetailButtonObject;
    [SerializeField] GameObject assignLadJob;
    [SerializeField] GameObject ladObject;
    [SerializeField] public Button farmerButton;
    [SerializeField] public Button joblessButton;
    [SerializeField] public Button scientistButton;
    [SerializeField] TextMeshProUGUI ladNameText;
    [SerializeField] GameObject topHatObject;
    [SerializeField] GameObject partyHatObject;
    [SerializeField] GameObject pirateHatObject;
    [SerializeField] GameObject topHatObject2;
    [SerializeField] GameObject partyHatObject2;
    [SerializeField] GameObject pirateHatObject2;

    [Header("Tree Menu")]
    [SerializeField] public Button helpButton;
    [SerializeField] public Button questionButton;
    [SerializeField] public Button buyFruitButton;
    [SerializeField] public Button closeTreeButton;
    [SerializeField] public TextMeshProUGUI treeText;
    int message = 0;

    [Header("Hat Menu")]
    [SerializeField] public Button hatHelpButton;
    [SerializeField] public Button hatQuestionButton;
    [SerializeField] public Button buyTopHatButton;
    [SerializeField] public Button buyPartyHatButton;
    [SerializeField] public Button buyPirateHatButton;
    [SerializeField] public Button closeHatButton;
    [SerializeField] public TextMeshProUGUI hatText;

    [Header("Hat Buttons")]
    [SerializeField] Button topHatButton;
    [SerializeField] Button partyHatButton;
    [SerializeField] Button pirateHatButton;
    int topHatNum;
    int partyHatNum;
    int pirateHatNum;

    [Header("Menus Open?")]
    [SerializeField] bool isInventoryOpen;
    [SerializeField] bool isLadInventoryOpen;
    [SerializeField] public bool isLabMenuOpen;
    [SerializeField] public bool isLabCreationMenuOpen;
    [SerializeField] public bool isLadDetailOpen;
    [SerializeField] public bool isTreeMenuOpen;
    [SerializeField] public bool isHatMenuOpen;

    [Header("Buttons")]
    [SerializeField] public Button combineButton;
    [SerializeField] Button createButton;
    [SerializeField] GameObject createButtonGameObject;

    [Header("Lad 1 Button")]
    [SerializeField] public Button lad1Button;
    [SerializeField] public TextMeshProUGUI lad1Name;
    [SerializeField] public bool lad1ButtonClicked;

    [Header("Lad 2 Button")]
    [SerializeField] public Button lad2Button;
    [SerializeField] public TextMeshProUGUI lad2Name;
    [SerializeField] public bool lad2ButtonClicked;

    [Header("Input Fields")]
    [SerializeField] InputField ladNameInput;
    [SerializeField] GameObject input;
    string userladName;

    [Header("Lads")]
    [SerializeField] GameObject ladPrefab;
    [SerializeField] Transform ladParent;
    [HideInInspector] public int numOfLadFruit;

    [Header("Currency")]
    // [SerializeField] TextMeshProUGUI JaxCounter;
    // [SerializeField] TextMeshProUGUI LadFruitCounter;
    // int numOfLadFruit;
    int numOfJax;


    // [Header("Lad Fruit")]
    // [SerializeField] TextMeshProUGUI LadFruitCounter;
    // int numOfLadFruit;


    FirstPersonController player;
    Player player2;
    Inventory inventory;
    UI_Inventory ui_inventory;
    LadInventory ladList;
    LadMixing ladMixing;

    GameObject newLad;
    private float fuel;

    void Start()
    {
        player = FindObjectOfType<FirstPersonController>();
        player2 = FindObjectOfType<Player>();
        ui_inventory = FindObjectOfType<UI_Inventory>();
        ladList = FindObjectOfType<LadInventory>();
        ladMixing = FindObjectOfType<LadMixing>();
        fuel = player.getFuel();
        FuelSlider.maxValue = 100f;


        // set all menus to false
        isInventoryOpen = false;
        isLadInventoryOpen = false;
        isLabMenuOpen = false;
        isLabCreationMenuOpen = false;
        isLadDetailOpen = false;
        lad1ButtonClicked = false;
        lad2ButtonClicked = false;
        isTreeMenuOpen = false;
        isHatMenuOpen = false;
    }

    void Update()
    {
        fuel = player.fuel;
        FuelSlider.value = fuel;
        CheckInventory();
        CheckLadInventory();

        // makes sure that a lad cannot have a blank name
        if (ladNameInput.text == "")
        {
            createButton.interactable = false;
        }
        else
        {
            createButton.interactable = true;
        }

        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemType == Item.ItemType.LadFruit)
            {
                numOfLadFruit = item.amount;
            }
            if (item.itemType == Item.ItemType.Jax)
            {
                numOfJax = item.amount;
            }
        }
        // JaxCounter.text = (numOfJax.ToString());
        // LadFruitCounter.text = (numOfLadFruit.ToString());

        // Update hat button states
        UpdateHatButtonStates();
    }

    private void UpdateHatButtonStates()
    {
        string currentHat = ladObject.GetComponent<LadGenes>().GetHat();

        // Top Hat
        bool hasTopHat = inventory.HasAvailableHat(Item.ItemType.TopHat);
        topHatButton.interactable = hasTopHat || currentHat == "top";
        ColorBlock topHatColors = topHatButton.colors;
        if (currentHat == "top") {
            topHatColors.normalColor = Color.green;
            topHatColors.disabledColor = Color.green;
        } else {
            topHatColors.normalColor = Color.white;
            topHatColors.disabledColor = Color.red;
        }
        topHatButton.colors = topHatColors;

        // Party Hat
        bool hasPartyHat = inventory.HasAvailableHat(Item.ItemType.PartyHat);
        partyHatButton.interactable = hasPartyHat || currentHat == "party";
        ColorBlock partyHatColors = partyHatButton.colors;
        if (currentHat == "party") {
            partyHatColors.normalColor = Color.green;
            partyHatColors.disabledColor = Color.green;
        } else {
            partyHatColors.normalColor = Color.white;
            partyHatColors.disabledColor = Color.red;
        }
        partyHatButton.colors = partyHatColors;

        // Pirate Hat
        bool hasPirateHat = inventory.HasAvailableHat(Item.ItemType.PirateHat);
        pirateHatButton.interactable = hasPirateHat || currentHat == "pirate";
        ColorBlock pirateHatColors = pirateHatButton.colors;
        if (currentHat == "pirate") {
            pirateHatColors.normalColor = Color.green;
            pirateHatColors.disabledColor = Color.green;
        } else {
            pirateHatColors.normalColor = Color.white;
            pirateHatColors.disabledColor = Color.red;
        }
        pirateHatButton.colors = pirateHatColors;
    }

    public void SetLabInventory(Inventory inventory)
    {
        this.inventory = inventory;
    }

    private void CheckInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isInventoryOpen)
            {
                OpenInventory();
            }
            else if (isInventoryOpen)
            {
                CloseInventory();
            }
        }
    }

    private void CheckLadInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isLadInventoryOpen)
            {
                CloseLadInventory();
            }
        }
    }


    //////////////////////////////////////////// Button On CLick Methods ///////////////////////////////////////////////////////////
    public void ClickLad1Button()
    {
        lad1ButtonClicked = true;
        lad2ButtonClicked = false;
    }

    public void ClickLad2Button()
    {
        lad1ButtonClicked = false;
        lad2ButtonClicked = true;
    }


    // is attached to the combine button
    public void CombineButtonOnClick()
    {
        // subtracts the one lad fruit from inventory
        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemType == Item.ItemType.LadFruit)
            {
                item.amount = item.amount - 1;
                numOfLadFruit = numOfLadFruit - 1;

                if (item.amount == 0)
                {
                    inventory.RemoveItem(item);
                }
                ui_inventory.RefreshInventoryItems();
            }
        }
    }


    // Is attached to the Create button
    // the new lad is created in the OpenLabCreationMenu function
    public void CreateButtonOnClick()
    {
        CloseLabCreationMenu();
        CloseLab();
        userladName = ladNameInput.text;

        newLad.name = userladName;
        ladList.ladList.Add(newLad);
        ladList.RefreshLadInventoryItems();
        ladNameInput.text = "";
    }

    public void FarmerButtonOnClick()
    {
        farmerButton.interactable = false;
        joblessButton.interactable = true;
        scientistButton.interactable = true;
        ladObject.transform.GetComponent<LadGenes>().SetJob("farmer");
    }

    public void JoblessButtonOnClick()
    {
        farmerButton.interactable = true;
        joblessButton.interactable = false;
        scientistButton.interactable = true;
        ladObject.transform.GetComponent<LadGenes>().SetJob("jobless");
    }

    public void ScientistButtonOnClick()
    {
        farmerButton.interactable = true;
        joblessButton.interactable = true;
        scientistButton.interactable = false;
        ladObject.transform.GetComponent<LadGenes>().SetJob("scientist");
    }

    //Hat Shop Dialogue
    public void HatHelpButtonOnClick()
    {
        hatText.text = ("Legends say it was a part of a much larger island once, but it was torn away for some reason... ah but that's just a legend...");
    }

    public void HatQuestionButtonOnClick()
    {
        hatText.text = ("I'm from an island not dissimilar to this one, although it is far more populated. I flew over on a ship and decided to sell my wares here when I heard it was being rebuilt! Figured you folks could use some fashion to spice up rebuilding efforts.");
    }

    public void TopHatButtonOnClick()
    {
        if (numOfJax >= 10)
        {
            inventory.AddItem(new Item { itemType = Item.ItemType.TopHat });

            // subtracts the jax
            foreach (Item item in inventory.GetItemList())
            {
                if (item.itemType == Item.ItemType.Jax)
                {
                    item.amount = item.amount - 10;
                    numOfJax = numOfJax - 10;

                    if (item.amount == 0)
                    {
                        inventory.RemoveItem(item);
                    }
                    ui_inventory.RefreshInventoryItems();
                }
            }

            hatText.SetText("Enjoy!");
        }
        else if (numOfJax < 10)
        {
            hatText.text = ("Hold on. You need " + (10 - numOfJax) + " more Jax for that hat!");
        }
    }
    public void PartyHatButtonOnClick()
    {
        if (numOfJax >= 15)
        {
            inventory.AddItem(new Item { itemType = Item.ItemType.PartyHat });
            foreach (Item item in inventory.GetItemList())
            {
                if (item.itemType == Item.ItemType.Jax)
                {
                    item.amount = item.amount - 15;
                    numOfJax = numOfJax - 15;

                    if (item.amount == 0)
                    {
                        inventory.RemoveItem(item);
                    }
                    ui_inventory.RefreshInventoryItems();
                }
            }

            hatText.SetText("Enjoy!");
        }
        else if (numOfJax < 10)
        {
            hatText.text = ("Hold on. You need " + (15 - numOfJax) + " more Jax for that hat!");
        }
    }
    public void PirateHatButtonOnClick()
    {
        if (numOfJax >= 30)
        {
            inventory.AddItem(new Item { itemType = Item.ItemType.PirateHat });
            foreach (Item item in inventory.GetItemList())
            {
                if (item.itemType == Item.ItemType.Jax)
                {
                    item.amount = item.amount - 30;
                    numOfJax = numOfJax - 30;

                    if (item.amount == 0)
                    {
                        inventory.RemoveItem(item);
                    }
                    ui_inventory.RefreshInventoryItems();
                }
            }

            hatText.SetText("Enjoy!");
        }
        else if (numOfJax < 10)
        {
            hatText.text = ("Hold on. You need " + (30 - numOfJax) + " more Jax for that hat!");
        }
    }
    public void HatCloseButtonOnClick()
    {
        CloseHat();
    }
    //End of Hat Dialogue



    //Lad Tree Dialogue
    public void HelpButtonOnClick()
    {
        message = message + 1;
        if (message == 1)
        {
            treeText.text = ("Well, for starters, why not try assigning a lad or two as farmers? Do so in the Lad inventory. " +
                            "Farmers accumulate Jax overtime.");
        }
        if (message == 2)
        {
            treeText.SetText("If you find a lad fruit or buy one from me, you can use them in the nearby lab.");
        }
        if (message == 3)
        {
            treeText.SetText("There are a few rare varients of lads out there. Perhaps you can search the nearby islands for those.");
        }
        if (message == 4)
        {
            treeText.SetText("I see a hat shop has opened nearby. Maybe go take a look?");
            message = 0;
        }

    }

    public void QuestionButtonOnClick()
    {
        treeText.text = ("I am the Lad Tree of this island. I am one of many, but this island is my home. I've been here since I was but a seedling.");
    }

    public void BuyButtonOnClick()
    {
        if (numOfJax >= 5)
        {
            inventory.AddItem(new Item { itemType = Item.ItemType.LadFruit });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.Jax });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.Jax });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.Jax });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.Jax });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.Jax });

            treeText.SetText("Take this fruit and create a new lad at the nearby lab.");
        }
        else if (numOfJax < 5)
        {
            treeText.text = ("You need " + (5 - numOfJax) + " more Jax for a fruit. Maybe try searching nearby?");
        }
    }

    public void CloseButtonOnClick()
    {
        CloseTree();
    }
    //End of tree Dialogue

    // set the hat button methods
    public void SetTopHatButtonOnClick()
    {
        if (ladObject.GetComponent<LadGenes>().GetHat() == "top")
        {
            ladObject.GetComponent<LadGenes>().setHat("none");
            inventory.AddItem(new Item { itemType = Item.ItemType.TopHat });
        }
        else if (inventory.HasAvailableHat(Item.ItemType.TopHat))
        {
            ladObject.GetComponent<LadGenes>().setHat("top");
            inventory.RemoveItem(new Item { itemType = Item.ItemType.TopHat });
        }
        ui_inventory.RefreshInventoryItems();
        UpdateHatButtonStates();
    }

    public void SetPartyHatButtonOnClick()
    {
        if (ladObject.GetComponent<LadGenes>().GetHat() == "party")
        {
            ladObject.GetComponent<LadGenes>().setHat("none");
            inventory.AddItem(new Item { itemType = Item.ItemType.PartyHat });
        }
        else if (inventory.HasAvailableHat(Item.ItemType.PartyHat))
        {
            ladObject.GetComponent<LadGenes>().setHat("party");
            inventory.RemoveItem(new Item { itemType = Item.ItemType.PartyHat });
        }
        ui_inventory.RefreshInventoryItems();
        UpdateHatButtonStates();
    }

    public void SetPirateHatButtonOnClick()
    {
        if (ladObject.GetComponent<LadGenes>().GetHat() == "pirate")
        {
            ladObject.GetComponent<LadGenes>().setHat("none");
            inventory.AddItem(new Item { itemType = Item.ItemType.PirateHat });
        }
        else if (inventory.HasAvailableHat(Item.ItemType.PirateHat))
        {
            ladObject.GetComponent<LadGenes>().setHat("pirate");
            inventory.RemoveItem(new Item { itemType = Item.ItemType.PirateHat });
        }
        ui_inventory.RefreshInventoryItems();
        UpdateHatButtonStates();
    }

    ////////////////////////////////////// Open and close Menus /////////////////////////////////////////////////////////////////////
    public void Open(GameObject menu)
    {
        player.GetComponent<FirstPersonController>().enabled = false;
        menu.SetActive(true);
        crosshair.SetActive(false);
        miniMapCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Close(GameObject menu)
    {
        player.GetComponent<FirstPersonController>().enabled = true;
        menu.SetActive(false);
        crosshair.SetActive(true);
        miniMapCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenTree()
    {
        treeText.text = ("Ah, hello young one... How may I help you?");
        isTreeMenuOpen = true;
        Open(ladTreeMenu);
    }

    public void CloseTree()
    {
        isTreeMenuOpen = false;
        Close(ladTreeMenu);
    }

    public void OpenHat()
    {
        hatText.text = ("Hello, friend! Would you like to buy a hat or two?");
        isHatMenuOpen = true;
        Open(hatShop);
    }

    public void CloseHat()
    {
        isHatMenuOpen = false;
        Close(hatShop);
    }

    public void OpenLab()
    {
        isLabMenuOpen = true;

        // You need 1 ladFruits to combine
        if (numOfLadFruit < 1)
        {
            errorPanel.SetActive(true);
            combineButton.interactable = false;
            if (!ladList.canCombine)
            {
                combineButton.interactable = false;
            }
        }
        else if (numOfLadFruit >= 1)
        {
            errorPanel.SetActive(false);
            combineButton.interactable = false;
            if (ladList.canCombine)
            {
                combineButton.interactable = true;
            }
        }

        Open(labMenu);
    }

    public void CloseLab()
    {
        Close(labMenu);
        errorPanel.SetActive(false);
        isLabMenuOpen = false;
        Image image1 = lad1Button.GetComponent<Image>();
        image1.sprite = ItemAssets.Instance.ladButtonDefault;
        lad1Name.SetText("");
        ladList.lad1Picked = false;
        ladList.lad1Icon.SetActive(false);

        Image image2 = lad2Button.GetComponent<Image>();
        image2.sprite = ItemAssets.Instance.ladButtonDefault;
        lad2Name.SetText("");
        ladList.lad2Picked = false;
        ladList.lad2Icon.SetActive(false);
        ladList.canCombine = false;

        ladList.errorMessage.text = "You need 1 Lad Fruit to combine Lads!";
    }

    public void OpenLabCreationMenu()
    {
        input.SetActive(true);
        createButtonGameObject.SetActive(true);
        Open(labCreationMenu);

        newLad = ladMixing.createNewLadColors(ladList.lad1, ladList.lad2);
        newLad.transform.SetParent(ladParent);
        newLad.tag = "Lad";
        DateTime birthday = DateTime.Now;
        newLad.transform.GetComponent<LadGenes>().SetBirthday(birthday);

        birthdayText.text = (birthday.ToString("d MMM yyyy"));
        creativityText.text = ("Creativity \n" + newLad.transform.GetComponent<LadGenes>().GetCreativity().ToString());
        intelligenceText.text = ("Intelligence \n" + newLad.transform.GetComponent<LadGenes>().GetIntelligence().ToString());
        strengthText.text = ("Strength \n" + newLad.transform.GetComponent<LadGenes>().GetStrength().ToString());
        adaptabilityText.text = ("Adaptability \n" + newLad.transform.GetComponent<LadGenes>().GetAdaptability().ToString());
        speedText.text = ("Speed \n" + newLad.transform.GetComponent<LadGenes>().GetSpeed().ToString());

        // set the colors for the model of the new lad if the lad is regular
        if (newLad.transform.GetComponent<LadGenes>().GetKind() == "regular")
        {
            newRegularLadIcon.SetActive(true);
            newThomiscianLadIcon.SetActive(false);
            newRegularLadIcon.transform.GetComponent<LadGenes>().setBodyColor(newLad.transform.GetComponent<LadGenes>().getBodyColor());
            newRegularLadIcon.transform.GetComponent<LadGenes>().setAccentColor(newLad.transform.GetComponent<LadGenes>().getAccentColor());

            for (int i = 1; i < newRegularLadIcon.transform.childCount; i++)
            {
                GameObject child = newRegularLadIcon.transform.GetChild(i).gameObject;
                ChangeColor childScript = child.GetComponentInChildren<ChangeColor>();
                childScript.setBodyColor(newLad.transform.GetComponent<LadGenes>().getBodyColor());
                childScript.setAccentColor(newLad.transform.GetComponent<LadGenes>().getAccentColor());
            }
        }
        else
        {
            newThomiscianLadIcon.SetActive(true);
            newRegularLadIcon.SetActive(false);
            newThomiscianLadIcon.transform.GetComponent<LadGenes>().setBodyColor(newLad.transform.GetComponent<LadGenes>().getBodyColor());
            newThomiscianLadIcon.transform.GetComponent<LadGenes>().setAccentColor(newLad.transform.GetComponent<LadGenes>().getAccentColor());

            for (int i = 1; i < newThomiscianLadIcon.transform.childCount; i++)
            {
                GameObject child = newThomiscianLadIcon.transform.GetChild(i).gameObject;
                ChangeColor childScript = child.GetComponentInChildren<ChangeColor>();
                childScript.setBodyColor(newLad.transform.GetComponent<LadGenes>().getBodyColor());
                childScript.setAccentColor(newLad.transform.GetComponent<LadGenes>().getAccentColor());
            }
        }

        isLabCreationMenuOpen = true;
        crosshair.SetActive(false);
    }

    public void CloseLabCreationMenu()
    {
        newRegularLadIcon.SetActive(false);
        newThomiscianLadIcon.SetActive(false);
        Close(labCreationMenu);
        isLabCreationMenuOpen = false;
        crosshair.SetActive(true);
    }

    public void OpenLadDetail(GameObject lad)
    {
        ladObject = lad;
        input.SetActive(false);
        crosshair.SetActive(false);
        createButtonGameObject.SetActive(false);
        closeLadDetailButtonObject.SetActive(true);
        ladNameText.SetText(lad.name);
        assignLadJob.SetActive(true);
        farmerButton.interactable = true;
        joblessButton.interactable = true;
        scientistButton.interactable = true;
        CloseLadInventory();
        Open(labCreationMenu);

        birthdayText.text = (lad.transform.GetComponent<LadGenes>().GetBirthday().ToString("d MMM yyyy"));
        creativityText.text = ("Creativity \n" + lad.transform.GetComponent<LadGenes>().GetCreativity().ToString());
        intelligenceText.text = ("Intelligence \n" + lad.transform.GetComponent<LadGenes>().GetIntelligence().ToString());
        strengthText.text = ("Strength \n" + lad.transform.GetComponent<LadGenes>().GetStrength().ToString());
        adaptabilityText.text = ("Adaptability \n" + lad.transform.GetComponent<LadGenes>().GetAdaptability().ToString());
        speedText.text = ("Speed \n" + lad.transform.GetComponent<LadGenes>().GetSpeed().ToString());

        // set the colors for the model of the lad if the lad is regular
        if (lad.transform.GetComponent<LadGenes>().GetKind() == "regular")
        {
            newRegularLadIcon.SetActive(true);
            newThomiscianLadIcon.SetActive(false);
            newRegularLadIcon.transform.GetComponent<LadGenes>().setBodyColor(lad.transform.GetComponent<LadGenes>().getBodyColor());
            newRegularLadIcon.transform.GetComponent<LadGenes>().setAccentColor(lad.transform.GetComponent<LadGenes>().getAccentColor());

            string newHat;
            newHat = newRegularLadIcon.transform.GetComponent<LadGenes>().GetHat();
            if (newHat == "top")
            {
                topHatObject.SetActive(true);
                partyHatObject.SetActive(false);
                pirateHatObject.SetActive(false);
            }
            else if (newHat == "party")
            {
                topHatObject.SetActive(false);
                partyHatObject.SetActive(true);
                pirateHatObject.SetActive(false);
            }
            else if (newHat == "pirate")
            {
                topHatObject.SetActive(false);
                partyHatObject.SetActive(false);
                pirateHatObject.SetActive(true);
            }


            for (int i = 1; i < newRegularLadIcon.transform.childCount; i++)
            {
                GameObject child = newRegularLadIcon.transform.GetChild(i).gameObject;
                ChangeColor childScript = child.GetComponentInChildren<ChangeColor>();
                if (childScript != null)
                {
                childScript.setBodyColor(lad.transform.GetComponent<LadGenes>().getBodyColor());
                childScript.setAccentColor(lad.transform.GetComponent<LadGenes>().getAccentColor());
                }
            }
        }
        else
        {
            newThomiscianLadIcon.SetActive(true);
            newRegularLadIcon.SetActive(false);
            newThomiscianLadIcon.transform.GetComponent<LadGenes>().setBodyColor(lad.transform.GetComponent<LadGenes>().getBodyColor());
            newThomiscianLadIcon.transform.GetComponent<LadGenes>().setAccentColor(lad.transform.GetComponent<LadGenes>().getAccentColor());

            string newHat2;
            newHat2 = newThomiscianLadIcon.transform.GetComponent<LadGenes>().GetHat();
            if (newHat2 == "top")
            {
                topHatObject2.SetActive(true);
                partyHatObject2.SetActive(false);
                pirateHatObject2.SetActive(false);
            }
            else if (newHat2 == "party")
            {
                topHatObject2.SetActive(false);
                partyHatObject2.SetActive(true);
                pirateHatObject2.SetActive(false);
            }
            else if (newHat2 == "pirate")
            {
                topHatObject2.SetActive(false);
                partyHatObject2.SetActive(false);
                pirateHatObject2.SetActive(true);
            }

            for (int i = 1; i < newThomiscianLadIcon.transform.childCount; i++)
            {
                GameObject child = newThomiscianLadIcon.transform.GetChild(i).gameObject;
                ChangeColor childScript = child.GetComponentInChildren<ChangeColor>();
                childScript.setBodyColor(lad.transform.GetComponent<LadGenes>().getBodyColor());
                childScript.setAccentColor(lad.transform.GetComponent<LadGenes>().getAccentColor());
            }
        }

        // shows what the current job of the lad is.
        string job = lad.transform.GetComponent<LadGenes>().GetJob();
        if (job == "farmer")
        {
            farmerButton.interactable = false;
            joblessButton.interactable = true;
            scientistButton.interactable = true;
        }
        else if (job == "jobless")
        {
            joblessButton.interactable = false;
            farmerButton.interactable = true;
            scientistButton.interactable = true;
        }
        else if (job == "scientist")
        {
            scientistButton.interactable = false;
            farmerButton.interactable = true;
            joblessButton.interactable = true;
        }
        else
        {
            farmerButton.interactable = true;
            joblessButton.interactable = true;
            scientistButton.interactable = true;
        }

        // counts the number of hats.
        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemType == Item.ItemType.TopHat)
            {
                topHatNum = item.amount;
            }
            else if (item.itemType == Item.ItemType.PartyHat)
            {
                partyHatNum = item.amount;
            }
            else if (item.itemType == Item.ItemType.PirateHat)
            {
                pirateHatNum = item.amount;
            }
        }
        isLadDetailOpen = true;
    }


    public void CloseLadDetail()
    {
        Close(labCreationMenu);
        closeLadDetailButtonObject.SetActive(false);
        ladNameText.SetText("");
        assignLadJob.SetActive(false);
        isLadDetailOpen = false;
        crosshair.SetActive(true);

        // if (ladObject.transform.GetComponent<LadGenes>().GetHat() == "top")
        // {
        //     Debug.Log("top");
        //     inventory.RemoveItem(new Item { itemType = Item.ItemType.TopHat});
        //     ui_inventory.RefreshInventoryItems();
        // }
        // else if (ladObject.transform.GetComponent<LadGenes>().GetHat() == "party")
        // {
        //     inventory.RemoveItem(new Item { itemType = Item.ItemType.PartyHat});
        //     ui_inventory.RefreshInventoryItems();
        // }
        // else if (ladObject.transform.GetComponent<LadGenes>().GetHat() == "pirate")
        // {
        //     inventory.RemoveItem(new Item { itemType = Item.ItemType.PirateHat});
        //     ui_inventory.RefreshInventoryItems();
        // }
    }

    public void OpenInventory()
    {
        Open(InventoryMenu);
        isInventoryOpen = true;
        crosshair.SetActive(false);
    }

    public void CloseInventory()
    {
        if (isLabMenuOpen)
        {
            player.GetComponent<FirstPersonController>().enabled = true;
            InventoryMenu.SetActive(false);
        }
        else
        {
            Close(InventoryMenu);
        }
        isInventoryOpen = false;
        crosshair.SetActive(true);
    }

    public void OpenLadInventory()
    {
        ladList = FindObjectOfType<LadInventory>();
        ladList.RefreshLadInventoryItems();
        Open(ladInventory);
        isLadInventoryOpen = true;
        isInventoryOpen = true;
        ladList.lad2Icon.SetActive(false);
        crosshair.SetActive(false);
    }

    public void CloseLadInventory()
    {
        if (isLabMenuOpen)
        {
            player.GetComponent<FirstPersonController>().enabled = true;
            ladInventory.SetActive(false);

            if (ladList.lad2Picked)
            {
                ladList.lad2Icon.SetActive(true);
            }
        }
        else
        {
            Close(ladInventory);
        }
        isLadInventoryOpen = false;
        crosshair.SetActive(true);
    }
}
