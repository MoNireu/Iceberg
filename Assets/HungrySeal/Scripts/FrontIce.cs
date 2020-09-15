using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontIce : MonoBehaviour
{

    public Renderer renderer;

    private float alpha;

    // Start is called before the first frame update
    void Start()
    {
        renderer = this.gameObject.GetComponent<Renderer>();
        alpha = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (alpha <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            Debug.Log("Collide");
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
            Color c = renderer.material.color;
            c.a = alpha;
            renderer.material.color = c;
            Debug.Log(f);
            yield return null;
        }        
        
    }
}
