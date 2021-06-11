using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Last Man To Break/Character")]
public class SO_Character : ScriptableObject 
{
    public string characterName;
    public Sprite characterImage;
    public SO_Weapon weapon;
    public SO_Deployable deployable;

}