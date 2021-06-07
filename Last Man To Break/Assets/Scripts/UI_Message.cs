using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UI_Message : MonoBehaviour
{
    [Header("Colors")]
    public   Color CRITICAL_ACCENT_COLOR;
    public  Color NORMAL_ACCENT_COLOR;
    public  Color TRIVIAL_ACCENT_COLOR;

#region UI Elements

    [Header("UI Elements")]
    public Text headingText;
    public Text contentText;
    public Image accent;
    public GameObject okButton;

#endregion

#region Private Variables
  
#endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

#region  Public Functions

    public void GoToMainMenu()
    {
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainMenu")
        {
             PhotonNetwork.LoadLevel("MainMenu");
        }
       
        MessageBus.Instance.isDisplayingMessage = false;
        Destroy(this.gameObject, 0.3f);
    }

    public void ConfigureMesage(string headingInput, string messageContentInput, MessageAccentType messageType = MessageAccentType.Trivial, bool userInteractible = true)
    {
        SetMessageAccent(messageType);
        SetHeading(headingInput);
        SetContent(messageContentInput);
        SetOkButton(userInteractible);
    }

#endregion

# region Private Functions

    private void SetMessageAccent(MessageAccentType messageType)
    {
        switch(messageType)
        {
            case MessageAccentType.Critical:
                accent.color = CRITICAL_ACCENT_COLOR;
                break;
            case MessageAccentType.Normal:
                accent.color = NORMAL_ACCENT_COLOR;
                break;
            case MessageAccentType.Trivial:
                accent.color = TRIVIAL_ACCENT_COLOR;
                break;
            default:
                break;
        }
    }

    private void SetHeading(string headingString)
    {
        headingText.text = headingString;
    }

    private void SetContent(string content)
    {
        contentText.text = content;
    }

    private void SetOkButton(bool setButton)
    {
        okButton.SetActive(setButton);
    }

#endregion


}


public enum MessageAccentType
{
    Critical, Normal, Trivial
}