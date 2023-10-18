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

        if (gameState == GameState.ReceivingEmoticons)
        {
            playButton.SetActive(false);
            receiveEmoticon.ReceiveEmoticons();
            ChangeGameState(); //might need to move this to ReceiveEmoticons.
        }
        else if (gameState == GameState.SendingEmoticons)
        {
            //Once this is called, the user should now be able to perform a gesture or series of taps to send an emoticon
            //Once they have completed their gestures/taps, they can press the triangle on the bottom right to change the gamestate
            //The data from the gestures should be sent back to the manager, which will then be given to the readEmoticon script
            sendEmoticon.enabled = true;
            playButton.SetActive(true);
        }
        else if (gameState == GameState.ReadingEmoticons)
        {
            sendEmoticon.enabled = false;
            readEmoticon.ReadEmoticons(sendEmoticon.GetTaps());
        }
    }
}