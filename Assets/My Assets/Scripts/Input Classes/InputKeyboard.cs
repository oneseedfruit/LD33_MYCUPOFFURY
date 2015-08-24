using UnityEngine;
using System.Collections;

public class InputKeyboard : IInput 
{
    float inputReturn = 0;

    public float InputReturn
    {
        get { return inputReturn; }
    }

    public InputKeyboard (float iReturn)
    {
        inputReturn = iReturn;
    }
}
