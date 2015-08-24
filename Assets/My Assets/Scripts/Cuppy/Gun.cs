using UnityEngine;
using System.Collections;

public enum PlayerAttackState
{
    Idling,
    Attacking
}

public class Gun : MonoBehaviour
{
    public static PlayerAttackState CuppyGunAttackState = PlayerAttackState.Idling;

    public Fire ammo;

    Rigidbody2D cuppyRb2D;
    Animator gunAnim;
    SpriteRenderer gunSR;

    void Awake ()
    {
        cuppyRb2D = GetComponentInParent<Rigidbody2D>();
        gunAnim = GetComponent<Animator>();
        gunSR = GetComponent<SpriteRenderer>();   
	}

    void Start ()
    {
        StartCoroutine(Fire());
    }

    void FixedUpdate()
    {
        if (GameStateManager.GameState == StateEnum.Play)
        {
            if (cuppyRb2D.velocity.x > 0.5f)
            {
                if (tag == "Gun Front")
                {
                    gunSR.sortingOrder = 1;
                }
                else if (tag == "Gun Back")
                {
                    gunSR.sortingOrder = -2;
                }
            }
            else if (cuppyRb2D.velocity.x < -0.5f)
            {
                if (tag == "Gun Front")
                {
                    gunSR.sortingOrder = -2;
                }
                else if (tag == "Gun Back")
                {
                    gunSR.sortingOrder = 1;
                }
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (GameStateManager.GameState == StateEnum.Play)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, AngleToFaceMouse2D()));
           
            if (InputControl.Fire.InputReturn > 0)
            {
                gunAnim.SetBool("isShooting", true);
                CuppyGunAttackState = PlayerAttackState.Attacking;
            }
            else
            {
                gunAnim.SetBool("isShooting", false);
                CuppyGunAttackState = PlayerAttackState.Idling;
            }
        }        
	}

    float AngleToFaceMouse2D()
    {
        float targetAngle = 0f;
        Vector3 mousePos = Input.mousePosition;
        Vector2 gunPos = GameStateManager.mainCamera.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - gunPos.x;
        mousePos.y = mousePos.y - gunPos.y;
        mousePos.z = 0;
        
        targetAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        

        return targetAngle;
    }

    IEnumerator Fire ()
    {
        while (true)
        {
            if (GameStateManager.GameState == StateEnum.Play)
            {
                if (CuppyGunAttackState == PlayerAttackState.Attacking)
                {
                    Fire fire = GameObject.Instantiate<Fire>(ammo);

                    fire.transform.position = new Vector2(transform.position.x, transform.position.y) + new Vector2((transform.right * 2f).x, (transform.right * 2f).y);
                    fire.fireDirection = transform.right;

                    yield return new WaitForSeconds(0.5f);
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
