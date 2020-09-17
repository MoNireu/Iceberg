using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBreakManager : MonoBehaviour
{

    public Renderer renderer;
    [SerializeField] public float destroyPercentage;
    private float randomDestroyPercentage;

    private float alpha;
    private int hitCount;

    // Start is called before the first frame update
    void Start()
    {
        renderer = this.gameObject.GetComponent<Renderer>();
        alpha = 1f;
        randomDestroyPercentage = Random.Range(0f, 1f);
        hitCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (hitCount != 0)
        {
            fadeAndDestroy();
        }        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if ((collider.tag == "DestroyIce") ||
            (collider.tag == "Player" && randomDestroyPercentage <= destroyPercentage))
        {            
            hitCount += 1;
        }
    }


    private void fadeAndDestroy()
    {
        alpha = Mathf.MoveTowards(alpha, 0f, 3f * Time.deltaTime);

        if (alpha == 0)
        {
            Destroy(this.gameObject);
        }

        Color c = renderer.material.color;
        c.a = alpha;
        renderer.material.color = c;

        // 빙하 하강.
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y - (1f * Time.deltaTime),
            transform.position.y);
    }
}
