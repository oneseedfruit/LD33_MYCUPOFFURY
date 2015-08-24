using UnityEngine;
using System.Collections;


public class Fire : MonoBehaviour
{
    public float speed = 5f;
    public Vector2 fireDirection;

    Rigidbody2D fireRb2D;
    SpriteRenderer fireSR;
    AudioSource fireAudioSource;

	void Awake ()
    {
        fireRb2D = GetComponent<Rigidbody2D>();
        fireSR = GetComponent<SpriteRenderer>();
        fireAudioSource = GetComponent<AudioSource>();
	}
	
    void Start ()
    {
        fireAudioSource.Play();
        StartCoroutine(FadeOutAndDestroy());
    }

	// Update is called once per frame
	void Update ()
    {
	    if (GameStateManager.GameState == StateEnum.Play)
        {
            fireRb2D.velocity = fireDirection * speed;
        }
	}

    public IEnumerator FadeOutAndDestroy()
    {
        while (true)
        {
            if (GameStateManager.GameState == StateEnum.Play)
            {
                float i = 1;

                while (fireSR.color.a > 0)
                {
                    i -= Time.deltaTime;

                    fireSR.color = new Color(1, 1, 1, i);

                    yield return new WaitForEndOfFrame();
                }

                if (fireSR.color.a < 1)
                {
                    DestroyImmediate(this.gameObject);

                    yield break;
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
