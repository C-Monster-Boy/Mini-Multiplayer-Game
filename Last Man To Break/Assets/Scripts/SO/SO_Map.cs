using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Last Man To Break/Map")]
public class SO_Map : ScriptableObject {

    public string mapName;
    public Sprite mapImage;
    public string sceneToLoadName;
    
}