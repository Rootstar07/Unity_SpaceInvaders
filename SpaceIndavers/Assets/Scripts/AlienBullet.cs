using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : MonoBehaviour
{

    private Rigidbody2D rigidbody;

    public float speed = 30;

    public Sprite explodedShipImage;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.velocity = Vector2.down * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if (collision.tag == "Shield")
        {
            Destroy(gameObject);

            Object.Destroy(collision.gameObject);
        }

        if (collision.tag == "Player")
        {
            SoundManager.Instance.PlayOneShot
                (SoundManager.Instance.shipexplosion);

            collision.GetComponent<SpriteRenderer>().sprite =
                explodedShipImage;

            Destroy(gameObject);

            Object.Destroy(collision.gameObject, 0.5f);
        }
    }


    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
