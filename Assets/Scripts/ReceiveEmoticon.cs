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

    private int group = 1; // 1 or 2, group 1 is purely magnitude-based, group 2 is rhythm-based

    private Queue<Emoticon> emoticonQueue = new Queue<Emoticon>();

    // Initialize the queue with all the emoticons
    void Start(){
        Debug.Log("Initializing Emoticons");
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
                if (group == 1)
                {
                    Taptic.Default();
                    Taptic.Default();
                } else if (group == 2)
                {
                    Taptic.Success();
                    Taptic.Success();
                }
                break;
            case Emoticon.Heart:
                Debug.Log("Heart");
                if (group == 1)
                {
                    Taptic.Light();
                    Taptic.Light();
                }
                else if (group == 2)
                {
                    for (int i = 0; i < 4; i++) {
                        Taptic.Light();
                        Taptic.Heavy();
                    }
                }
                break;
            case Emoticon.Laugh:
                Debug.Log("Laugh");
                if (group == 1)
                {
                    Taptic.Heavy();
                    Taptic.Heavy();
                }
                else if (group == 2)
                {
                    Taptic.Light();
                    Taptic.Light();
                    Taptic.Heavy();
                }
                break;
            case Emoticon.Smile:
                Debug.Log("Smile");
                if (group == 1)
                {
                    Taptic.Success();
                    Taptic.Success();
                }
                else if (group == 2)
                {
                    Taptic.Light();
                    Taptic.Light();
                    Taptic.Medium();
                    Taptic.Heavy();
                }
                break;
            case Emoticon.Cry:
                Debug.Log("Cry");
                if (group == 1)
                {
                    Taptic.Medium();
                    Taptic.Medium();
                }
                else if (group == 2)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Taptic.Heavy();
                        Taptic.Light();
                        Taptic.Light();
                    }
                }
                break;
            case Emoticon.Angry:
                Debug.Log("Angry");
                if (group == 1)
                {
                    Taptic.Warning();
                    Taptic.Warning();
                }
                else if (group == 2)
                {
                    Taptic.Warning();
                }
                break;
        }
    }
}
