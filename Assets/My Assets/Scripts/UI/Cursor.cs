using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour
{        
	// Use this for initialization
	void Awake ()
    {
        if (GameStateManager.GameState == StateEnum.Play)
        {
            UnityEngine.Cursor.visible = false;
        }        
        else
        {
            UnityEngine.Cursor.visible = true;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameStateManager.GameState == StateEnum.Play)
        {
            UnityEngine.Cursor.visible = false;
            Vector3 mousePos = GameStateManager.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        }
        else
        {
            UnityEngine.Cursor.visible = true;
        }
    }
}
