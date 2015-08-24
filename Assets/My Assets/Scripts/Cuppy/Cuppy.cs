using UnityEngine;
using System.Collections;

public enum PlayerMoveState
{
    Idling,
    Moving
}

public class Cuppy : MonoBehaviour
{
    public static PlayerMoveState CuppyMoveState = PlayerMoveState.Idling;

    void Awake()
    {

    }

    void FixedUpdate()
    {
        if (GameStateManager.GameState == StateEnum.Play)
        {

        }
    }
    void Update()
    {
        if (GameStateManager.GameState == StateEnum.Play)
        {
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (GameStateManager.GameState == StateEnum.Play)
        {
            if (col.tag == "Floor")
            {
                GameStateManager.GameState = StateEnum.GameOver;
            }
        }
    }

}
