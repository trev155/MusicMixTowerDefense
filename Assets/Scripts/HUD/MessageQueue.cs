using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MessageQueue : MonoBehaviour {
    //---------- Fields ----------
    public Text messageQueueEntry;

    private Queue<string> messages;

    private int messageNum = 1;
    private readonly int messageQueueDisplayLimit = 5;
    private bool isCleaning = false;

    private Color32 infoMessageColour = new Color32(14, 10, 25, 255);
    private Color32 positiveMessageColour = new Color32(150, 250, 102, 255);
    private Color32 negativeMessageColour = new Color32(250, 50, 0, 255);

    //---------- Methods ----------
    private void Awake() {
        messages = new Queue<string>();
    }

    public void PushMessage(string message) {
        PushMessage(message, MessageType.INFO);
    }

    public void PushMessage(string message, MessageType messageType) {
        messages.Enqueue(message);
        CreateNewTextEntryDisplay(message, messageType);
        
        if (MessageQueueOverflowed() && !isCleaning) {
            StartCoroutine(CleanMessageQueue());
        }
    }

    private void CreateNewTextEntryDisplay(string message, MessageType messageType) {
        Text entry = Instantiate(messageQueueEntry, transform) as Text;
        entry.transform.SetAsFirstSibling();
        entry.text = message;
        entry.name = "MessageEntry_" + messageNum;
        SetTextColour(entry, messageType);
        messageNum++;
    }

    private void SetTextColour(Text text, MessageType messageType) {
        switch (messageType) {
            case MessageType.INFO:
                text.color = infoMessageColour;
                break;
            case MessageType.POSITIVE:
                text.color = positiveMessageColour;
                break;
            case MessageType.NEGATIVE:
                text.color = negativeMessageColour;
                break;
            default:
                throw new GameplayException("Unrecognized message type, cannot set text colour");
        }
    }

    private bool MessageQueueOverflowed() {
        return transform.childCount > messageQueueDisplayLimit;
    }

    private IEnumerator CleanMessageQueue() {
        isCleaning = true;
        while (true) {
            if (transform.childCount > messageQueueDisplayLimit) {
                RemoveOldestMessage();
                yield return new WaitForSeconds(0.1f);
            } else {
                break;
            }
        }
        isCleaning = false;
    }

    private void RemoveOldestMessage() {
        Destroy(transform.GetChild(transform.childCount - 1).gameObject);
    }

    public void LogMessageQueue() {
        foreach (string message in messages) {
            Debug.Log(message);
        }
    }
}