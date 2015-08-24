using UnityEngine;
using System.Collections;

public enum VictimState
{
    Idle,
    Walk,
    Run,
    Die
}

public class Victim : MonoBehaviour
{
    public VictimState ThisVictimState = VictimState.Idle;
    public float walkingSpeed;
    public float runningSpeed;

    Rigidbody2D victimRb2D;
    Animator victimAnim;
    StateMachineBehaviour victimDeadSMB;

    void Awake()
    {
        victimRb2D = GetComponent<Rigidbody2D>();
        victimAnim = GetComponent<Animator>();
        GameStateManager.manlingCount++;
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Walk());
    }

    void Update()
    {
        if (GameStateManager.GameState == StateEnum.Play)
        {
            switch (ThisVictimState)
            {
                case VictimState.Idle:
                    victimAnim.SetTrigger("isIdling");
                    break;

                case VictimState.Walk:
                    victimAnim.SetTrigger("isWalking");
                    break;

                case VictimState.Run:
                    victimAnim.SetTrigger("isRunning");
                    break;

                case VictimState.Die:                    
                    victimAnim.SetTrigger("isDying");
                    break;
            }

            if (ThisVictimState == VictimState.Die)
            {
                
            }
        }
    }

    void FixedUpdate()
    {
        if (GameStateManager.GameState == StateEnum.Play)
        {
            if (ThisVictimState != VictimState.Die)
            {
                if (victimRb2D.velocity.x > 0.1f)
                {
                    transform.localRotation = Quaternion.Euler(0, 180f, 0);
                    if (victimRb2D.velocity.x > runningSpeed)
                    {
                        ThisVictimState = VictimState.Run;
                    }
                    else
                    {
                        ThisVictimState = VictimState.Walk;
                    }
                }
                else if (victimRb2D.velocity.x < -0.1f)
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    if (victimRb2D.velocity.x < -runningSpeed)
                    {
                        ThisVictimState = VictimState.Run;
                    }
                    else
                    {
                        ThisVictimState = VictimState.Walk;
                    }
                }
                else if (Mathf.Approximately(victimRb2D.velocity.x, 0))
                {
                    ThisVictimState = VictimState.Idle;
                }
            }
        }
    }    

    void OnDestroy()
    {
        GameStateManager.manlingCount--;
    }

    IEnumerator Walk()
    {
        while (true)
        {
            if (GameStateManager.GameState == StateEnum.Play)
            {
                victimRb2D.velocity = Vector3.right * (Random.value >= 0.5f ? -1 : 1) * walkingSpeed;

                yield return new WaitForSeconds(Random.Range(0.5f, 5f));
            }

            yield return new WaitForFixedUpdate();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameStateManager.GameState == StateEnum.Play)
        {
            if (collision.collider.tag == "Fire")
            {                
                ThisVictimState = VictimState.Die;
            }
        }
    }
}
