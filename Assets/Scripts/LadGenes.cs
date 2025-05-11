using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using SaveLoadSystem;

public class LadGenes : MonoBehaviour, ISaveable
{
    [SerializeField] GameObject Lad;
    [SerializeField] bool isParent;
    [SerializeField] TextMeshPro ladName;
    [SerializeField] bool regularLad;
    [SerializeField] bool thomiscianLad;
    [SerializeField] string ladTag;

    [Header("Job")]
    [SerializeField] public bool farmer;
    [SerializeField] public bool jobless;
    [SerializeField] public bool scientist;

    [Header("Hats")]
    [SerializeField] bool topHat;
    [SerializeField] bool partyHat;
    [SerializeField] bool pirateHat;
    [SerializeField] GameObject topHatObject;
    [SerializeField] GameObject partyHatObject;
    [SerializeField] GameObject pirateHatObject;

    [SerializeField] DateTime birthday = new DateTime(2022, 11, 25);

    [Header("Lad Parents")]
    [SerializeField] GameObject ladParentA;
    [SerializeField] GameObject ladParentB;
    string ladParentAname;
    string ladParentBname;

    [Header("Colors")]
    [SerializeField] public Color bodyColor;
    [SerializeField] public Color accentColor;
    float bodyR, bodyG, bodyB, bodyA;
    float accentR, accentG, accentB, accentA;

    [Header("Stats")]
    [SerializeField] int creativity;
    [SerializeField] int intelligence;
    [SerializeField] int strength;
    [SerializeField] int adaptability;
    [SerializeField] int speed;

    LadInventory ladList;

    [Header("Farmer Payout")]
    [SerializeField] float JaxTime;
    [SerializeField] private float timer;
    // Inventory inventory;
    Player player;


    void Start()
    {
        player = FindObjectOfType<Player>();
        ladList = FindObjectOfType<LadInventory>();

        timer = 0;
        if (ladName != null)
        {
            ladName.text = Lad.name;
        }

        for (int i = 1; i < Lad.transform.childCount; i++)
        {
            GameObject child = Lad.transform.GetChild(i).gameObject;
            //child.setBodyColor(bodyColor);
            ChangeColor childScript = child.GetComponentInChildren<ChangeColor>();
            if (childScript != null)
            {
                childScript.setBodyColor(bodyColor);
                childScript.setAccentColor(accentColor);
            }
        }

        if (regularLad && gameObject.tag == "Untagged")
        {
            gameObject.tag = "Lad";
        }
        else if (thomiscianLad && gameObject.tag == "Untagged")
        {
            gameObject.tag = "UncollectedLad";
        }
        else if (isParent && gameObject.tag == "Untagged")
        {
            gameObject.tag = "LadParent";
        }
    }

    void Update()
    {
        if (ladName != null)
        {
            ladName.text = Lad.name;
        }

        if (topHat)
        {
            topHatObject.SetActive(true);
            partyHatObject.SetActive(false);
            pirateHatObject.SetActive(false);
            partyHat = false;
            pirateHat = false;
        }
        else if (partyHat)
        {
            topHatObject.SetActive(false);
            partyHatObject.SetActive(true);
            pirateHatObject.SetActive(false);
            topHat = false;
            pirateHat = false;
        }
        else if (pirateHat)
        {
            topHatObject.SetActive(false);
            partyHatObject.SetActive(false);
            pirateHatObject.SetActive(true);
            topHat = false;
            partyHat = false;
        }

        if (scientist == true)
        {
            Lad.GetComponent<LadMovement>().enabled = false;
        }
        else
        {
            Lad.GetComponent<LadMovement>().enabled = true;
            //Farmer Payout
            if (farmer == true)
            {
                JaxTime = (70 - (creativity * 5)) * 1f;
                timer += Time.deltaTime;
                if (timer >= JaxTime)
                {
                    timer = 0f;
                    player.inventory.AddItem(new Item { itemType = Item.ItemType.Jax });
                }
            }
        }
        //setBodyColor(bodyColor);
    }


