using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveEmoticon : MonoBehaviour
{
    private enum Emoticon{
        Like,
        Heart,
        Laugh,
        Smile,
        Cry,
        Angry
    }

    private Queue<Emoticon> emoticonQueue = new Queue<Emoticon>();

    // Initialize the queue with all the emoticons
    void Start(){
        emoticonQueue.Enqueue(Emoticon.Like);
        emoticonQueue.Enqueue(Emoticon.Heart);
        emoticonQueue.Enqueue(Emoticon.Laugh);
        emoticonQueue.Enqueue(Emoticon.Smile);
        emoticonQueue.Enqueue(Emoticon.Cry);
        emoticonQueue.Enqueue(Emoticon.Angry);
    }

    // Send the first emoticon in the queue to the user. 
    public void ReceiveEmoticons()
    {
        Emoticon emoticon = emoticonQueue.Dequeue(); //emoticon to send to the user

        //make vibration calls here corresponding with each emoticon
        switch(emoticon){
            case Emoticon.Like:
                Debug.Log("Like");
                Taptic.Medium(); //Follow the calls from the documentation - Taptic.Medium() is just an example
                break;
            case Emoticon.Heart:
                Debug.Log("Heart");
                break;
            case Emoticon.Laugh:
                Debug.Log("Laugh");
                break;
            case Emoticon.Smile:
                Debug.Log("Smile");
                break;
            case Emoticon.Cry:
                Debug.Log("Cry");
                break;
            case Emoticon.Angry:
                Debug.Log("Cry");
                break;
        }
    }
}
