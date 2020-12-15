using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 30;

    private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {

        rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.velocity = Vector2.up * speed;

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
