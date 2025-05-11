using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadMixing : MonoBehaviour
{

    // [SerializeField] GameObject Lad1;
    // [SerializeField] GameObject Lad2;
    [SerializeField] GameObject regularLadPrefab;
    [SerializeField] GameObject thomiscianLadPrefab;
    GameObject newLad;

    // 1 is a regular lad, 2 is a thomiscian
    int type;


    public GameObject createNewLadColors(GameObject Lad1, GameObject Lad2)
    {
        //will get values from two parent lads, and the four parent lads of them.
        float H1, S1, V1,
              H2, S2, V2,
              H3, S3, V3,
              H4, S4, V4,
              H5, S5, V5,
              H6, S6, V6,
              H7, S7, V7,
              H8, S8, V8,
              H9, S9, V9,
              H10, S10, V10,
              H11, S11, V11,
              H12, S12, V12;

        Color g1B = Lad1.transform.GetComponent<LadGenes>().getBodyColor();
        Color g1A = Lad1.transform.GetComponent<LadGenes>().getAccentColor();

        Color g1PAB = Lad1.transform.GetComponent<LadGenes>().getParentA().transform.GetComponent<LadGenes>().getBodyColor();
        Color g1PAA = Lad1.transform.GetComponent<LadGenes>().getParentA().transform.GetComponent<LadGenes>().getAccentColor();

        Color g1PBB = Lad1.transform.GetComponent<LadGenes>().getParentA().transform.GetComponent<LadGenes>().getBodyColor();
        Color g1PBA = Lad1.transform.GetComponent<LadGenes>().getParentA().transform.GetComponent<LadGenes>().getAccentColor();




        Color g2B = Lad2.transform.GetComponent<LadGenes>().getBodyColor();
        Color g2A = Lad2.transform.GetComponent<LadGenes>().getAccentColor();

        Color g2PAB = Lad2.transform.GetComponent<LadGenes>().getParentA().transform.GetComponent<LadGenes>().getBodyColor();
        Color g2PAA = Lad2.transform.GetComponent<LadGenes>().getParentA().transform.GetComponent<LadGenes>().getAccentColor();

        Color g2PBB = Lad2.transform.GetComponent<LadGenes>().getParentA().transform.GetComponent<LadGenes>().getBodyColor();
        Color g2PBA = Lad2.transform.GetComponent<LadGenes>().getParentA().transform.GetComponent<LadGenes>().getAccentColor();

        //Body and accent for the first lad
        Color.RGBToHSV(g1B, out H1, out S1, out V1);
        Color.RGBToHSV(g1A, out H2, out S2, out V2);
        //Body and accent for parent a of lad 1
        Color.RGBToHSV(g1PAB, out H3, out S3, out V3);
        Color.RGBToHSV(g1PAA, out H4, out S4, out V4);
        //Body and accent for parent b of lad 1
        Color.RGBToHSV(g1PBB, out H5, out S5, out V5);
        Color.RGBToHSV(g1PBA, out H6, out S6, out V6);

        //Stats for the first lad
        int creativity1 = Lad1.transform.GetComponent<LadGenes>().GetCreativity();
        int intelligence1 = Lad1.transform.GetComponent<LadGenes>().GetIntelligence();
        int strength1 = Lad1.transform.GetComponent<LadGenes>().GetStrength();
        int adaptability1 = Lad1.transform.GetComponent<LadGenes>().GetAdaptability();
        int speed1 = Lad1.transform.GetComponent<LadGenes>().GetSpeed();

        //Body and accent for the second lad
        Color.RGBToHSV(g2B, out H7, out S7, out V7);
        Color.RGBToHSV(g2A, out H8, out S8, out V8);
        //Body and accent for the parent a of the second lad
        Color.RGBToHSV(g2PAB, out H9, out S9, out V9);
        Color.RGBToHSV(g2PAA, out H10, out S10, out V10);
        //Body and accent for the parent b of the second lad.
        Color.RGBToHSV(g2PBB, out H11, out S11, out V11);
        Color.RGBToHSV(g2PBA, out H12, out S12, out V12);

        //Stats for the second lad
        int creativity2 = Lad2.transform.GetComponent<LadGenes>().GetCreativity();
        int intelligence2 = Lad2.transform.GetComponent<LadGenes>().GetIntelligence();
        int strength2 = Lad2.transform.GetComponent<LadGenes>().GetStrength();
        int adaptability2 = Lad2.transform.GetComponent<LadGenes>().GetAdaptability();
        int speed2 = Lad2.transform.GetComponent<LadGenes>().GetSpeed();

        //Color values for the body of lad 1 and its parents
        float[,] valuesAB = new float[,] { { H1, S1, V1 }, { H3, S3, V3 }, { H5, S5, V5 } };
        //Color values for the accent of lad 1 and its parents
        float[,] valuesAA = new float[,] { { H2, S2, V2 }, { H4, S4, V4 }, { H6, S6, V6 } };

        //Color values for the body of lad 2 and its parents
        float[,] valuesBB = new float[,] { { H7, S7, V7 }, { H9, S9, V9 }, { H11, S11, V11 } };
        //Color values for the accent of lad 1 and its parents
        float[,] valuesBA = new float[,] { { H8, S8, V8 }, { H10, S10, V10 }, { H12, S12, V12 } };

        int ladBodyChoice1 = Random.Range(0, 2);
        int ladAccentChoice1 = Random.Range(0, 2);
        int ladBodyChoice2 = Random.Range(0, 2);
        int ladAccentChoice2 = Random.Range(0, 2);

        float[] bodyColorChild1 = new float[] { valuesAB[ladBodyChoice1, 0], valuesAB[ladBodyChoice1, 1], valuesAB[ladBodyChoice1, 2] };
        float[] bodyAccentChild1 = new float[] { valuesAB[ladAccentChoice1, 0], valuesAB[ladAccentChoice1, 1], valuesAB[ladAccentChoice1, 2] };

        float[] bodyColorChild2 = new float[] { valuesAB[ladBodyChoice2, 0], valuesAB[ladBodyChoice2, 1], valuesAB[ladBodyChoice2, 2] };
        float[] bodyAccentChild2 = new float[] { valuesAB[ladAccentChoice2, 0], valuesAB[ladAccentChoice2, 1], valuesAB[ladAccentChoice2, 2] };

        Color finalBodyColor;
        Color finalAccentColor;


        //calculate body color
        if (Mathf.Abs(bodyColorChild1[2] - bodyColorChild2[2]) < 0.33)
        {
            int choice = Random.Range(0, 100);
            if (choice < 33)
            {
                finalBodyColor = Color.HSVToRGB(bodyColorChild1[0], bodyColorChild1[1], bodyColorChild1[2]);
            }
            else if (choice > 33 && choice < 66)
            {
                finalBodyColor = Color.HSVToRGB(bodyColorChild2[0], bodyColorChild2[1], bodyColorChild2[2]);
            }
            else
            {
                finalBodyColor = Color.HSVToRGB(bodyColorChild1[0] / 2, bodyColorChild1[1], bodyColorChild1[2]) + Color.HSVToRGB(bodyColorChild2[0] / 2, bodyColorChild2[1], bodyColorChild2[2]);
            }
        }
        else
        {
            if (bodyColorChild1[2] > bodyColorChild2[2])
            {
                finalBodyColor = Color.HSVToRGB(bodyColorChild1[0], bodyColorChild1[1], bodyColorChild1[2]);
            }
            else
            {
                finalBodyColor = Color.HSVToRGB(bodyColorChild2[0], bodyColorChild2[1], bodyColorChild2[2]);
            }
        }

        //calculate accent color
        if (Mathf.Abs(bodyAccentChild1[2] - bodyAccentChild2[2]) < 0.33)
        {
            int choice = Random.Range(0, 100);
            if (choice < 33)
            {
                finalAccentColor = Color.HSVToRGB(bodyAccentChild1[0], bodyAccentChild1[1], bodyAccentChild1[2]);
            }
            else if (choice > 33 && choice < 66)
            {
                finalAccentColor = Color.HSVToRGB(bodyAccentChild2[0], bodyAccentChild2[1], bodyAccentChild2[2]);
            }
            else
            {
                finalAccentColor = Color.HSVToRGB(bodyAccentChild1[0] / 2, bodyAccentChild1[1], bodyAccentChild1[2]) + Color.HSVToRGB(bodyAccentChild2[0] / 2, bodyAccentChild2[1], bodyAccentChild2[2]);
            }
        }
        else
        {
            if (bodyAccentChild1[2] > bodyAccentChild2[2])
            {
                finalAccentColor = Color.HSVToRGB(bodyAccentChild1[0], bodyAccentChild1[1], bodyAccentChild1[2]);
            }
            else
            {
                finalAccentColor = Color.HSVToRGB(bodyAccentChild2[0], bodyAccentChild2[1], bodyAccentChild2[2]);
            }
        }

        // calculate the stats for the new lad
        int finalCreativity;
        int finalIntelligence;
        int finalStrength;
        int finalAdaptability;
        int finalSpeed;
        int statChoice = Random.Range(0, 2);

        finalCreativity = (creativity1 + creativity2) / 2;
        finalIntelligence = (intelligence1 + intelligence2) / 2;
        finalStrength = (strength1 + strength2) / 2;
        finalAdaptability = (adaptability1 + adaptability2) / 2;
        finalSpeed = (speed1 + speed2) / 2;

        // the random number determines if the new lad gets additional points added or take away.
        if (statChoice == 1)
        {
            finalCreativity += Random.Range(0, 6);
            finalIntelligence += Random.Range(0, 6);
            finalStrength += Random.Range(0, 6);
            finalAdaptability += Random.Range(0, 6);
            finalSpeed -= Random.Range(0, 6);
        }
        else if (statChoice == 0)
        {
            finalCreativity -= Random.Range(0, 6);
            finalIntelligence -= Random.Range(0, 6);
            finalStrength -= Random.Range(0, 6);
            finalAdaptability -= Random.Range(0, 6);
            finalSpeed += Random.Range(0, 6);
        }

        if (finalCreativity < 0)
        {
            finalCreativity = 2;
        }
        else if (finalIntelligence < 0)
        {
            finalIntelligence = 2;
        }
        else if (finalStrength < 0)
        {
            finalStrength = 2;
        }
        else if (finalAdaptability < 0)
        {
            finalAdaptability = 2;
        }
        else if (finalSpeed < 0)
        {
            finalSpeed = 10;
        }


        // gives back the lad
        FirstPersonController player;
        player = FindObjectOfType<FirstPersonController>();

        // figuring out the type of lad to be created.
        int random = Random.Range(1, 101);
        string lad1Type = Lad1.transform.GetComponent<LadGenes>().GetKind();
        string lad2Type = Lad1.transform.GetComponent<LadGenes>().GetKind();

        if (lad1Type == "regular" && lad2Type == "regular")
        {
            type = 1;
        }
        else if (lad1Type == "thomiscian" && lad2Type == "thomiscian")
        {
            if (random > 90)
            {
                type = 1;
            }
            else if (random <= 90)
            {
                type = 2;
            }
        }
        else if (lad1Type == "thomiscian" || lad2Type == "thomiscian")
        {
            if (random > 25)
            {
                type = 1;
            }
            else if (random <= 25)
            {
                type = 2;
            }
        }

        if (type == 1)
        {
            newLad = regularLadPrefab;
        }
        else if (type == 2)
        {
            newLad = thomiscianLadPrefab;
        }

        newLad.transform.GetComponent<LadGenes>().setBodyColor(finalBodyColor);
        newLad.transform.GetComponent<LadGenes>().setAccentColor(finalAccentColor);

        newLad.transform.GetComponent<LadGenes>().SetCreativity(finalCreativity);
        newLad.transform.GetComponent<LadGenes>().SetIntelligence(finalIntelligence);
        newLad.transform.GetComponent<LadGenes>().SetStrength(finalStrength);
        newLad.transform.GetComponent<LadGenes>().SetAdaptability(finalAdaptability);
        newLad.transform.GetComponent<LadGenes>().SetSpeed(finalSpeed);

        newLad.transform.GetComponent<LadGenes>().setParentA(Lad1);
        newLad.transform.GetComponent<LadGenes>().setParentB(Lad2);

        if (type == 1)
        {
            newLad.transform.GetComponent<LadGenes>().SetRegularLad(true);
            newLad.transform.GetComponent<LadGenes>().SetThomas(false);
            newLad = Instantiate(regularLadPrefab, player.transform.position, Quaternion.identity);
        }
        else if (type == 2)
        {
            newLad.transform.GetComponent<LadGenes>().SetThomas(true);
            newLad.transform.GetComponent<LadGenes>().SetRegularLad(false);
            newLad = Instantiate(thomiscianLadPrefab, player.transform.position, Quaternion.identity);
        }

        //I don't know if the start method will begin upon instantiation or on its declaration. So it may not update unless
        //The logic that sets the color is moved to update in the genes script or some other code is set to make it so that
        //the values are set before start occurs in the gene script.
        return newLad;
    }
}
