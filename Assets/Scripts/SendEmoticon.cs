using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendEmoticon : MonoBehaviour
{
    private Touch currentTouch;
    private float startTime;
    private Vector2 startPosition;

    [SerializeField]
    private GameObject linePrefab;

    [SerializeField]
    private GameObject PNG_Camera_Object;

    [SerializeField]
    private RenderTexture PNG_Texture;

    private Line activeLine;

    private int group = 1;

    public struct TapData
    {
        public float startTime;
        public float endTime;
    }

    private Queue<TapData> tapQueue = new Queue<TapData>();

    void Update()
    {
        if (group == 1){ //Group 1
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
        else{ //Group 2
            if (Input.touchCount > 0){
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    GameObject newLine = Instantiate(linePrefab);
                    activeLine = newLine.GetComponent<Line>();
                }

                if (activeLine != null && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    activeLine.UpdateLine(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));
                }

                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    activeLine = null;
                }
            }
        }
    }

    public byte[] GetImageOfGesture()
    {
        PNG_Camera_Object.SetActive(true);
        Camera PNG_Camera = PNG_Camera_Object.GetComponent<Camera>();
        PNG_Camera.Render();

        Texture2D texture2D = new Texture2D(PNG_Texture.width, PNG_Texture.height, TextureFormat.RGB24, false);
        RenderTexture.active = PNG_Texture;
        texture2D.ReadPixels(new Rect(0, 0, PNG_Texture.width, PNG_Texture.height), 0, 0);
        texture2D.Apply();

        PNG_Camera.targetTexture = null;
        RenderTexture.active = null;

        byte[] pngData = texture2D.EncodeToPNG();

        PNG_Camera_Object.SetActive(false);

        return pngData;
    }
    public void ChangeEmoticonGroup(int sendingEmoticonGroup)
    {
        group = sendingEmoticonGroup;
    }

    public TapData[] GetTaps()
    {
        return tapQueue.ToArray();
    }

    public void Reset()
    {
        tapQueue = new Queue<TapData>();
    }
}
