using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // speed
    public float speed = 7.0f;
    // jump power
    public float jumpPower = 5.0f;
    // 2D rigidbody
    public Rigidbody2D rb;
    // isGrounded
    public bool isGrounded = true;

    // ScoreKeeper
    public ScoreKeeper scoreKeeper;

    // // LifeKeeper
    // public LifeKeeper lifeKeeper;

    // Start is called before the first frame update
    void Start()
    {
        // get the rigidbody
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // move the player
        float x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(x * speed, rb.velocity.y);

        // if the player is grounded and the player presses the space key
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // jump
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isGrounded = false;
        }

    }

    // on collision enter
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if the player is grounded
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

        // if the player collides with the coin
        if (collision.gameObject.tag == "Coin")
        {
            // destroy the coin
            Destroy(collision.gameObject);
            // increase the score
            scoreKeeper.AddScore(1);
        }

        // // if the player collides with the enemy
        // if (collision.gameObject.tag == "Enemy")
        // {
        //     // decrease the life
        //     lifeKeeper.DecreaseLife(1);
        // }

        // // if the player collides with the enemy projectile
        // if (collision.gameObject.tag == "EnemyProjectile")
        // {
        //     // decrease the life
        //     lifeKeeper.DecreaseLife(1);
        // }
    }
}
