using UnityEngine;
using System.Collections;

public class Saucer : MonoBehaviour
{
    public float movingForce = 130f;
    public float defaultAngularDrag = 100f;
    public AudioClip hoverAudio;
    public AudioClip engineAudio;

    Rigidbody2D saucerRb2D;
    Animator saucerAnim;
    AudioSource saucerAudioSource;

    void Awake()
    {
        saucerRb2D = GetComponent<Rigidbody2D>();
        saucerAnim = GetComponent<Animator>();
        saucerAudioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (GameStateManager.GameState == StateEnum.Play) // if game is playing
        {
            if (InputControl.MoveHorizontal.InputReturn > 0) // if input value for horizontal axis is more than 0
            {
                saucerRb2D.AddForce(transform.right * movingForce);// move the saucer right
                saucerAnim.SetTrigger("isSpinning"); // set saucuer to spin animation
                saucerAudioSource.clip = engineAudio;
            }

            if (InputControl.MoveHorizontal.InputReturn < 0) // if input value of horizontal axis is less than 0
            {
                saucerRb2D.AddForce(-transform.right * movingForce); // move the saucer left
                saucerAnim.SetTrigger("isSpinning"); // set saucuer to spin animation
                saucerAudioSource.clip = engineAudio;
            }

            if (InputControl.MoveVertical.InputReturn > 0) // if input value of vertical axis is more than 0
            {
                saucerRb2D.AddForce(saucerRb2D.transform.up * 1.3f * movingForce); // hover the saucer
                saucerAnim.SetTrigger("isHovering"); // set saucer to hover animation
                saucerAudioSource.clip = hoverAudio;
            }

            // if no horizontal and vertical input
            if (Mathf.Approximately(InputControl.MoveVertical.InputReturn, 0) && Mathf.Approximately(InputControl.MoveHorizontal.InputReturn, 0))
            {
                saucerRb2D.drag = 10f; // set drag to 10f
                saucerAnim.SetTrigger("isIdling");  // set saucer to idle animation
            }

            // manage Cuppy's movement state
            if (Mathf.Approximately(saucerRb2D.velocity.x, 0) && Mathf.Approximately(saucerRb2D.velocity.y, 0))
            {
                Cuppy.CuppyMoveState = PlayerMoveState.Idling;
            }
            else
            {
                Cuppy.CuppyMoveState = PlayerMoveState.Moving;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            saucerRb2D.angularDrag = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            saucerRb2D.angularDrag = defaultAngularDrag;
        }
    }
}
