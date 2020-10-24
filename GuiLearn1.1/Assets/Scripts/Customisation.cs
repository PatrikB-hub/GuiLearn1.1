using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Customisation : MonoBehaviour
{
    public bool showOnGUI = true;
    [SerializeField]
    private Player player;

    [SerializeField]
    private string TextureLocation = "Character/";
    public enum CustomiseParts { Skin, Hair, Mouth, Eyes, Clothes, Armour }

    //defaults for professions
    [SerializeField] PlayerProfession[] playerProfessions;

    //Renderer for our character mesh so we can reference material list within script for changing visuals
    public Renderer characterRenderer;

    // Enum.GetNames(typeof(CustomiseParts)).Length , this part gets the number of customise parts we have (6)
    //an array of List<Texture2D>
    // in other words, 6 lists
    public List<Texture2D>[] partsTexture = new List<Texture2D>[Enum.GetNames(typeof(CustomiseParts)).Length];
    [SerializeField] private int[] currentPartsTextureIndex = new int[Enum.GetNames(typeof(CustomiseParts)).Length];

    public Vector2 scrollPosition = Vector2.zero;

    [SerializeField] private string sceneToPlay = "PlayerControllerNew";

    //first number = which body part
    //second number = which version of that body part
    // partsTexture[0][0] = Skin_0
    // partsTexture[0][1] = Skin_1
    // partsTexture[0][2] = Skin_2
    // partsTexture[0][3] = Skin_3
    // partsTexture[1][0] = Eyes_0
    // partsTexture[1][1] = Eyes_1

    private void Start()
    {
        int partCount = 0;
        foreach (string part in Enum.GetNames(typeof(CustomiseParts)))//loop through our array of parts
        {
            int textureCount = 0;
            Texture2D tempTexture;

            partsTexture[partCount] = new List<Texture2D>();
            do //loop through each texture and add it to our list
            {
                //tempTexture = Resources.Load(TextureLocation + part + "_" + count) as Texture2D;
                tempTexture = (Texture2D)Resources.Load(TextureLocation + part + "_" + textureCount);

                if (tempTexture != null)
                {
                    partsTexture[partCount].Add(tempTexture);
                    //add this texture to a collection;
                }

                textureCount++;
            } while (tempTexture != null);
            partCount++;
        }

        if (player == null)
        {
            Debug.LogError("playerstats in customization");
        }
        else
        {
            if (player.customisationTextureIndex.Length != 0)
            {
                currentPartsTextureIndex = player.customisationTextureIndex;
            }
        }

        if (playerProfessions != null && playerProfessions.Length > 0)
        {
            player.Profession = playerProfessions[0];
        }

        //string[] of each body part = Enum.GetNames(typeof(CustomiseParts))
        // ["Skin", "Hair", "Mouth", "Eyes", "Clothes", "Armour"]
        foreach (string part in Enum.GetNames(typeof(CustomiseParts)))//loop through our array of parts
        {
            SetTexture(part, 0);
        }
    }

    void SetTexture(string type, int direction)
    {
        int partIndex = 0;

        switch (type)
        {
            case "Skin":
                partIndex = 0;
                //set textures
                break;
            case "Hair":
                partIndex = 1;
                break;
            case "Mouth":
                partIndex = 2;
                break;
            case "Eyes":
                partIndex = 3;
                break;
            case "Clothes":
                partIndex = 4;
                break;
            case "Armour":
                partIndex = 5;
                break;
            default:
                Debug.LogError("Invalid set texture type");
                //if its none of the above, then its default
                break;
        }
        int max = partsTexture[partIndex].Count;

        int currentTexture = currentPartsTextureIndex[partIndex];
        currentTexture += direction;
        if (currentTexture < 0)
        {
            currentTexture = max - 1;
        }
        else if (currentTexture > max - 1)
        {
            currentTexture = 0;
        }

        currentPartsTextureIndex[partIndex] = currentTexture;

        Material[] mats = characterRenderer.materials;
        mats[partIndex].mainTexture = partsTexture[partIndex][currentTexture];
        characterRenderer.materials = mats;
    }

    public void SaveCharacter()
    {
        player.customisationTextureIndex = currentPartsTextureIndex;
        PlayerBinarySave.SavePlayerData(player);

        /*
        PlayerPrefs.SetInt("Skin Index", currentPartsTextureIndex[0]);
        PlayerPrefs.SetInt("Hair Index", currentPartsTextureIndex[0]);
        PlayerPrefs.SetInt("Eyes Index", currentPartsTextureIndex[0]);
        PlayerPrefs.SetInt("Mouth Index", currentPartsTextureIndex[0]);
        PlayerPrefs.SetInt("Clothes Index", currentPartsTextureIndex[0]);
        PlayerPrefs.SetInt("Armour Index", currentPartsTextureIndex[0]);

        //do character name on own
        //PlayerPrefs.SetString("Character Name", from wherever character name from );

        //finalstat = defaultStat + additionalStat + levelUpStat
        for (int i = 0; i < player.playerStats.baseStats.Length; i++)
        {
            PlayerPrefs.SetInt(player.playerStats.baseStats[i].baseStatName + " defaultStat",    player.playerStats.baseStats[i].defaultStat);
            PlayerPrefs.SetInt(player.playerStats.baseStats[i].baseStatName + " additionalStat", player.playerStats.baseStats[i].additionalStat);
            PlayerPrefs.SetInt(player.playerStats.baseStats[i].baseStatName + " levelUpStat",    player.playerStats.baseStats[i].levelUpStat);
        }

        PlayerPrefs.SetString("Character Profession", player.Profession.professionName);
        */

    }

    private void OnGUI()
    {
        if (showOnGUI)
        {

            CustomizeOnGUI();

            StatsOnGUI();

            ProfessionsOnGUI();

            if (GUI.Button(new Rect(10, 250, 120, 20), "Save and Play"))
            {
                SaveCharacter();
                SceneManager.LoadScene(sceneToPlay);
            }
        }

    }

    private void ProfessionsOnGUI()
    {
        float currentHeight = 0;

        GUI.Box(new Rect(Screen.width - 170, 230, 155, 80), "Profession");

        scrollPosition = GUI.BeginScrollView(new Rect(Screen.width - 170, 250, 155, 50),
                                             scrollPosition,
                                             new Rect(0, 0, 100, 30 * playerProfessions.Length));

        int i = 0;
        foreach (PlayerProfession profession in playerProfessions)
        {
            if (GUI.Button(new Rect(20, currentHeight + i * 30, 100, 20), profession.professionName))
            {
                player.Profession = profession;
            }
            i++;
        }

        GUI.EndScrollView();

        GUI.Box(new Rect(Screen.width - 170, Screen.height - 90, 155, 80), "Display");
        GUI.Label(new Rect(Screen.width - 140, Screen.height - 90 + 30, 100, 20), player.Profession.professionName);
        GUI.Label(new Rect(Screen.width - 140, Screen.height - 90 + 45, 100, 20), player.Profession.abilityName);
        GUI.Label(new Rect(Screen.width - 140, Screen.height - 90 + 60, 100, 20), player.Profession.abilityDescription);

    }

    private void StatsOnGUI()
    {
        float currentHeight = 40;
        GUI.Box(new Rect(Screen.width - 170, 10, 155, 210), "Stats : " + player.playerStats.stats.baseStatPoints);

        for (int i = 0; i < player.playerStats.stats.baseStats.Length; i++)
        {
            BaseStats stat = player.playerStats.stats.baseStats[i];

            if (GUI.Button(new Rect(Screen.width - 165, currentHeight + i * 30, 20, 20), "-"))
            {
                player.playerStats.SetStats(i, -1);
            }

            GUI.Label(new Rect(Screen.width - 140, currentHeight + i * 30, 100, 20),
                stat.baseStatName + ": " + stat.finalStat);

            if (GUI.Button(new Rect(Screen.width - 40, currentHeight + i * 30, 20, 20), "+"))
            {
                player.playerStats.SetStats(i, 1);
            }
        }
    }

    private void CustomizeOnGUI()
    {
        float currentHeight = 40f;

        //GUI.Box(new Rect(Screen.width - 110, 10, 100, 90), "Top Right");

        GUI.Box(new Rect(10, 10, 110, 210), "Visuals");

        string[] names = { "Skin", "Hair", "Mouth", "Eyes", "Clothes", "Armour" };

        for (int i = 0; i < names.Length; i++)
        {
            if (GUI.Button(new Rect(20, currentHeight + i * 30, 20, 20), "<"))
            {
                SetTexture(names[i], -1);
            }

            GUI.Label(new Rect(45, currentHeight + i * 30, 60, 20), names[i]);

            if (GUI.Button(new Rect(90, currentHeight + i * 30, 20, 20), ">"))
            {
                SetTexture(names[i], 1);
            }
        }
    }
}











