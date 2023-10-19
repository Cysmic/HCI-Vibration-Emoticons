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

    private enum Vibration
    {
        Warning,
        Failure,
        Success,
        Light,
        Medium,
        Heavy,
        Selection,
        Default,
        Vibrate
    }

    private int group = 1; // 1 or 2, group 1 is purely magnitude-based, group 2 is rhythm-based

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

    public void ChangeEmoticonGroup(int newGroup)
    {
        group = newGroup;
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    IEnumerator WaitThenVibrate(float time_buffer, Vibration vibration)
    {
        yield return new WaitForSeconds(time_buffer);
        switch (vibration)
        {
            case Vibration.Default:
                Debug.Log("default");
                Taptic.Default();
                break;
            case Vibration.Vibrate:
                Debug.Log("vibrate");
                Taptic.Vibrate();
                break;
            case Vibration.Selection:
                Debug.Log("selection");
                Taptic.Selection();
                break;
            case Vibration.Heavy:
                Debug.Log("heavy");
                Taptic.Heavy();
                break;
            case Vibration.Medium:
                Debug.Log("medium");
                Taptic.Medium();
                break;
            case Vibration.Light:
                Debug.Log("light");
                Taptic.Light();
                break;
            case Vibration.Success:
                Debug.Log("success");
                Taptic.Success();
                break;
            case Vibration.Failure:
                Debug.Log("failure");
                Taptic.Failure();
                break;
            case Vibration.Warning:
                Debug.Log("warning");
                Taptic.Warning();
                break;
        }
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
                    StartCoroutine(WaitThenVibrate(0.0f, Vibration.Default));
                    StartCoroutine(WaitThenVibrate(0.4f, Vibration.Default));
                } else if (group == 2)
                {
                    StartCoroutine(WaitThenVibrate(0.0f, Vibration.Success));
                    StartCoroutine(WaitThenVibrate(0.4f, Vibration.Success));
                }
                break;
            case Emoticon.Heart:
                Debug.Log("Heart");
                if (group == 1)
                {
                    StartCoroutine(WaitThenVibrate(0.0f, Vibration.Light));
                    StartCoroutine(WaitThenVibrate(0.4f, Vibration.Light));
                }
                else if (group == 2)
                {
                    for (int i = 0; i < 4; i++) {
                        StartCoroutine(WaitThenVibrate(0.0f, Vibration.Light));
                        StartCoroutine(WaitThenVibrate(0.2f, Vibration.Heavy));
                        StartCoroutine(Wait(1.3f));
                    }
                }
                break;
            case Emoticon.Laugh:
                Debug.Log("Laugh");
                if (group == 1)
                {
                    StartCoroutine(WaitThenVibrate(0.0f, Vibration.Heavy));
                    StartCoroutine(WaitThenVibrate(0.4f, Vibration.Heavy));
                }
                else if (group == 2)
                {
                    StartCoroutine(WaitThenVibrate(0.0f, Vibration.Light));
                    StartCoroutine(WaitThenVibrate(0.2f, Vibration.Light));
                    StartCoroutine(WaitThenVibrate(0.2f, Vibration.Heavy));
                }
                break;
            case Emoticon.Smile:
                Debug.Log("Smile");
                if (group == 1)
                {
                    StartCoroutine(WaitThenVibrate(0.0f, Vibration.Success));
                    StartCoroutine(WaitThenVibrate(0.4f, Vibration.Success));
                }
                else if (group == 2)
                {
                    StartCoroutine(WaitThenVibrate(0.0f, Vibration.Light));
                    StartCoroutine(WaitThenVibrate(0.4f, Vibration.Light));
                    StartCoroutine(WaitThenVibrate(0.5f, Vibration.Medium));
                    StartCoroutine(WaitThenVibrate(0.6f, Vibration.Heavy));
                }
                break;
            case Emoticon.Cry:
                Debug.Log("Cry");
                if (group == 1)
                {
                    StartCoroutine(WaitThenVibrate(0.0f, Vibration.Medium));
                    StartCoroutine(WaitThenVibrate(0.4f, Vibration.Medium));
                }
                else if (group == 2)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        StartCoroutine(WaitThenVibrate(0.0f, Vibration.Heavy));
                        StartCoroutine(WaitThenVibrate(0.8f, Vibration.Light));
                        StartCoroutine(WaitThenVibrate(0.2f, Vibration.Light));
                    }
                }
                break;
            case Emoticon.Angry:
                Debug.Log("Angry");
                if (group == 1)
                {
                    StartCoroutine(WaitThenVibrate(0.0f, Vibration.Warning));
                    StartCoroutine(WaitThenVibrate(0.4f, Vibration.Warning));
                }
                else if (group == 2)
                {
                    StartCoroutine(WaitThenVibrate(0.0f, Vibration.Warning));
                }
                break;
        }
    }
}
