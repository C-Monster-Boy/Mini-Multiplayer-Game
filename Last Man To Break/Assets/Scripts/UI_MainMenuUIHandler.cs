using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenuUIHandler : MonoBehaviour
{

#region UI Element References

    [Header("UI Elements")]
    public Button multiplayerButton;
    public Button reconnectButton;
    public GameObject connectingView;
    public GameObject disconnectedSymbol;

#endregion

#region Script References

    [Header("Launcher")]
    public Launcher launcher;

#endregion
    private void Start() {
        RenderUIView();
    }

    private void Update() {
        RenderUIView();
    }

#region Public Functions

public void Reconnect()
{
    launcher.Connect();
}

#endregion


#region Private Functions

    private void RenderUIView()
    {
        switch(launcher.connectionState)
        {
            case ConnectionState.Connected:
                SetConnectedView();
                break;
            case ConnectionState.Disconnected:
                SetDisconnectedView();
                break;
            case ConnectionState.Connecting:
                SetConnectingView();
                break;
            default:
                break;
        }
    }
    private void SetDisconnectedView()
    {
        reconnectButton.gameObject.SetActive(true);
        multiplayerButton.interactable = false;
        connectingView.SetActive(false);
        disconnectedSymbol.SetActive(true);
    }

    private void SetConnectedView()
    {
        reconnectButton.gameObject.SetActive(false);
        multiplayerButton.interactable = true;
        connectingView.SetActive(false);
        disconnectedSymbol.SetActive(false);
    }

    private void SetConnectingView()
    {
        reconnectButton.gameObject.SetActive(false);
        multiplayerButton.interactable = false;
        connectingView.SetActive(true);
        disconnectedSymbol.SetActive(false);
    }

#endregion

}
