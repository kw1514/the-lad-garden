using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveLoadSystem;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;
using System.IO;

public class SaveLoadUI : MonoBehaviour, ISaveable
{
    [SerializeField] GameObject loadSaveScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] bool isLoadSaveScreen;
    [SerializeField] bool delete;
    [SerializeField] GameObject container;

    [Header("Buttons")]
    [SerializeField] Button loadButton;
    [SerializeField] GameObject loadButtonObject;
    [SerializeField] Button newButton;
    [SerializeField] GameObject newButtonObject;
    [SerializeField] Button createProfileButton;
    [SerializeField] GameObject createProfileObject;
    [SerializeField] GameObject backButtonObject;
    [SerializeField] GameObject deleteButtonObject;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI deleteText;
    [SerializeField] TextMeshProUGUI errorText;

    [Header("Input Field")]
    [SerializeField] InputField profileNameInput;
    [SerializeField] GameObject profileNameObject;

    [Header("RectTransforms")]
    [SerializeField] Transform slotContainer;
    [SerializeField] Transform slotTemplate;

    public List<string> profiles;

    UI ui;
    SaveLoadNamespace saveSystem;
    Player player;
    LadInventory lads;

    void Start()
    {
        profiles.Add("");
        profiles.Add("");
        profiles.Add("");
        profiles.Add("");

        saveSystem = FindObjectOfType<SaveLoadNamespace>();
        saveSystem.SetSavePath("SavesFolder/");
        saveSystem.SetSaveName("profiles.save");
        saveSystem.Load();
        saveSystem.SetSavePath("SavesFolder/");
        saveSystem.SetSaveName("initialState.save");


        if (!File.Exists(saveSystem.GetFullSavePath()))
        {
            Debug.Log("started new file");
            saveSystem.SaveNew();
        }
        else
        {
            Debug.Log("saved to existing file");
            saveSystem.Save();
        }
        saveSystem.Load();

        ui = FindObjectOfType<UI>();
        player = FindObjectOfType<Player>();
        lads = FindObjectOfType<LadInventory>();

        loadButtonObject.SetActive(true);
        newButtonObject.SetActive(true);
        isLoadSaveScreen = false;
        profileNameObject.SetActive(false);
        errorText.SetText("");
        delete = false;

        int index = 0;
        foreach (string profile in profiles)
        {
            if (profile != "")
            {
                index++;
            }

            if (index >= 4)
            {
                createProfileButton.interactable = false;
                errorText.SetText("The maxium (4) number of accounts have been created. " +
                                 "Please load an account or delete one to create a new account.");

                // ui.Open(loadSaveScreen);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ui.Open(pauseScreen);
        }

        if (profileNameInput.text == "")
        {
            createProfileButton.interactable = false;
        }
        else
        {
            createProfileButton.interactable = true;
        }
    }


    public void LoadButtonOnClick()
    {
        saveSystem.SetSavePath("SavesFolder/");
        saveSystem.SetSaveName("profiles.save");
        saveSystem.Load();

        container.SetActive(true);
        loadButtonObject.SetActive(false);
        newButtonObject.SetActive(false);
        backButtonObject.SetActive(true);
        deleteButtonObject.SetActive(true);
        errorText.SetText("");

        ui.Open(loadSaveScreen);
        DisplayProfiles();
        isLoadSaveScreen = true;
    }

    public void NewButtonOnClick()
    {
        loadButtonObject.SetActive(false);
        newButtonObject.SetActive(false);
        backButtonObject.SetActive(true);
        ui.Open(loadSaveScreen);
        profileNameObject.SetActive(true);
        createProfileObject.SetActive(true);
        // DisplayProfiles();
        isLoadSaveScreen = true;
    }

    public void CreateProfileButtonOnClick()
    {
        // the parent lads need to be active when being loaded
        foreach (GameObject lad in lads.parentLadList)
        {
            lad.SetActive(true);
        }

        saveSystem.SetSavePath("SavesFolder/");
        saveSystem.SetSaveName("profiles.save");
        saveSystem.Load();

        // they are loaded so they are turned off again
        foreach (GameObject lad in lads.parentLadList)
        {
            lad.SetActive(false);
        }

        saveSystem.SetSavePath("SavesFolder/");
        string fileName = profileNameInput.text;
        saveSystem.SetSaveName(fileName + ".save");

        int index = 0;
        foreach (string profile in profiles)
        {

            if (profile == "")
            {
                profiles[index] = fileName;
                break;
            }
            index++;
            Debug.Log(index);
        }



        ui.Close(loadSaveScreen);
        ui.miniMapCanvas.SetActive(true);
        player.startupInfo.SetActive(false);
        player.startupInfoOpen = false;
        backButtonObject.SetActive(false);

        saveSystem.SaveNew();

        foreach (string profile in profiles)
        {
            saveSystem.SetSavePath("SavesFolder/");
            saveSystem.SetSaveName("profiles.save");
            if (!File.Exists(saveSystem.GetFullSavePath()))
            {
                Debug.Log("started new file");
                saveSystem.SaveNew();
            }
            else
            {
                Debug.Log("saved to existing file");
                saveSystem.Save();
            }
        }

        saveSystem.SetSavePath("SavesFolder/");
        saveSystem.SetSaveName(fileName + ".save");
    }

    public void BackButtonOnClick()
    {
        ui.Close(loadSaveScreen);
        player.GetComponent<FirstPersonController>().enabled = false;
        player.startupInfo.SetActive(true);
        player.startupInfoOpen = true;

        profileNameObject.SetActive(false);
        createProfileObject.SetActive(false);
        container.SetActive(false);
        deleteButtonObject.SetActive(false);

        newButtonObject.SetActive(true);
        loadButtonObject.SetActive(true);
    }

    public void DeleteButtonOnClick()
    {
        deleteText.SetText("Pick Account");
        delete = true;
    }

    public void SaveButtonOnCLick()
    {
        // the parent lads need to be active to be saved
        foreach (GameObject lad in lads.parentLadList)
        {
            lad.SetActive(true);
        }

        saveSystem.Save();

        // done saving so turned off
        foreach (GameObject lad in lads.parentLadList)
        {
            lad.SetActive(false);
        }
        ui.Close(pauseScreen);
    }

    public void ResumeButtonOnClick()
    {
        ui.Close(pauseScreen);
    }

    public void QuitButtonOnClick()
    {
        // will go to title screen
    }





    public void DisplayProfiles()
    {
        foreach (Transform child in slotContainer)
        {
            if (child == slotTemplate) continue;
            Destroy(child.gameObject);
        }

        float x = 0f;
        float y = -1f;
        float slotCellSize = 150f;


        foreach (string file in profiles)
        {
            if (file != "")
            {
                RectTransform slotRectTransform = Instantiate(slotTemplate, slotContainer).GetComponent<RectTransform>();
                slotRectTransform.gameObject.SetActive(true);


                slotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
                {
                    saveSystem.SetSavePath("SavesFolder/");
                    saveSystem.SetSaveName(file + ".save");

                    if (!delete)
                    {
                        saveSystem.Load();

                        foreach (GameObject lad in lads.parentLadList)
                        {
                            lad.SetActive(false);
                        }

                        ui.Close(loadSaveScreen);
                        ui.miniMapCanvas.SetActive(true);

                        player.startupInfo.SetActive(false);
                        player.startupInfoOpen = false;
                    }
                    else if (delete)
                    {
                        saveSystem.Delete();
                        delete = false;
                        deleteText.SetText("Delete Account");

                        saveSystem.SetSavePath("SavesFolder/");
                        saveSystem.SetSaveName("profiles.save");
                        profiles.Remove(file);
                        profiles.Add("");
                        if (!File.Exists(saveSystem.GetFullSavePath()))
                        {
                            Debug.Log("started new file");
                            saveSystem.SaveNew();
                        }
                        else
                        {
                            Debug.Log("saved to existing file");
                            saveSystem.Save();
                        }


                        DisplayProfiles();
                    }
                };

                slotRectTransform.anchoredPosition = new Vector2(x * slotCellSize, y * slotCellSize);

                TextMeshProUGUI profileName = slotRectTransform.Find("slotName").GetComponent<TextMeshProUGUI>();
                profileName.SetText(file);

                y++;
                if (y > 5f)
                {
                    y = y + 100f;
                }
            }
        }
    }

    [System.Serializable]
    struct SaveLoadData
    {
        public string profile1;
        public string profile2;
        public string profile3;
        public string profile4;
        public int listIndex;
    }


    public object SaveState()
    {
        return new SaveLoadData()
        {
            profile1 = profiles[0],
            profile2 = profiles[1],
            profile3 = profiles[2],
            profile4 = profiles[3]
        };
    }

    public void LoadState(object state)
    {
        SaveLoadData data = (SaveLoadData)state;
        profiles[0] = data.profile1;
        profiles[1] = data.profile2;
        profiles[2] = data.profile3;
        profiles[3] = data.profile4;
    }

    public bool NeedsToBeSaved()
    {
        return true;
    }

    public bool NeedsReinstantiation()
    {
        return false;
    }

    public void PostInstantiation(object state)
    {
        SaveLoadData data = (SaveLoadData)state;
    }

    public void GotAddedAsChild(GameObject obj, GameObject hisParent)
    {
    }
}
