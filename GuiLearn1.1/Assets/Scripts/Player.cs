using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


/// <summary>
/// Code relating to the player
/// </summary>

public class Player : MonoBehaviour
{
    public Inventory inventory;
    public PlayerStats playerStats;
    private bool disableRegen = false;
    private float disableRegenTime;
    public float regenCooldown = 5f;

    public float disableStaminaRegenTime;
    public float staminaRegenCooldown = 1f;
    public float staminaDegen = 20f;

    public int[] customisationTextureIndex;

    [SerializeField]
    private PlayerProfession profession;

    public PlayerProfession Profession
    {
        get
        {
            return profession;
        }
        set
        {
            ChangeProfession(value);
        }
    }

    private void Awake()
    {
        //Load player data
        if (SceneManager.GetActiveScene().name != "Customise")
        {
            PlayerData loadedPlayer = PlayerBinarySave.LoadPlayerData();
            if (loadedPlayer != null)
            {
                playerStats.stats = loadedPlayer.stats;
                profession = loadedPlayer.profession;
                customisationTextureIndex = loadedPlayer.customisationTextureIndex;
            }
        }
    }

    public void ChangeProfession(PlayerProfession cProfession)
    {
        profession = cProfession;
        SetUpProfession();
    }

    public void SetUpProfession()
    {
        for (int i = 0; i < playerStats.stats.baseStats.Length; i++)
        {
            if (i < profession.defaultStats.Length)//check if i exists in profession
            {
                playerStats.stats.baseStats[i].defaultStat = profession.defaultStats[i].defaultStat;
            }
        }
    }

    private void Update()
    {
        Interact();

        #region health regen
        if (!disableRegen)
        {
            if (playerStats.CurrentHealth < playerStats.stats.maxHealth)
            {
                playerStats.CurrentHealth += playerStats.stats.regenHealth * Time.deltaTime;
            }
        }
        else
        {
            if (Time.time > disableRegenTime + regenCooldown)
            {
                disableRegen = false;
            }
        }
        #endregion

        #region stamina regen
        if(Time.time > disableStaminaRegenTime + staminaRegenCooldown)
        {
            if(playerStats.stats.currentStamina < playerStats.stats.maxStamina)
            {
                playerStats.stats.currentStamina += playerStats.stats.regenStamina * Time.deltaTime;

            }
            else
            {
                playerStats.stats.currentStamina = playerStats.stats.maxStamina;
            }
        }

        #endregion
    }

    public void LevelUp()
    {
        playerStats.stats.baseStatPoints += 3;

        for (int i = 0; i < playerStats.stats.baseStats.Length; i++)
        {
            playerStats.stats.baseStats[i].levelUpStat += 1;
        }
    }


    private void OnGUI()
    {
        if (GUI.Button(new Rect(130, 10, 120, 20), "Level Up"))
        {
            LevelUp();
        }

        if (GUI.Button(new Rect(130, 40, 120, 20), "Do Damage : " + playerStats.CurrentHealth))
        {
            DealDamage(25f);
        }



        //display our current health
        //GUI.HorizontalSlider(new Rect(20, 10, 100, 25), playerStats.currentHealth, 0f, playerStats.maxHealth);
        //display our current mana
        //GUI.HorizontalSlider(new Rect(20, 30, 100, 25), playerStats.currentMana, 0f, playerStats.maxMana);
        //display our current stamina
        //GUI.HorizontalSlider(new Rect(20, 50, 100, 25), playerStats.currentStamina, 0f, playerStats.maxStamina);

    }

    public void DealDamage(float damage)
    {
        playerStats.CurrentHealth -= damage;
        disableRegen = true;
        disableRegenTime = Time.time;
    }

    public void Heal(float heal)
    {
        playerStats.CurrentHealth += heal;
    }

    void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))//keybinding code here
        {
            Ray ray;
            RaycastHit hitInfo;

            ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

            int layerMask = LayerMask.NameToLayer("Interactable"); // get the layer ID
            layerMask = 1 << layerMask; // actually turning it into a mask (uning bitwise operations)


            if (Physics.Raycast(ray, out hitInfo, 10f, layerMask))
            {
                if (hitInfo.collider.TryGetComponent<NPC>(out NPC npc))
                {
                    npc.Interact();
                }

                if (hitInfo.collider.TryGetComponent<InWorldItem>(out InWorldItem worldItem))
                {
                    inventory.AddItem(worldItem.item);
                    Destroy(hitInfo.collider.gameObject);
                }
            }
        }
    }


}

/*
 * public int level = 3;
    public int health = 55;
    //move()

    public void Save()
    {

        SaveSystem.SavePlayer(this);
    }

    public void Load()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        level = data.level;
        health = data.health;
        Vector3 pos = new Vector3(data.position[0],
                                    data.position[1],
                                    data.position[2]);
        transform.position = pos;
    }
*/
