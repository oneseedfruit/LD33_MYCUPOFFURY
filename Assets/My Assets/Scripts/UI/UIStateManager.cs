using UnityEngine;
using System.Collections;

public class UIStateManager : MonoBehaviour
{
    Canvas UICanvas;

	// Use this for initialization
	void Awake () {
        UICanvas = GetComponentInChildren<Canvas>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    switch (GameStateManager.GameState)
        {
            case StateEnum.Menu:                
                UICanvas.enabled = true;
                break;
            case StateEnum.Pause:
                UICanvas.enabled = true;
                break;
            case StateEnum.Play:
                UICanvas.enabled = false;
                break;
            case StateEnum.GameOver:
                UICanvas.enabled = true;
                break;
            case StateEnum.GameWon:
                UICanvas.enabled = true;
                break;
        }
	}

    void StartGame ()
    {
        if (GameStateManager.GameState != StateEnum.Play && GameStateManager.GameState != StateEnum.GameOver)
        {
            if (!GameStateManager.isGameInProgress)
            {
                GameStateManager.isGameInProgress = true;
            }
            GameStateManager.GameState = StateEnum.Play;
        }
        else if (GameStateManager.GameState == StateEnum.GameOver || GameStateManager.GameState == StateEnum.GameWon)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    void QuitGame ()
    {
        Application.Quit();
    }
}
