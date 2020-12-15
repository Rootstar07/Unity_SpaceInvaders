using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{ 

    public float speed = 10;

    public Rigidbody2D rigidbody;

    public Sprite startingImage;
    public Sprite altImage;

    private SpriteRenderer spriterenderer;

    public float secBeforeSpriteChange = 0.5f;

    public GameObject alinebullet;

    public float minFireRate = 1.0f;
    public float maxFireRate = 3.0f;
    public float baseFireWaitTime = 3.0f;

    public Sprite explodeShipImage;

    // Start is called before the first frame update
    void Start()
    {

        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(1, 0) * speed;

        spriterenderer = GetComponent<SpriteRenderer>();

        StartCoroutine ((IEnumerator)changeAlienSprite());

        baseFireWaitTime += Random.Range
            (minFireRate, maxFireRate);

    }

    void Turn(int dir)
    {
        Vector2 newVelocity = rigidbody.velocity;
        newVelocity.x = speed * dir;
        rigidbody.velocity = newVelocity;
    }

    void MoveDown()
    {
        Vector2 position = transform.position;
        position.y = -1;
        transform.position = position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "LeftWall")
        {
            Turn(1);
            MoveDown();
        }

        if (collision.gameObject.name == "RightWall")
        {
            Turn(-1);
            MoveDown();
        }

        if (collision.gameObject.tag == "Bullet")
        {
            SoundManager.Instance.PlayOneShot
                (SoundManager.Instance.alienDies);
            Destroy(gameObject);

        }
    }



    public IEnumerable changeAlienSprite()
    {
        while (true)
        {
            if (spriterenderer.sprite == startingImage)
            {
                spriterenderer.sprite = altImage;
                SoundManager.Instance.PlayOneShot
                    (SoundManager.Instance.alienBuzz1);
            }
            else
            {
                spriterenderer.sprite = startingImage;
                SoundManager.Instance.PlayOneShot
                    (SoundManager.Instance.alienBuzz2);
            }

            yield return new WaitForSeconds(secBeforeSpriteChange);

        }
    }

    private void FixedUpdate()
    {
        if(Time.time > baseFireWaitTime)
        {
            baseFireWaitTime += Random.Range
            (minFireRate, maxFireRate);

            Instantiate(alinebullet, transform.position, 
                Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SoundManager.Instance.PlayOneShot
                (SoundManager.Instance.shipexplosion);

            collision.GetComponent<SpriteRenderer>().sprite = explodeShipImage;
            Destroy(gameObject);
            Object.Destroy(collision.gameObject);
        }
    }

}
