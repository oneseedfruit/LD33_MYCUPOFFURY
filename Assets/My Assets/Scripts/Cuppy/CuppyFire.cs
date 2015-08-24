using UnityEngine;
using System.Collections;

public class CuppyFire : MonoBehaviour
{
    public GameObject cuppySmoke;

    GameObject smoke1;
    GameObject smoke2;
    GameObject smoke3;

    void Awake ()
    {
        smoke1 = Instantiate<GameObject>(cuppySmoke);
        smoke2 = Instantiate<GameObject>(cuppySmoke);
        smoke3 = Instantiate<GameObject>(cuppySmoke);

        smoke1.transform.position = Vector3.up * 5000f;
        smoke2.transform.position = Vector3.up * 5000f;
        smoke3.transform.position = Vector3.up * 5000f;
    }

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Smoking());
	}
	
	IEnumerator Smoking ()
    {
        while (true)
        {
            if (GameStateManager.GameState == StateEnum.Play)
            {                
                smoke1.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                smoke2.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                smoke3.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            }

            yield return new WaitForSeconds(Random.Range(0f, 0.5f));

            smoke1.transform.position = Vector3.up * 5000f;
            smoke2.transform.position = Vector3.up * 5000f;
            smoke3.transform.position = Vector3.up * 5000f;

            yield return new WaitForEndOfFrame();
        }
    }
}
