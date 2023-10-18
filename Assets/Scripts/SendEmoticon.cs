using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendEmoticon : MonoBehaviour
{
    private Touch currentTouch;
    private float startTime;
    private Vector2 startPosition;

    //See Touch class in unity documentation
    //https://docs.unity3d.com/ScriptReference/Touch.html
    public struct GestureData
    {
        Vector2 deltaPosition;
        float deltaTime;
        TouchPhase phase;
        Vector2 position;
        int tapCount;
    }

    // private Queue<GestureData> gestureQueue = new Queue<GestureData>();
    
    //Records gestures of the users
    //For now make a check to make sure the input is above a certain y coordinate (ie the y coordinate of the play button. can also add x coordinate check)
    //This will be a rudimentary check to prevent the tap on the play button from being recorded as a gesture
    //
    //any gesture that occurs, add to the gestureQueue. Thats all that has to happen in here.
    //
    //Look at reference for touch I added above.

    public struct TapData
    {
        float startTime;
        float endTime;
    }

    private Queue<TapData> tapQueue = new Queue<TapData>();

    void Update()
    {
        if (Input.touchCount > 0) {
            currentTouch = Input.GetTouch(0);
            
            //if (currentTouch.position.Y < buttonY) {return;}

            if (currentTouch.phase == TouchPhase.Began) {
                startTime = Time.time;
            } else if (currentTouch.phase == TouchPhase.Ended) {
                TapData currentTap = new TapData();
                currentTap.startTime = startTime;
                currentTap.endTime = Time.time;

                tapQueue.Enqueue(currentTap);
            }
        }
    }

    public TapData[] GetTaps()
    {
        return tapQueue.ToArray();
    }
}
