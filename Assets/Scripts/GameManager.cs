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

    enum GameState
    {
        Idle,
        ReceivingEmoticons,
        SendingEmoticons,
        ReadingEmoticons
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
            receiveEmoticon.ReceiveEmoticons();
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
            sendEmoticon.enabled = false;
            
            playButton.SetActive(false);
            endReadEmoticonButton.SetActive(true);

            readEmoticon.Reset();
            readEmoticon.ReadEmoticons(sendEmoticon.GetTaps());
        }
    }
}