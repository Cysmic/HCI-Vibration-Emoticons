using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendEmoticon : MonoBehaviour
{
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

    private Queue<GestureData> gestureQueue = new Queue<GestureData>();
    
    //Records gestures of the users
    //For now make a check to make sure the input is above a certain y coordinate (ie the y coordinate of the play button. can also add x coordinate check)
    //This will be a rudimentary check to prevent the tap on the play button from being recorded as a gesture
    //
    //any gesture that occurs, add to the gestureQueue. Thats all that has to happen in here.
    //
    //Look at reference for touch I added above.
    void Update()
    {

    }

    public GestureData[] GetGestures()
    {
        return gestureQueue.ToArray();
    }
}
