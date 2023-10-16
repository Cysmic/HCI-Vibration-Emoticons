using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveEmoticon : MonoBehaviour
{
    private enum Emoticon{
        Happy,
        Sad,
        Angry,
        Surprised,
        Disgusted,
        Fearful,
        Neutral
    }

    private Queue<Emoticon> emoticonQueue = new Queue<Emoticon>();

    // Initialize the queue with all the emoticons
    void Start(){
        emoticonQueue.Enqueue(Emoticon.Happy);
        emoticonQueue.Enqueue(Emoticon.Sad);
        emoticonQueue.Enqueue(Emoticon.Angry);
        emoticonQueue.Enqueue(Emoticon.Surprised);
        emoticonQueue.Enqueue(Emoticon.Disgusted);
        emoticonQueue.Enqueue(Emoticon.Fearful);
        emoticonQueue.Enqueue(Emoticon.Neutral);
    }

    // Send the first emoticon in the queue to the user. 
    public void ReceiveEmoticons()
    {
        Emoticon emoticon = emoticonQueue.Dequeue(); //emoticon to send to the user

        //make vibration calls here corresponding with each emoticon
        switch(emoticon){
            case Emoticon.Happy:
                Debug.Log("Happy");
                Taptic.Medium(); //Follow the calls from the documentation - Taptic.Medium() is just an example
                break;
            case Emoticon.Sad:
                Debug.Log("Sad");
                break;
            case Emoticon.Angry:
                Debug.Log("Angry");
                break;
            case Emoticon.Surprised:
                Debug.Log("Surprised");
                break;
            case Emoticon.Disgusted:
                Debug.Log("Disgusted");
                break;
            case Emoticon.Fearful:
                Debug.Log("Fearful");
                break;
            case Emoticon.Neutral:
                Debug.Log("Neutral");
                break;
        }
    }
}
