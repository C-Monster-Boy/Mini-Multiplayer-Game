using UnityEngine;

[CreateAssetMenu(fileName = "New Message", menuName = "Last Man To Break/Message")]
public class SO_Message : ScriptableObject {
    public string heading;

    [TextArea]
    public string content;

    public MessageAccentType messageAccentType;

    public bool routeToMainMenu;
}