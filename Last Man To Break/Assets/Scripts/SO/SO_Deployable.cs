using UnityEngine;

[CreateAssetMenu(fileName = "New Deployable", menuName = "Last Man To Break/Deployable", order = 0)]
public class SO_Deployable : ScriptableObject
{
    public string deployableName;
    public Sprite deployableImage;
    public string description;
    
}