/*
        void SetTexture(CustomiseParts part, int direction)
        {
            int partIndex = (int)part;
            int max = partsTexture[partIndex].Count;
            int currentTexture = currentPartsTextureIndex[partIndex];
            currentTexture += direction;
            if (currentTexture < 0)
            {
                currentTexture = max - 1;
            }
            else if (currentTexture > max - 1)
            {
                currentTexture = 0;
            }
            currentPartsTextureIndex[partIndex] = currentTexture;
            Material[] mats = characterRenderer.materials;
            mats[partIndex].mainTexture = partsTexture[partIndex][currentTexture];
            characterRenderer.materials = mats;
        }
        private void OnGUI()
        {
            float currentHeight = 40;
            //GUI.Box(new Rect(Screen.width - 110, 10, 100, 90), "Top Right");
            GUI.Box(new Rect(10, 10, 100, 210), "Visuals");
            //string[] names = Enum.GetNames(typeof(CustomiseParts));// { "Skin", "Hair", "Mouth", "Eyes", "Clothes", "Armour" };
            int i = 0;
            foreach (CustomiseParts names in Enum.GetValues(typeof(CustomiseParts)))
            {

                if (GUI.Button(new Rect(20, currentHeight + i * 30, 20, 20), "<"))
                {
                    SetTexture(names, -1);
                }
                GUI.Label(new Rect(45, currentHeight + i * 30, 60, 20), names.ToString());
                if (GUI.Button(new Rect(90, currentHeight + i * 30, 20, 20), ">"))
                {
                    SetTexture(names, 1);
                }
                i++;
            }
        }*/

