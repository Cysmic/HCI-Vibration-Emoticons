using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadEmoticon : MonoBehaviour
{
    private enum Emoticon {
        Happy,
        Sad,
        Angry,
        Surprised,
        Disgusted,
        Fearful,
        Neutral
    }

    private enum TapLength {
        Short,
        Long
    }

    private Queue<List<TapLength>> GestureQueue = new Queue<List<TapLength>>();
    private Queue<Emoticon> emoticonQueue = new Queue<Emoticon>();
    private float lastTapTime = 0;

    private TapLength[][] GestureMap = new TapLength[6][]{
                                        new TapLength[2]{TapLength.Short, TapLength.Short},
                                        new TapLength[8]{TapLength.Short, TapLength.Long, TapLength.Short, TapLength.Long, TapLength.Short, TapLength.Long, TapLength.Short, TapLength.Long, },
                                        new TapLength[3]{TapLength.Short, TapLength.Short, TapLength.Long},
                                        new TapLength[4]{TapLength.Short, TapLength.Short, TapLength.Long, TapLength.Long},
                                        new TapLength[6]{TapLength.Long, TapLength.Short, TapLength.Short, TapLength.Long, TapLength.Short, TapLength.Short},
                                        new TapLength[1]{TapLength.Long}
    };

    private string[] EmoticonMap = {"Like", "Hearts", "Laugh", "Smile", "Crying", "Angry"};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEmoticonDisplayOff()
    {
        //Turn off all emoticons
    }

    //This is where the ml or rhythm detection should occur.
    //Gesture data will be in the format found in SendEmoticons. 
    //If a gesture is determined from the ml model or from predefined gestures, 
    //then the corresponding emoticon will be displayed.
    //
    //I will add references to each emoticon in this script. Based on which emoticon is determined,
    //you just have to call the following (example): Happy.SetActive(true);
    public void ReadEmoticons(SendEmoticon.TapData[] taps)
    {
        List<TapLength> gesture = new List<TapLength>();
        Debug.Log("StartReadingEmoticons");

        // Enqueue each gesture. A gesture is a list of TapLengths, where the threshold between a long
        // and short tap is 0.25 seconds. The threshold for time between taps in the same gesture is 0.5
        // seconds - beyond this, the next tap is considered a part of a new gesture.
        for (int i = 0; i < taps.Length; i++) {
            if (taps[i].startTime - lastTapTime > 0.5) {
                GestureQueue.Enqueue(gesture);
                gesture = new List<TapLength>();
            }
            if (taps[i].endTime - taps[i].startTime < 0.25) {
                gesture.Add(TapLength.Short);
            } else {
                gesture.Add(TapLength.Long);
            }
            lastTapTime = taps[i].endTime;
        }
        GestureQueue.Enqueue(gesture);

        // Interpret queue of gestures and pattern-match to known gestures. If a match is found, log
        // the matched emoticon.
        while (GestureQueue.Count > 0) {
            TapLength[] currentGesture = GestureQueue.Dequeue().ToArray();
            for (int i = 0; i < GestureMap.Length; i++) {
                if (GestureMap[i].Length == currentGesture.Length) {
                    for (int j = 0; j < GestureMap[i].Length; j++) {
                        if (GestureMap[i][j] != currentGesture[j]) {
                            break;
                        }
                        if (j == GestureMap.Length - 1) {
                            Debug.Log(EmoticonMap[i]);
                        }
                    }
                }
            }
        }
    }
}
