using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    public float speed = 30;

    private Rigidbody2D rigidbody;

    public Sprite explodeAlienImage;

    // Start is called before the first frame update
    void Start()
    {

        rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.velocity = Vector2.up * speed;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if (collision.tag == "Aliens")
        {
            SoundManager.Instance.PlayOneShot
                (SoundManager.Instance.alienDies);

            IncreaseTextScore();

            collision.GetComponent<SpriteRenderer>().sprite =
                explodeAlienImage;

            Destroy(gameObject);

            Object.Destroy(collision.gameObject, 0.5f);

        }

        if(collision.tag == "Shield")
        {
            Destroy(gameObject);
            Object.Destroy(collision.gameObject);
        }

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void IncreaseTextScore()
    {
        var textUIComp = GameObject.Find("Score").GetComponent<Text>();

        int score = int.Parse(textUIComp.text);

        score += 10;

        textUIComp.text = score.ToString();
    }

}
