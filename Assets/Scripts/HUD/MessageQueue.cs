using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MessageQueue : MonoBehaviour {
    //---------- Fields ----------
    public Transform messageQueue;
    public Text messageQueueEntry;

    private Queue<string> messages;

    private int messageNum = 1;
    private readonly int messageQueueDisplayLimit = 3;
    
    //---------- Methods ----------
    private void Awake() {
        messages = new Queue<string>();
    }

    public void PushMessage(string message) {
        messages.Enqueue(message);
        CreateNewTextEntryDisplay(message);
        
        if (MessageQueueOverflowed()) {
            RemoveOldestMessage();
        }
    }

    private void CreateNewTextEntryDisplay(string message) {
        Text entry = Instantiate(messageQueueEntry, messageQueue) as Text;
        entry.transform.SetAsFirstSibling();
        entry.text = message;
        entry.name = "MessageEntry_" + messageNum;
        messageNum++;
    }

    private bool MessageQueueOverflowed() {
        return messageQueue.childCount > messageQueueDisplayLimit;
    }

    private void RemoveOldestMessage() {
        Destroy(messageQueue.GetChild(messageQueue.childCount - 1).gameObject);
    }

    public void LogMessageQueue() {
        foreach (string message in messages) {
            Debug.Log(message);
        }
    }
}