    public GameObject GetLad()
    {
        return Lad;
    }

    public void setBodyColor(Color color)
    {
        bodyColor = color;
    }

    public void setAccentColor(Color color)
    {
        accentColor = color;
    }

    public Color getBodyColor()
    {
        return bodyColor;
    }

    public Color getAccentColor()
    {
        return accentColor;
    }

    public GameObject getParentA()
    {
        return ladParentA;
    }

    public GameObject getParentB()
    {
        return ladParentB;
    }

    public void setParentA(GameObject lad)
    {
        ladParentA = lad;
    }
    public void setParentB(GameObject lad)
    {
        ladParentB = lad;
    }

    public void SetJob(string job)
    {
        if (job == "farmer")
        {
            farmer = true;
            jobless = false;
            scientist = false;
        }
        else if (job == "scientist")
        {
            scientist = true;
            farmer = false;
            jobless = false;
        }
        else if (job == "jobless")
        {
            scientist = false;
            farmer = false;
            jobless = true;
        }
        else
        {
            farmer = false;
            scientist = false;
            jobless = true;
        }
    }

    public string GetJob()
    {
        if (farmer)
        {
            return "farmer";
        }
        else if (jobless)
        {
            return "jobless";
        }
        else if (scientist)
        {
            return "scientist";
        }
        else
        {
            return null;
        }
    }

    public void SetCreativity(int stat)
    {
        creativity = stat;
    }

    public int GetCreativity()
    {
        return creativity;
    }

    public void SetIntelligence(int stat)
    {
        intelligence = stat;
    }

    public int GetIntelligence()
    {
        return intelligence;
    }

    public void SetStrength(int stat)
    {
        strength = stat;
    }

    public int GetStrength()
    {
        return strength;
    }

    public void SetAdaptability(int stat)
    {
        adaptability = stat;
    }

    public int GetAdaptability()
    {
        return adaptability;
    }

    public void SetSpeed(int stat)
    {
        speed = stat;
    }

    public int GetSpeed()
    {
        return speed;
    }

    public void SetBirthday(DateTime time)
    {
        birthday = time;
    }

    public DateTime GetBirthday()
    {
        return birthday;
    }

    public void SetRegularLad(bool regular)
    {
        regularLad = regular;
    }

    public void SetThomas(bool thomas)
    {
        thomiscianLad = thomas;
    }

    public string GetKind()
    {
        if (regularLad)
        {
            return "regular";
        }
        else
        {
            return "thomiscian";
        }
    }

    public string GetName()
    {
        return Lad.name;
    }

    void SetTag(string loadLadTag)
    {
        gameObject.tag = loadLadTag;
    }

    void SetIsParent(bool parent)
    {
        isParent = parent;
    }

    public void setHat(string hat)
    {
        if (hat == "pirate")
        {
            topHat = false;
            partyHat = false;
            pirateHat = true;
        }
        else if (hat == "top")
        {
            topHat = true;
            partyHat = false;
            pirateHat = false;
        }
        else if (hat == "party")
        {
            topHat = false;
            partyHat = true;
            pirateHat = false;
        }
    }

    public string GetHat()
    {
        if (topHat)
        {
            return "top";
        }
        else if (partyHat)
        {
            return "party";
        }
        else if (pirateHat)
        {
            return "pirate;";
        }
        else
        {
            return null;
        }
    }


    // Methods to save the lads.
    [System.Serializable]
    struct LadData
    {
        // public GameObject Lad;
        public bool isParent;
        public bool regularLad;
        public bool thomiscianLad;
        public string ladTag;
        public bool farmer;
        public bool jobless;
        public bool scientist;
        public bool topHat;
        public bool partyHat;
        public bool pirateHat;
        public DateTime birthday;
        public string ladParentAname;
        public string ladParentBname;
        public float bodyR, bodyG, bodyB, bodyA;
        public float accentR, accentG, accentB, accentA;
        public int creativity;
        public int intelligence;
        public int strength;
        public int adaptability;
        public int speed;
        public float JaxTime;
    }

