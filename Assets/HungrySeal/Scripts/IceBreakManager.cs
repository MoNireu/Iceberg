using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBreakManager : MonoBehaviour
{

    public Renderer renderer;
    [SerializeField] public float destroyPercentage;
    private float randomDestroyPercentage;

    private float alpha;

    // Start is called before the first frame update
    void Start()
    {
        renderer = this.gameObject.GetComponent<Renderer>();
        alpha = 1f;
        randomDestroyPercentage = Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        // 투명도가 0이 되면 빙하 부수기.
        if (alpha <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" && randomDestroyPercentage <= destroyPercentage)
        {
            StartCoroutine("FadeAndDestroy");
        }

        if (collider.tag == "DestroyIce")
        {
            StartCoroutine("FadeAndDestroy");
        }
    }

    IEnumerator FadeAndDestroy()
    {
        for (float f = 1; f >= 0; f -= 3f * Time.deltaTime)
        {
            if (f <= 0.05)
            {
                alpha = 0;
            }
            else
            {
                alpha = f;
            }
            // 투명도 감소
            Color c = renderer.material.color;
            c.a = alpha;
            renderer.material.color = c;


            // 빙하 하강.
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y - (1f * Time.deltaTime),
                transform.position.y);
            yield return null;
        }
    }
}
