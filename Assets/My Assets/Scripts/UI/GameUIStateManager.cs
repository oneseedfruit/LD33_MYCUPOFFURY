using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUIStateManager : MonoBehaviour
{
    Canvas gameUICanvas;
    Text scoreText;

    void Awake ()
    {
        gameUICanvas = GetComponentInChildren<Canvas>();
        scoreText = GetComponentInChildren<Text>();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        scoreText.text = "Staw huts destroyed: " + GameStateManager.hutsDestroyed + "\n" + "Manlings Victimized: " + GameStateManager.manlingsVictimized;
	    if (GameStateManager.GameState == StateEnum.Play || GameStateManager.GameState == StateEnum.GameOver || GameStateManager.GameState == StateEnum.GameWon)
        {
            gameUICanvas.enabled = true;
            if (GameStateManager.GameState == StateEnum.GameOver)
            {
                scoreText.text = "GAME OVER!!! Staw huts destroyed: " + GameStateManager.hutsDestroyed + "\n" + "Manlings Victimized: " +
                        GameStateManager.manlingsVictimized;
            }
            else if (GameStateManager.GameState == StateEnum.GameWon)
            {
                scoreText.text = "YOU WIN!!! Staw huts destroyed: " + GameStateManager.hutsDestroyed + "\n" + "Manlings Victimized: " +
                        GameStateManager.manlingsVictimized;
            }
        }
        else
        {
            gameUICanvas.enabled = false;
        }
	}
}
