using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameState gameState = GameState.Idle;
    
    [SerializeField]
    private GameObject playButton;

    [SerializeField]
    private GameObject endReadEmoticonButton;

    [SerializeField]
    private SendEmoticon sendEmoticon;

    [SerializeField]
    private ReceiveEmoticon receiveEmoticon;

    [SerializeField]
    private ReadEmoticon readEmoticon;

    [SerializeField]
    private GameObject Toggle1;

    [SerializeField]
    private GameObject Toggle2;

    public int receivingEmoticonGroup = 1;
    private int sendingEmoticonGroup = 1;

    enum GameState
    {
        Idle,
        ReceivingEmoticons,
        SendingEmoticons,
        ReadingEmoticons
    }

    public void ChangeReceivingEmoticonGroup()
    {
        if (receivingEmoticonGroup == 1)
        {
            receivingEmoticonGroup = 2;
        }
        else
        {
            receivingEmoticonGroup = 1;
        }
        Debug.Log("Changing Receiving Group to " + receivingEmoticonGroup);
        receiveEmoticon.ChangeEmoticonGroup(receivingEmoticonGroup);
    }

    public void ChangeSendingEmoticonGroup()
    {
        if (sendingEmoticonGroup == 1)
        {
            sendingEmoticonGroup = 2;
        }
        else
        {
            sendingEmoticonGroup = 1;
        }
        Debug.Log("Changing Sending Group to " + sendingEmoticonGroup);
        sendEmoticon.ChangeEmoticonGroup(sendingEmoticonGroup);
    }

    public void TurnOffToggles()
    {
        Toggle1.SetActive(false);
        Toggle2.SetActive(false);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting Game");
        sendEmoticon.enabled = false;
    }
    
    public void ChangeGameState()
    {
        Debug.Log("Button Pressed: Changing Game State");
        gameState = gameState == GameState.ReadingEmoticons ? GameState.Idle : gameState + 1;
        
        Debug.Log("ChangeGameState to: " + gameState.ToString());

        if (gameState == GameState.Idle)
        {
            endReadEmoticonButton.SetActive(false);
            playButton.SetActive(true);
        }
        else if (gameState == GameState.ReceivingEmoticons)
        {
            StartCoroutine(receiveEmoticon.ReceiveEmoticons());
            ChangeGameState(); //might need to move this to ReceiveEmoticons.
        }
        else if (gameState == GameState.SendingEmoticons)
        {
            //Once this is called, the user should now be able to perform a gesture or series of taps to send an emoticon
            //Once they have completed their gestures/taps, they can press the triangle on the bottom right to change the gamestate
            //The data from the gestures should be sent back to the manager, which will then be given to the readEmoticon script
            sendEmoticon.enabled = true;
            sendEmoticon.Reset();
        }
        else if (gameState == GameState.ReadingEmoticons)
        {
            playButton.SetActive(false);
            endReadEmoticonButton.SetActive(true);

            if (sendingEmoticonGroup == 1)
            {
                readEmoticon.Reset();
                readEmoticon.ReadEmoticons(sendEmoticon.GetTaps());
            }
            else{
                StartCoroutine(readEmoticon.CallImageClassifier(sendEmoticon.GetImageOfGesture()));
            }
            sendEmoticon.enabled = false;
        }
    }
}
