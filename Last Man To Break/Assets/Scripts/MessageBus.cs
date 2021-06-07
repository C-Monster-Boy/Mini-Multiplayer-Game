using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBus : MonoBehaviour
{

#region Static Variables
    public static MessageBus Instance;
#endregion
    

#region Public Variables

    [Header("Message Bus Variables")]
    public GameObject mesagePrefab;
    public bool isDisplayingMessage = false;
    public Queue<MessageType> messageQueue;

#endregion

#region Private Variables

#endregion

#region Message Data

    public SO_Message disconnected;

#endregion

#region Private Variables
    
#endregion

    private void Awake() {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);

            messageQueue = new Queue<MessageType>();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        if(messageQueue.Count > 0)
        {
            DisplayMessage();
        }
        
    }


#region Public Functions

    public void AddMessageToQueue(MessageType messageType = MessageType.Disconnected)
    { 
        messageQueue.Enqueue(messageType);
    }

#endregion

#region Private Functions

    private void DisplayMessage()
    {
        if(!isDisplayingMessage)
        {
            GameObject message = Instantiate(mesagePrefab);

            MessageType currentMessageType = messageQueue.Dequeue();
            SO_Message messageData = GetMessageData(currentMessageType);

            if(messageData == null)
            {
                return;
            }

            message.GetComponent<UI_Message>().ConfigureMesage(messageData.heading, messageData.content, messageData.messageAccentType, messageData.userInteractible);

            isDisplayingMessage = true;
        }
    }

    private SO_Message GetMessageData(MessageType messageType)
    {
        switch(messageType)
        {
            case MessageType.Disconnected:
                return disconnected;
            default:
                return null;
        }
    }


#endregion
}

public enum MessageType
{
    None, Disconnected
}