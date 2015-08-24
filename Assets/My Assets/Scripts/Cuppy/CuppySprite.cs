using UnityEngine;
using System.Collections;

public class CuppySprite : MonoBehaviour
{
    Rigidbody2D cuppyRb2D;
    Animator cuppyAnim;
    
    void Awake ()
    {
        cuppyRb2D = GetComponentInParent<Rigidbody2D>();
        cuppyAnim = GetComponentInParent<Animator>();
    }

	void FixedUpdate ()
    {
        if (GameStateManager.GameState == StateEnum.Play)
        {
            if (cuppyRb2D.velocity.x > 0.5f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (cuppyRb2D.velocity.x < -0.5f)
            {
                transform.localRotation = Quaternion.Euler(0, 180f, 0);
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GameStateManager.GameState == StateEnum.Play)
        {
            switch (Gun.CuppyGunAttackState)
            {
                case PlayerAttackState.Attacking:
                    cuppyAnim.SetBool("isAngry", true);
                    break;

                case PlayerAttackState.Idling:
                    cuppyAnim.SetBool("isAngry", false);
                    break;
            }
        }
    }
}
