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
        Debug.Log("Receiving Emoticon");
        Emoticon emoticon = emoticonQueue.Dequeue(); //emoticon to send to the user

        //make vibration calls here corresponding with each emoticon
        switch(emoticon){
            case Emoticon.Like:
                Debug.Log("Like");
                Taptic.Default(); //Follow the calls from the documentation - Taptic.Medium() is just an example
                Taptic.Default();
                break;
            case Emoticon.Heart:
                Debug.Log("Heart");
                Taptic.Light();
                Taptic.Light();
                break;
            case Emoticon.Laugh:
                Debug.Log("Laugh");
                Taptic.Heavy();
                Taptic.Heavy();
                break;
            case Emoticon.Smile:
                Debug.Log("Smile");
                Taptic.Success();
                Taptic.Success();
                break;
            case Emoticon.Cry:
                Debug.Log("Cry");
                Taptic.Medium();
                Taptic.Medium();
                break;
            case Emoticon.Angry:
                Debug.Log("Angry");
                Taptic.Warning();
                Taptic.Warning();
                break;
        }
    }
}
