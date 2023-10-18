using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadEmoticon : MonoBehaviour
{
    private enum TapLength {
        Short,
        Long
    }

    [SerializeField]
    private GameObject Like;

    [SerializeField]
    private GameObject Heart;

    [SerializeField]
    private GameObject Laugh;

    [SerializeField]
    private GameObject Smile;

    [SerializeField]
    private GameObject Cry;

    [SerializeField]
    private GameObject Angry;

    [SerializeField]
    private GameObject Unknown;

    private GameObject currentlyDisplayedEmoticon;

    private Queue<List<TapLength>> GestureQueue = new Queue<List<TapLength>>();
    private float lastTapTime = 0;
    private bool matched = false;

    private TapLength[][] GestureMap = new TapLength[6][]{
                                        new TapLength[2]{TapLength.Short, TapLength.Short},
                                        new TapLength[8]{TapLength.Short, TapLength.Long, TapLength.Short, TapLength.Long, TapLength.Short, TapLength.Long, TapLength.Short, TapLength.Long},
                                        new TapLength[3]{TapLength.Short, TapLength.Short, TapLength.Long},
                                        new TapLength[4]{TapLength.Short, TapLength.Short, TapLength.Long, TapLength.Long},
                                        new TapLength[6]{TapLength.Long, TapLength.Short, TapLength.Short, TapLength.Long, TapLength.Short, TapLength.Short},
                                        new TapLength[1]{TapLength.Long}
    };

    private GameObject[] EmoticonMap;

    // Start is called before the first frame update
    void Start()
    {
        EmoticonMap = new GameObject[] { Like, Heart, Laugh, Smile, Cry, Angry };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEmoticonDisplayOff()
    {
        if (currentlyDisplayedEmoticon != null){
            Debug.Log("SetEmoticonDisplayOff");
            currentlyDisplayedEmoticon.SetActive(false);
        }
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
        Debug.Log(string.Format("Taps recorded: {0}.", taps.Length));

        // Enqueue each gesture. A gesture is a list of TapLengths, where the threshold between a long
        // and short tap is 0.25 seconds. The threshold for time between taps in the same gesture is 1
        // second - beyond this, the next tap is considered a part of a new gesture.
        for (int i = 0; i < taps.Length; i++) {
            if (taps[i].startTime - lastTapTime > 1 && i != 0) {
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
            for (int i = 0; i < GestureMap.Length + 1; i++) {
                if (matched == true) {
                    matched = false;
                    break;
                }
                if (i == GestureMap.Length && !matched) {
                    Debug.Log("Gesture unknown");
                    Unknown.SetActive(true);
                    currentlyDisplayedEmoticon = Unknown;
                    List<string> tapList = new List<string>();
                    foreach (TapLength tap in currentGesture) {
                        tapList.Add(tap.ToString());
                    }
                    Debug.Log(string.Join(", ", tapList));
                    break;
                }
                if (GestureMap[i].Length == currentGesture.Length) {
                    for (int j = 0; j < GestureMap[i].Length; j++) {
                        if (GestureMap[i][j] != currentGesture[j]) {
                            break;
                        }
                        if (j == GestureMap[i].Length - 1) {
                            Debug.Log("Emoticon Displayed");
                            EmoticonMap[i].SetActive(true);
                            currentlyDisplayedEmoticon = EmoticonMap[i];
                            matched = true;
                            break;
                        }
                    }
                }
            }
        }
    }

    public void Reset()
    {
        GestureQueue = new Queue<List<TapLength>>();
    }
}
