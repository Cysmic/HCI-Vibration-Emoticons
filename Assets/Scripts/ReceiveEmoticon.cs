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

    private int group = 1;

    private Queue<Emoticon> emoticonQueue = new Queue<Emoticon>();

    // Initialize the queue with all the emoticons
    void Start(){
        //first allow the user to learn all 6 emoticon vibrations
        emoticonQueue.Enqueue(Emoticon.Like);
        emoticonQueue.Enqueue(Emoticon.Heart);
        emoticonQueue.Enqueue(Emoticon.Laugh);
        emoticonQueue.Enqueue(Emoticon.Smile);
        emoticonQueue.Enqueue(Emoticon.Cry);
        emoticonQueue.Enqueue(Emoticon.Angry);


        // then test them on a random 12
        emoticonQueue.Enqueue(Emoticon.Smile);
        emoticonQueue.Enqueue(Emoticon.Cry);
        emoticonQueue.Enqueue(Emoticon.Like);
        emoticonQueue.Enqueue(Emoticon.Heart);
        emoticonQueue.Enqueue(Emoticon.Like);
        emoticonQueue.Enqueue(Emoticon.Angry);
        emoticonQueue.Enqueue(Emoticon.Laugh);
        emoticonQueue.Enqueue(Emoticon.Angry);
        emoticonQueue.Enqueue(Emoticon.Smile);
        emoticonQueue.Enqueue(Emoticon.Laugh);
        emoticonQueue.Enqueue(Emoticon.Cry);
        emoticonQueue.Enqueue(Emoticon.Heart);
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
    public IEnumerator ReceiveEmoticons()
    {
        Debug.Log("Receiving Emoticon");
        Emoticon emoticon = emoticonQueue.Dequeue(); //emoticon to send to the user

        //make vibration calls here corresponding with each emoticon
        switch (emoticon)
        {
            case Emoticon.Like:
                Debug.Log("Like Emoji Received");
                if (group == 1)
                {
                    Debug.Log("Rhythm Group: Clapping (10x heavy)");
                    for (int i = 0; i < 10; i++)
                    {
                        Debug.Log("Taptic Heavy");
                        Taptic.Heavy();
                        yield return new WaitForSeconds(0.25f);
                    }
                }
                else if (group == 2)
                {
                    Debug.Log("Numbering Group: 1 Vibration");
                    Handheld.Vibrate();
                }
                break;
            case Emoticon.Heart:
                Debug.Log("Heart Emoji Received");
                if (group == 1)
                {
                    Debug.Log("Rhythm Group: Heartbeats (4x success)");
                    for (int i = 0; i < 4; i++)
                    {
                        Taptic.Success();
                        yield return new WaitForSeconds(1.0f);
                    }
                }
                else if (group == 2)
                {
                    Debug.Log("Numbering Group: 2 Vibrations");
                    Handheld.Vibrate();
                    yield return new WaitForSeconds(0.8f);
                    Handheld.Vibrate();
                }
                break;
            case Emoticon.Laugh:
                Debug.Log("Laugh Emoji Received");
                if (group == 1)
                {
                    Debug.Log("Rhythm Group: hahaha (3x heavy-heavy-heavy)");
                    for (int i = 0; i < 3; i++)
                    {
                        Taptic.Heavy();
                        yield return new WaitForSeconds(0.5f);
                        Taptic.Heavy();
                        yield return new WaitForSeconds(0.5f);
                        Taptic.Heavy();
                        yield return new WaitForSeconds(1.5f);
                    }
                }
                else if (group == 2)
                {
                    Debug.Log("Numbering Group: 3 Vibrations");
                    Handheld.Vibrate();
                    yield return new WaitForSeconds(0.8f);
                    Handheld.Vibrate();
                    yield return new WaitForSeconds(0.8f);
                    Handheld.Vibrate();
                }
                break;
            case Emoticon.Smile:
                Debug.Log("Smile Emoji Received");
                if (group == 1)
                {
                    Debug.Log("Rhythm Group: smile (1x vibrate) ");
                    Handheld.Vibrate();
                }
                else if (group == 2)
                {
                    Debug.Log("Numbering Group: 4 Vibrations");
                    Handheld.Vibrate();
                    yield return new WaitForSeconds(0.8f);
                    Handheld.Vibrate();
                    yield return new WaitForSeconds(0.8f);
                    Handheld.Vibrate();
                    yield return new WaitForSeconds(0.8f);
                    Handheld.Vibrate();
                }
                break;
            case Emoticon.Cry:
                Debug.Log("Cry Emoji Received");
                if (group == 1)
                {
                    Debug.Log("Rhythm Group: tears trickling down (2x failure)");
                    Taptic.Failure();
                    yield return new WaitForSeconds(1.0f);
                    Taptic.Failure();
                }
                else if (group == 2)
                {
                    Debug.Log("Numbering Group: 5 Vibrations");
                    Handheld.Vibrate();
                    yield return new WaitForSeconds(0.8f);
                    Handheld.Vibrate();
                    yield return new WaitForSeconds(0.8f);
                    Handheld.Vibrate();
                    yield return new WaitForSeconds(0.8f);
                    Handheld.Vibrate();
                    yield return new WaitForSeconds(0.8f);
                    Handheld.Vibrate();
                    yield return new WaitForSeconds(0.8f);
                }
                break;
            case Emoticon.Angry:
                Debug.Log("Angry Emoji Received");
                if (group == 1)
                {
                    Debug.Log("Rhythm Group: Stomping");
                    for (int i = 0; i < 5; i++)
                    {
                        Handheld.Vibrate();
                        yield return new WaitForSeconds(0.8f);
                    }
                }
                else if (group == 2)
                {
                    Debug.Log("Numbering Group: 6 Vibrations");
                    Handheld.Vibrate();
                    yield return new WaitForSeconds(0.8f);
                    Handheld.Vibrate();
                    yield return new WaitForSeconds(0.8f);
                    Handheld.Vibrate();
                    yield return new WaitForSeconds(0.8f);
                    Handheld.Vibrate();
                    yield return new WaitForSeconds(0.8f);
                    Handheld.Vibrate();
                    yield return new WaitForSeconds(0.8f);
                    Handheld.Vibrate();
                    yield return new WaitForSeconds(0.8f);
                }
                break;
        }
    }
}
