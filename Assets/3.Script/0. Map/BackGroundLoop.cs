using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    private float height;


    private void Awake()
    {
        BoxCollider2D boxCollider = transform.GetComponent<BoxCollider2D>();
        height = boxCollider.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y-3f <= -height)
        {
            Reposition();
        }
    }

    public void Reposition()
    {
        Vector2 offset = new Vector2(0, height * 1.55f);
        transform.position = (Vector2)transform.position + offset;
    }
}
