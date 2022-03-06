using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.TransformDirection(new Vector3(speed, 0f, 0f));
        if(transform.position.x > 10.5f || transform.position.y > 6 || transform.position.y < -6) {
            transform.position = new Vector3(-10.5f + Random.Range(-4f, -1f), Random.Range(-3f, 3f), 0f);
            transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-15f, 15f));
        }
    }
}
