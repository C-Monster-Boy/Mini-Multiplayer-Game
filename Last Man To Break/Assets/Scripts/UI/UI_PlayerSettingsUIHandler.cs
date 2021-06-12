using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerSettingsUIHandler : MonoBehaviour
{

    private int MAX_DAMAGE_VALUE = 100;
    private int MAX_CAPACITY_VALUE = 14;

    [Header("Character UI")]
    public Image playerCharacterImage;
    public Text playerCharacterName;

    [Header("Weapon UI")]
    public Image weaponImage;
    public Image damageFill;
    public Image capacityFill;
    public Text damageCount;
    public Text capacityCount;

    [Header("Deployable UI")]
    public Image deployableImage;
    public Text deployableDescription;

    [Header("Script Reference")]
    public PlayerSettingsHandler playerSettingsHandler;


    // Start is called before the first frame update
    void Start()
    {
        SetCurrentPlayerUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

#region Public Functions

    public void IncreasePlayerIndex()
    {
        playerSettingsHandler.IncreaseCharacterIndex();
        SetCurrentPlayerUI();
    }

    public void DecreasePlayerIndex()
    {
        playerSettingsHandler.DecreaseCharacterIndex();
        SetCurrentPlayerUI();
    }

#endregion

#region Private Functions

    private void SetCurrentPlayerUI()
    {
        int index = playerSettingsHandler.myCharacterIndex;
        SO_Character myCharacter = playerSettingsHandler.characterList[index];

        SetPlayerCharacterUI(myCharacter);
        SetWeaponUI(myCharacter.weapon);
        SetDeployableUI(myCharacter.deployable);
    }

    private void SetPlayerCharacterUI(SO_Character character)
    {
       playerCharacterImage.sprite = character.characterImage;
       playerCharacterName.text = character.characterName;
    }

    private void SetWeaponUI(SO_Weapon weapon)
    {
        weaponImage.sprite = weapon.weaponImage;

        damageCount.text = weapon.damage.ToString();
        damageFill.fillAmount = weapon.damage / (MAX_DAMAGE_VALUE * 1f);

        capacityCount.text = weapon.capacity.ToString();
        capacityFill.fillAmount = weapon.capacity / (MAX_CAPACITY_VALUE * 1f);
    }

    private void SetDeployableUI(SO_Deployable deployable)
    {
        deployableImage.sprite = deployable.deployableImage;
        deployableDescription.text = deployable.description;
    }

#endregion
}
