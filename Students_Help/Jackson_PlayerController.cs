using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpPower = 5f;
    public GameObject attackSprite;
    public float attackDuration = 2f;
    private Rigidbody2D _rb;
    private float originalScaleX;
    private bool _isGrounded;
    public GameObject bullet;
    private bool hasGun = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        attackSprite.SetActive(false);
        originalScaleX = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();

        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(PerformMeleeAttck());
        }

        if (Input.GetMouseButtonDown(0) & hasGun)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position, ForceMode2D.Impulse);
    }


    private IEnumerator PerformMeleeAttck()
    {
        attackSprite.SetActive(true);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Destroy(collider.gameObject);
            }
        }
        yield return new WaitForSeconds(attackDuration);
            attackSprite.SetActive(false);
    }


        void Movement()
    {
        float horizonalInput = Input.GetAxis("Horizontal");

        if (horizonalInput > 0)
        {
            transform.localScale = new UnityEngine.Vector3(Math.Abs(originalScaleX), transform.localScale.y, transform.localScale.z); 
        }
        else if (horizonalInput < 0)
        {
            transform.localScale = new UnityEngine.Vector3(-Math.Abs(originalScaleX), transform.localScale.y, transform.localScale.z); 
        }

        transform.Translate(UnityEngine.Vector2.right * horizonalInput * _speed * Time.deltaTime);
    }
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower); 
            _isGrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == "Ground")
        {
            _isGrounded = true;
        }
        
        if(collision.gameObject.tag == "weapon")
        {
            hasGun = true;
            Destroy(collision.gameObject);
     
        }

          if(collision.gameObject.tag == "mask")
        {
            
            Destroy(collision.gameObject);
        }
    }

}