/*
//---Skin
if (GUI.Button(new Rect(20, currentHeight, 20, 20), "<"))
{
    TestMethod();
}

GUI.Label(new Rect(45, currentHeight, 60, 20), "Skin");

if (GUI.Button(new Rect(90, currentHeight, 20, 20), ">"))
{
    TestMethod();
}

currentHeight += 30;

//---Hair
if (GUI.Button(new Rect(20, currentHeight, 20, 20), "<"))
{
    TestMethod();
}

GUI.Label(new Rect(45, currentHeight, 60, 20), "Hair");

if (GUI.Button(new Rect(90, currentHeight, 20, 20), ">"))
{
    TestMethod();
}

currentHeight += 30;

//---Eyes
if (GUI.Button(new Rect(20, currentHeight, 20, 20), "<"))
{
    TestMethod();
}

GUI.Label(new Rect(45, currentHeight, 60, 20), "Eyes");

if (GUI.Button(new Rect(90, currentHeight, 20, 20), ">"))
{
    TestMethod();
}

currentHeight += 30;

//---Mouth
if (GUI.Button(new Rect(20, currentHeight, 20, 20), "<"))
{
    TestMethod();
}

GUI.Label(new Rect(45, currentHeight, 60, 20), "Mouth");

if (GUI.Button(new Rect(90, currentHeight, 20, 20), ">"))
{
    TestMethod();
}

currentHeight += 30;

//---Clothes
if (GUI.Button(new Rect(20, currentHeight, 20, 20), "<"))
{
    TestMethod();
}

GUI.Label(new Rect(45, currentHeight, 60, 20), "Clothes");

if (GUI.Button(new Rect(90, currentHeight, 20, 20), ">"))
{
    TestMethod();
}

currentHeight += 30;

//---Armour
if (GUI.Button(new Rect(20, currentHeight, 20, 20), "<"))
{
    TestMethod();
}

GUI.Label(new Rect(45, currentHeight, 60, 20), "Armour");

if (GUI.Button(new Rect(90, currentHeight, 20, 20), ">"))
{
    TestMethod();
}
*/
