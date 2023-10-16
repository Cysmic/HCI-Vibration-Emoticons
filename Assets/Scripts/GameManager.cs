using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameState gameState = GameState.Idle;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
