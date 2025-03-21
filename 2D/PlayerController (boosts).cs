using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Make sure you have the following components attached to the player GameObject:
/// - Rigidbody2D
/// - BoxCollider2D
/// - SpriteRenderer
/// - Animator with the following parameters:
///    - A_Jump (Trigger)
///    - A_Run (Float)
///    - A_Attack (Trigger)
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    
    // Private variables
    private bool isGrounded;
    private Rigidbody2D rb;                 // FOR PHYSICS
    private SpriteRenderer spriteRenderer;  // FOR FLIPPING SPRITE
    private Animator animator;              // FOR ANIMATIONS
    
    void Start()
    {
        isGrounded = false;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        // Handle horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        
        // Change sprite direction based on movement
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        
        // Set A_Run parameter in animator
        animator.SetFloat("A_Run", Mathf.Abs(moveInput));
        
        // Apply velocity to the rigidbody
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        
        // Handle jumping if on ground
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            // set A_Jump Trigger
            animator.SetTrigger("A_Jump");
        }
        
        // Handle attack if left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("A_Attack");
        }
        
        // Example usage of ApplyBoost - for demonstration purposes
        // Uncomment to test with a key press
        /*
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(ApplyBoost(5f, 2f, ref moveSpeed));
        }
        */
    }
    
    /// <summary>
    /// Temporarily boosts a player stat for a specified duration.
    /// </summary>
    /// <param name="duration">How long the boost should last in seconds</param>
    /// <param name="boost">The multiplier to apply to the stat (2.0f would double it)</param>
    /// <param name="player_stat">Reference to the stat being modified</param>
    /// <returns>IEnumerator for coroutine handling</returns>
    public IEnumerator ApplyBoost(float duration, float boost, ref float player_stat)
    {
        // Store the original value
        float originalValue = player_stat;
        
        // Apply the boost
        player_stat *= boost;
        
        // Optional: Add visual feedback that boost is active
        // For example, you could change the player's color
        // spriteRenderer.color = Color.yellow;
        
        // Wait for the duration
        yield return new WaitForSeconds(duration);
        
        // Restore the original value
        player_stat = originalValue;
        
        // Optional: Reset visual feedback
        // spriteRenderer.color = Color.white;
    }
    
    // Check collisions with objects tagged "ground"
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    
    // Use trigger collisions for Coins and Enemies
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            // Pick up coin by destroying the coin object
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            // Reset the current level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        // Example: Apply a speed boost when picking up a power-up
        else if (other.CompareTag("SpeedBoost"))
        {
            StartCoroutine(ApplyBoost(5f, 2f, ref moveSpeed));
            Destroy(other.gameObject);
        }
    }
}
