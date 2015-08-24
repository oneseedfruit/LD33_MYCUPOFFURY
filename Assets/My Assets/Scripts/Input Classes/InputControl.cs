using UnityEngine;
using System.Collections;

public class InputControl : MonoBehaviour 
{
    static IInput inputValue;
        
    #region INPUT
    public static float InputTriggerPause // propery returns Input.GetAxis("Cancel") when get
    {
        get { return Input.GetAxis("Cancel"); }
    }

    public static IInput MoveHorizontal
    {
        get
        {
            if (InputTriggerPause == 0)
            {
                if (Input.touchSupported)
                {
                    inputValue = new InputTouch();
                }
                else
                {
                    inputValue = new InputKeyboard(Input.GetAxis("Horizontal"));
                }
            }

            return inputValue;
        }
    }

    public static IInput MoveVertical
    {
        get
        {
            if (InputTriggerPause == 0)
            {
                if (Input.touchSupported)
                {
                    inputValue = new InputTouch();
                }
                else
                {
                    inputValue = new InputKeyboard(Input.GetAxis("Vertical"));
                }
            }

            return inputValue;
        }
    }

    public static IInput Fire
    {
        get
        {
            if (InputTriggerPause == 0)
            {
                if (Input.touchSupported)
                {
                    inputValue = new InputTouch();
                }
                else
                {
                    inputValue = new InputKeyboard(Input.GetAxis("Fire1"));
                }
            }

            return inputValue;
        }
    }
    #endregion

    // Use this for initialization
    void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}
}
