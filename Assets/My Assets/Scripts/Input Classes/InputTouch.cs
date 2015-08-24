using UnityEngine;
using System.Collections;

public class InputTouch : IInput 
{
    public float InputReturn
    {
        get
        {
            if (Input.touchSupported && Input.touchCount > 0)
            {
                if (Input.GetTouch(0).deltaPosition.x >= 1f && Mathf.Abs(Input.GetTouch(0).deltaPosition.x) <= 5f)
                {
                    return 1;
                }
                else if (Input.GetTouch(0).deltaPosition.x <= -1f && Mathf.Abs(Input.GetTouch(0).deltaPosition.x) <= 5f)
                {
                    return -1;
                }

                if (Input.GetTouch(0).deltaPosition.y >= 1f && Mathf.Abs(Input.GetTouch(0).deltaPosition.y) <= 5f)
                {
                    return 1;
                }
                else if (Input.GetTouch(0).deltaPosition.y <= -1f && Mathf.Abs(Input.GetTouch(0).deltaPosition.y) <= 5f)
                {
                    return -1;
                }
            }

            return 0;
        }
    }
}
