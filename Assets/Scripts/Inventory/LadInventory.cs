using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using SaveLoadSystem;
using System.IO;

public class LadInventory : MonoBehaviour
{
    [HideInInspector] public List<GameObject> ladList;
    public List<GameObject> parentLadList;

    [SerializeField] Transform ladSlotContainer;
    [SerializeField] Transform ladSlotTemplate;
    [SerializeField] public bool canCombine = false;

    [Header("Lab Menu Icons")]
    [SerializeField] public GameObject lad1Icon;
    [SerializeField] public GameObject lad2Icon;

    [Header("Lad Parts 1")]
    [SerializeField] Renderer headRenderer1;
    [SerializeField] Renderer rightEyeRenderer1;
    [SerializeField] Renderer leftEyeRenderer1;
    [SerializeField] Renderer noseRenderer1;

    [Header("Lad Parts 2")]
    [SerializeField] Renderer headRenderer2;
    [SerializeField] Renderer rightEyeRenderer2;
    [SerializeField] Renderer leftEyeRenderer2;
    [SerializeField] Renderer noseRenderer2;

    [Header("Error")]
    [SerializeField] GameObject errorPanel;
    [SerializeField] public TextMeshProUGUI errorMessage;

    [HideInInspector] public GameObject lad1;
    [HideInInspector] public GameObject lad2;
    public bool lad1Picked = false;
    public bool lad2Picked = false;

    UI ui;
    LadGenes ladGenes;
    SaveLoadNamespace saveSystem;

    private void Start()
    {
        ui = FindObjectOfType<UI>();
        saveSystem = FindObjectOfType<SaveLoadNamespace>();

        lad1Icon.SetActive(false);
        lad2Icon.SetActive(false);

        foreach (GameObject lad in GameObject.FindGameObjectsWithTag("Lad"))
        {
            if (!ladList.Contains(lad))
            {
                ladList.Add(lad);
            }
        }

        foreach (GameObject lad in GameObject.FindGameObjectsWithTag("LadParent"))
        {
            if (!parentLadList.Contains(lad))
            {
                parentLadList.Add(lad);
            }
        }

        // foreach(GameObject lad in parentLadList)
        // {
        //     lad.SetActive(false);
        // }

      
        RefreshLadInventoryItems();
      
    }


    public void RefreshLadInventoryItems()
    {
        // add any new lads
        foreach (GameObject lad in GameObject.FindGameObjectsWithTag("Lad"))
        {
            if (!ladList.Contains(lad))
            {
                ladList.Add(lad);
            }
        }

        foreach (Transform child in ladSlotContainer)
        {
            if (child == ladSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        float x = -2.47f;
        float y = 2.55f;
        float itemSlotCellSize = 16f;

        foreach (GameObject lad in ladList)
        {
            RectTransform ladSlotRectTransform = Instantiate(ladSlotTemplate, ladSlotContainer).GetComponent<RectTransform>();
            ladSlotRectTransform.gameObject.SetActive(true);

            ladSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                // left click
                // lad 1 is picked
                if (ui.isLabMenuOpen)
                {
                    if (ui.lad1ButtonClicked && !ui.lad2ButtonClicked)
                    {
                        lad1 = lad;

                        Image image = ui.lad1Button.GetComponent<Image>();
                        // image.sprite = ItemAssets.Instance.ladSprite;
                        ui.lad1Name.SetText(lad.name);


                        LadGenes ladGenes = lad.GetComponent<LadGenes>();
                        if (ladGenes != null)
                        {
                            headRenderer1.material.SetColor("_Color", ladGenes.bodyColor);
                            rightEyeRenderer1.material.SetColor("_Color", ladGenes.accentColor);
                            leftEyeRenderer1.material.SetColor("_Color", ladGenes.accentColor);
                            noseRenderer1.material.SetColor("_Color", ladGenes.accentColor);
                        }

                        lad1Icon.SetActive(true);
                        ui.CloseLadInventory();
                        lad1Picked = true;
                    }

                    // lad 2 is picked
                    else if (ui.lad2ButtonClicked && !ui.lad1ButtonClicked)
                    {
                        lad2 = lad;

                        Image image = ui.lad2Button.GetComponent<Image>();
                        // image.sprite = ItemAssets.Instance.ladSprite;
                        ui.lad2Name.SetText(lad.name);

                        LadGenes ladGenes = lad.GetComponent<LadGenes>();
                        if (ladGenes != null)
                        {
                            headRenderer2.material.SetColor("_Color", ladGenes.bodyColor);
                            rightEyeRenderer2.material.SetColor("_Color", ladGenes.accentColor);
                            leftEyeRenderer2.material.SetColor("_Color", ladGenes.accentColor);
                            noseRenderer2.material.SetColor("_Color", ladGenes.accentColor);
                        }

                        lad2Icon.SetActive(true);
                        ui.CloseLadInventory();
                        lad2Picked = true;
                    }

                    if (lad1Picked && lad2Picked)
                    {
                        // both lads must be unique
                        if (lad1 != lad2)
                        {
                            canCombine = true;
                            if (ui.numOfLadFruit < 1)
                            {
                                errorPanel.SetActive(true);
                                errorMessage.text = "You need 1 Lad Fruit to combine Lads!";
                                ui.combineButton.interactable = false;
                            }
                            else if (ui.numOfLadFruit >= 1)
                            {
                                errorPanel.SetActive(false);
                                ui.combineButton.interactable = true;
                            }
                        }
                        else if (lad1 == lad2)
                        {
                            canCombine = false;
                            ui.combineButton.interactable = false;
                            errorPanel.SetActive(true);
                            errorMessage.text = "You cannot combine the same lad!";
                        }
                    }
                    else if (!lad1Picked || !lad2Picked)
                    {
                        canCombine = false;
                        if (ui.numOfLadFruit < 1)
                        {
                            errorPanel.SetActive(true);
                            errorMessage.text = "You need 1 Lad Fruit to combine Lads!";
                            ui.combineButton.interactable = false;
                        }
                        ui.combineButton.interactable = false;
                    }
                }
                else if (!ui.isLabMenuOpen)
                {
                    ui.OpenLadDetail(lad);
                }
            };


            ladSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = ladSlotRectTransform.Find("image").GetComponent<Image>();

            Renderer headRenderer = ladSlotRectTransform.Find("Head").GetComponent<Renderer>();
            Renderer rightEyeRenderer = ladSlotRectTransform.Find("RightEye").GetComponent<Renderer>();
            Renderer leftEyeRenderer = ladSlotRectTransform.Find("LeftEye").GetComponent<Renderer>();
            Renderer noseRenderer = ladSlotRectTransform.Find("Shnoz").GetComponent<Renderer>();

            LadGenes ladGenes = lad.GetComponent<LadGenes>();
            if (ladGenes != null)
            {
                headRenderer.material.SetColor("_Color", ladGenes.bodyColor);
                rightEyeRenderer.material.SetColor("_Color", ladGenes.accentColor);
                leftEyeRenderer.material.SetColor("_Color", ladGenes.accentColor);
                noseRenderer.material.SetColor("_Color", ladGenes.accentColor);
            }


            TextMeshProUGUI nameText = ladSlotRectTransform.Find("nameText").GetComponent<TextMeshProUGUI>();
            nameText.SetText(lad.name);

            x++;
            if (x > 3)
            {
                x = -2.47f;
                y = y + -0.85f;
            }
        }
    }
}
