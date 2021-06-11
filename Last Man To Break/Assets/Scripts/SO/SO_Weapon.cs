using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Last Man To Break/Weapon")]
public class SO_Weapon : ScriptableObject 
{
    public string weaponName;
    public Sprite weaponImage;
    public string weaponDescription;
    public int damage;
    public int capacity;
}