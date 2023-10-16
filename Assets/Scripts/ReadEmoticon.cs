using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadEmoticon : MonoBehaviour
{
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
    public void ReadEmoticons(SendEmoticon.GestureData[] gestures)
    {
        Debug.Log("StartReadingEmoticons");
    }
}
