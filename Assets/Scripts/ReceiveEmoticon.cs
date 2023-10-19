using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceiveEmoticon : MonoBehaviour
{
    [SerializeField]
    private Group group = Group.rhythmBased;

    private enum Group
    {
        rhythmBased,
        numberingBased
    }

    public void ChangeGroup()
    {
        Debug.Log("Changing from Group " + group);
        if (group == Group.rhythmBased)
        {
            group = Group.numberingBased;
        }
        if (group == Group.numberingBased)
        {
            group = Group.rhythmBased;
        }
    }

    private enum Emoticon
    {
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

    private Queue<Emoticon> emoticonQueue = new Queue<Emoticon>();

    // Initialize the queue with all the emoticons
    void Start()
    {
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
                Debug.Log("default vibration");
                Taptic.Default();
                break;
            case Vibration.Vibrate:
                Debug.Log("vibrate vibration");
                Taptic.Vibrate();
                break;
            case Vibration.Selection:
                Debug.Log("selection vibration");
                Taptic.Selection();
                break;
            case Vibration.Heavy:
                Debug.Log("heavy vibration");
                Taptic.Heavy();
                break;
            case Vibration.Medium:
                Debug.Log("medium vibration");
                Taptic.Medium();
                break;
            case Vibration.Light:
                Debug.Log("light vibration");
                Taptic.Light();
                break;
            case Vibration.Success:
                Debug.Log("success vibration");
                Taptic.Success();
                break;
            case Vibration.Failure:
                Debug.Log("failure vibration");
                Taptic.Failure();
                break;
            case Vibration.Warning:
                Debug.Log("warning vibration");
                Taptic.Warning();
                break;
        }
    }

    private void VibrateFor(float duration)
    {
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            Debug.Log("vibrating: " + Time.time);
            Handheld.Vibrate();
        }
    }

    // Send the first emoticon in the queue to the user. 
    public void ReceiveEmoticons()
    {
        Debug.Log("Receiving Emoticon");
        Emoticon emoticon = emoticonQueue.Dequeue(); //emoticon to send to the user

        //make vibration calls here corresponding with each emoticon
        switch (emoticon)
        {
            case Emoticon.Like:
                Debug.Log("Like Emoji Received");
                if (group == Group.rhythmBased)
                {
                    Debug.Log("Rhythm Group: Clapping (10x heavy)");
                    for (int i = 0; i < 10; i++)
                    {
                        Taptic.Heavy();
                        StartCoroutine(Wait(0.5f));
                    }
                }
                else if (group == Group.numberingBased)
                {
                    Debug.Log("Numbering Group: 1 Vibration");
                    Handheld.Vibrate();
                }
                break;
            case Emoticon.Heart:
                Debug.Log("Heart Emoji Received");
                if (group == Group.rhythmBased)
                {
                    Debug.Log("Rhythm Group: Heartbeats (4x success)");
                    for (int i = 0; i < 4; i++)
                    {
                        Taptic.Success();
                        StartCoroutine(Wait(1.0f));
                    }
                }
                else if (group == Group.numberingBased)
                {
                    Debug.Log("Numbering Group: 2 Vibrations");
                    Handheld.Vibrate();
                    StartCoroutine(Wait(0.8f));
                    Handheld.Vibrate();
                }
                break;
            case Emoticon.Laugh:
                Debug.Log("Laugh Emoji Received");
                if (group == Group.rhythmBased)
                {
                    Debug.Log("Rhythm Group: hahaha (3x heavy-heavy-heavy)");
                    for (int i = 0; i < 3; i++)
                    {
                        Taptic.Heavy();
                        StartCoroutine(Wait(0.5f));
                        Taptic.Heavy();
                        StartCoroutine(Wait(0.5f));
                        Taptic.Heavy();
                        StartCoroutine(Wait(1.5f));
                    }
                }
                else if (group == Group.numberingBased)
                {
                    Debug.Log("Numbering Group: 3 Vibrations");
                    Handheld.Vibrate();
                    StartCoroutine(Wait(0.8f));
                    Handheld.Vibrate();
                    StartCoroutine(Wait(0.8f));
                    Handheld.Vibrate();
                }
                break;
            case Emoticon.Smile:
                Debug.Log("Smile Emoji Received");
                if (group == Group.rhythmBased)
                {
                    Debug.Log("Rhythm Group: smile (1x vibrate) ");
                    Handheld.Vibrate();
                }
                else if (group == Group.numberingBased)
                {
                    Debug.Log("Numbering Group: 4 Vibrations");
                    Handheld.Vibrate();
                    StartCoroutine(Wait(0.8f));
                    Handheld.Vibrate();
                    StartCoroutine(Wait(0.8f));
                    Handheld.Vibrate();
                    StartCoroutine(Wait(0.8f));
                    Handheld.Vibrate();
                }
                break;
            case Emoticon.Cry:
                Debug.Log("Cry Emoji Received");
                if (group == Group.rhythmBased)
                {
                    Debug.Log("Rhythm Group: tears trickling down (2x failure)");
                    Taptic.Failure();
                    StartCoroutine(Wait(1.0f));
                    Taptic.Failure();
                }
                else if (group == Group.numberingBased)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Debug.Log("Numbering Group: 5 Vibrations");
                        Handheld.Vibrate();
                        StartCoroutine(Wait(0.8f));
                        Handheld.Vibrate();
                        StartCoroutine(Wait(0.8f));
                        Handheld.Vibrate();
                        StartCoroutine(Wait(0.8f));
                        Handheld.Vibrate();
                        StartCoroutine(Wait(0.8f));
                        Handheld.Vibrate();
                        StartCoroutine(Wait(0.8f));
                        Handheld.Vibrate();
                    }
                }
                break;
            case Emoticon.Angry:
                Debug.Log("Angry Emoji Received");
                if (group == Group.rhythmBased)
                {
                    Debug.Log("Rhythm Group: Stomping");
                    for (int i = 0; i < 5; i++)
                    {
                        Handheld.Vibrate();
                        StartCoroutine(Wait(0.5f));
                    }
                }
                else if (group == Group.numberingBased)
                {
                    Debug.Log("Numbering Group: 6 Vibrations");
                    Handheld.Vibrate();
                    StartCoroutine(Wait(0.8f));
                    Handheld.Vibrate();
                    StartCoroutine(Wait(0.8f));
                    Handheld.Vibrate();
                    StartCoroutine(Wait(0.8f));
                    Handheld.Vibrate();
                    StartCoroutine(Wait(0.8f));
                    Handheld.Vibrate();
                    StartCoroutine(Wait(0.8f));
                    Handheld.Vibrate();
                    StartCoroutine(Wait(0.8f));
                    Handheld.Vibrate();
                }
                break;
        }
    }
}
