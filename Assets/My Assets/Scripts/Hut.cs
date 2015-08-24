using UnityEngine;
using System.Collections;

public class Hut : MonoBehaviour
{
    public Fire hutFire;

    BoxCollider2D[] hutBoxCols2D;
    GameObject hutOnFire;
    SpriteRenderer[] hutSRs;
    SpriteRenderer hutOnFireSR;

    int health = 3;
    bool isBurning = false;

    void Awake()
    {
        hutBoxCols2D = GetComponents<BoxCollider2D>();
        hutSRs = GetComponentsInChildren<SpriteRenderer>();
        GameStateManager.hutCount++;
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Burn());
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            isBurning = true;
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Fire")
        {
            health--;
        }
    }

    void OnDestroy ()
    {
        GameStateManager.hutCount--;
    }

    IEnumerator Burn ()
    {
        while (true)
        {
            if (GameStateManager.GameState == StateEnum.Play)
            {
                if (isBurning)
                {
                    if (hutOnFire == null)
                    {
                        hutOnFire = Instantiate<Fire>(hutFire).gameObject;
                        hutOnFire.GetComponent<Fire>().enabled = false;
                        Rigidbody2D hutOnFireRb2D = hutOnFire.GetComponent<Rigidbody2D>();                        
                        hutOnFireRb2D.constraints = RigidbodyConstraints2D.FreezePosition;
                        hutOnFireRb2D.mass = 0;
                        hutOnFireRb2D.gravityScale = 0;
                        hutOnFireRb2D.Sleep();

                        Collider2D hutOnFireCol2D = hutOnFire.GetComponent<Collider2D>();
                        hutOnFireCol2D.enabled = false;
                    }
                    hutOnFire.transform.position = transform.localPosition;
                    isBurning = false;
                    StartCoroutine(Burning());
                    yield break;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Burning ()
    {
        while (true)
        {
            if (GameStateManager.GameState == StateEnum.Play)
            {
                float i = 0f;
                while (hutOnFire.transform.localScale.x < 1.5f)
                {
                    hutOnFire.transform.localScale = Vector2.one * i;

                    i += Time.deltaTime;                   

                    yield return new WaitForEndOfFrame();
                }

                if (hutOnFire.transform.localScale.x >= 1.5f)
                {
                    hutOnFireSR = hutOnFire.GetComponent<SpriteRenderer>();
                    StartCoroutine(Burnt());                    
                    yield break;
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Burnt ()
    {
        while (true)
        {
            if (GameStateManager.GameState == StateEnum.Play)
            {
                float i = 1;
                while (hutOnFireSR.color.a > 0)
                {
                    i -= 2f * Time.deltaTime;

                    hutOnFireSR.color = new Color(1, 1, 1, i);
                    for (int j = 0; j < hutSRs.Length; j++)
                    {
                        hutSRs[j].color = hutOnFireSR.color;
                        yield return new WaitForEndOfFrame();
                    }

                    yield return new WaitForEndOfFrame();
                }

                if (hutOnFireSR.color.a < 1)
                {
                    GameStateManager.hutsDestroyed++;
                    DestroyImmediate(hutOnFire.gameObject);
                    DestroyImmediate(gameObject);
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
