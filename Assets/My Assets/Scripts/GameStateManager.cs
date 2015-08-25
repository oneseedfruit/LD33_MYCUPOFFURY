using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour
{
    public static Camera mainCamera;

    public StateEnum CurrentGameState; // exposed to the inspector
    public static StateEnum GameState; // can be accessed across all classes

    public static bool isGameInProgress = false;

    public static int manlingsVictimized = 0;
    public static int hutsDestroyed = 0;

    public static int manlingCount = 0;
    public static int hutCount = 0;

    bool pauseKeyPressed = false;

    void Awake ()
    {
        mainCamera = GetComponent<Camera>();
    }

	// Use this for initialization
	void Start ()
    {
        GameState = CurrentGameState; // set the static field GameState to value in exposed field CurrentGameState
        StartCoroutine(GameSystemControl());
    }
	
	// Update is called once per frame
	void Update ()
    {
        switch (GameState)
        {
            case StateEnum.Play:                
                Time.timeScale = 1;
                break;

            case StateEnum.Pause:
                Time.timeScale = 0;
                break;

            case StateEnum.Menu:
                if (!isGameInProgress)
                {
                    hutsDestroyed = 0;
                    manlingsVictimized = 0;
                }
                Time.timeScale = 0;
                break;

            case StateEnum.GameOver:
                isGameInProgress = false;
                Time.timeScale = 0;
                break;

            case StateEnum.GameWon:
                Time.timeScale = 0;
                break;
        }

        if (hutCount == 0 && manlingCount == 0)
        {
            GameStateManager.GameState = StateEnum.GameWon;
        }
    }

    IEnumerator GameSystemControl()
    {
        while (true)
        {
            if (Input.anyKeyDown && InputControl.InputTriggerPause > 0)
            {
                pauseKeyPressed = true;
            }
            else
            {
                pauseKeyPressed = false;
            }

            yield return StartCoroutine(Pause());
        }
    }

    IEnumerator Pause()
    {
        if (pauseKeyPressed)
        {            
            Time.timeScale = 0;
            GameState = StateEnum.Pause;                 
        }

        yield break;
    }
}