    public object SaveState()
    {
        // Debug.Log(this.tag);
        return new LadData()
        {
            // Lad = Lad,
            isParent = isParent,
            regularLad = regularLad,
            thomiscianLad = thomiscianLad,
            ladTag = this.tag,
            farmer = farmer,
            jobless = jobless,
            scientist = scientist,
            birthday = birthday,
            topHat = topHat,
            partyHat = partyHat,
            pirateHat = pirateHat,
            ladParentAname = ladParentA.name,
            ladParentBname = ladParentB.name,
            bodyR = bodyColor.r,
            bodyG = bodyColor.g,
            bodyB = bodyColor.b,
            bodyA = bodyColor.a,
            accentR = accentColor.r,
            accentG = accentColor.g,
            accentB = accentColor.b,
            accentA = accentColor.a,
            creativity = creativity,
            intelligence = intelligence,
            strength = strength,
            adaptability = adaptability,
            speed = speed,
            JaxTime = JaxTime
        };
    }

    public void LoadState(object state)
    {
        LadData data = (LadData)state;
        // Lad = data.Lad;
        isParent = data.isParent;
        regularLad = data.regularLad;
        thomiscianLad = data.thomiscianLad;
        ladTag = data.ladTag;
        farmer = data.farmer;
        jobless = data.jobless;
        scientist = data.scientist;
        birthday = data.birthday;
        topHat = data.topHat;
        partyHat = data.partyHat;
        pirateHat = data.pirateHat;
        ladParentAname = data.ladParentAname;
        ladParentBname = data.ladParentBname;
        bodyR = data.bodyR;
        bodyG = data.bodyG;
        bodyB = data.bodyB;
        bodyA = data.bodyA;
        accentA = data.accentA;
        accentR = data.accentR;
        accentG = data.accentG;
        accentB = data.accentB;
        creativity = data.creativity;
        intelligence = data.intelligence;
        strength = data.strength;
        adaptability = data.adaptability;
        speed = data.speed;
        JaxTime = data.JaxTime;
        // Debug.Log(isParent);

        // Debug.Log(ladTag);
        // setParentA(GameObject.Find(ladParentAname));
        // setParentB(GameObject.Find(ladParentBname));


        // setBodyColor(new Color(bodyR, bodyG, bodyB, bodyA));
        // setAccentColor(new Color(accentR, accentG, accentB, accentA));
    }

    public bool NeedsToBeSaved()
    {
        return true;
    }

    public bool NeedsReinstantiation()
    {
        return true;
    }

    public void PostInstantiation(object state)
    {
        LadData data = (LadData)state;

        setParentA(GameObject.Find(ladParentAname));
        setParentB(GameObject.Find(ladParentBname));

        setBodyColor(new Color(bodyR, bodyG, bodyB, bodyA));
        setAccentColor(new Color(accentR, accentG, accentB, accentA));

        SetTag(ladTag);
        SetIsParent(isParent);
        // SetHat();

        LadInventory loadLads;
        loadLads = FindObjectOfType<LadInventory>();

        loadLads.ladList.Clear();
        loadLads.parentLadList.Clear();

        foreach (GameObject lad in GameObject.FindGameObjectsWithTag("Lad"))
        {
            if (!loadLads.ladList.Contains(lad))
            {
                loadLads.ladList.Add(lad);
            }
        }

        foreach (GameObject lad in GameObject.FindGameObjectsWithTag("LadParent"))
        {
            if (!loadLads.parentLadList.Contains(lad))
            {
                loadLads.parentLadList.Add(lad);
            }
        }
        loadLads.RefreshLadInventoryItems();
    }

    public void GotAddedAsChild(GameObject obj, GameObject hisParent)
    {
    }
}