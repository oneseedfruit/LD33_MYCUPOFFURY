using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartButton : MonoBehaviour
{
    Text startText;

    void Awake ()
    {
        startText = GetComponentInChildren<Text>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (GameStateManager.GameState == StateEnum.Pause && GameStateManager.isGameInProgress)
        {
            startText.text = "Resume";
        }
        else if (GameStateManager.GameState == StateEnum.GameOver)
        {
            startText.text = "Try again?";
        }
        else
        {
            startText.text = "Start";
        }
	}
}
