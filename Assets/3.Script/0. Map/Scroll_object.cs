using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll_object : MonoBehaviour
{
    public float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
