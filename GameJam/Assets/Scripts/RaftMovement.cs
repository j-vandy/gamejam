using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RaftMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private int counter = 26;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.TransformDirection(new Vector3(speed, 0f, 0f));
        if((transform.position.x > 8.4f || transform.position.x < -8.4f
        || transform.position.y > 4.5f || transform.position.y < -4.5f) && counter > 25) {
            transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z - 180 + Random.Range(-30f, 30f));
            counter = 0;
        }
        counter++;
    }
}
