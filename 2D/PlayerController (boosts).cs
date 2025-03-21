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
    private float originalMoveSpeed;        // To store original value for boosts
    
    void Start()
    {
        isGrounded = false;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        originalMoveSpeed = moveSpeed;      // Store the original value
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
            StartCoroutine(ApplyMoveSpeedBoost(5f, 2f));
        }
        */
    }
    
    /// <summary>
    /// Temporarily boosts the player's move speed for a specified duration.
    /// </summary>
    /// <param name="duration">How long the boost should last in seconds</param>
    /// <param name="boost">The multiplier to apply to the speed (2.0f would double it)</param>
    public IEnumerator ApplyMoveSpeedBoost(float duration, float boost)
    {
        // Apply the boost
        moveSpeed = originalMoveSpeed * boost;
        
        // Optional: Add visual feedback that boost is active
        // For example, you could change the player's color
        // spriteRenderer.color = Color.yellow;
        
        // Wait for the duration
        yield return new WaitForSeconds(duration);
        
        // Restore the original value
        moveSpeed = originalMoveSpeed;
        
        // Optional: Reset visual feedback
        // spriteRenderer.color = Color.white;
    }
    
    /// <summary>
    /// Temporarily boosts the player's jump force for a specified duration.
    /// </summary>
    /// <param name="duration">How long the boost should last in seconds</param>
    /// <param name="boost">The multiplier to apply to the jump force (2.0f would double it)</param>
    public IEnumerator ApplyJumpBoost(float duration, float boost)
    {
        float originalJumpForce = jumpForce;
        jumpForce *= boost;
        
        yield return new WaitForSeconds(duration);
        
        jumpForce = originalJumpForce;
    }
    
    /// <summary>
    /// Generic method to apply temporary boost to any player stat.
    /// </summary>
    /// <param name="duration">How long the boost should last in seconds</param>
    /// <param name="boost">The multiplier to apply to the stat</param>
    /// <param name="statName">The name of the stat to boost ("moveSpeed" or "jumpForce")</param>
    public IEnumerator ApplyBoost(float duration, float boost, string statName)
    {
        if (statName == "moveSpeed")
        {
            float original = moveSpeed;
            moveSpeed *= boost;
            
            yield return new WaitForSeconds(duration);
            
            moveSpeed = original;
        }
        else if (statName == "jumpForce")
        {
            float original = jumpForce;
            jumpForce *= boost;
            
            yield return new WaitForSeconds(duration);
            
            jumpForce = original;
        }
        // Add more stats as needed
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
            StartCoroutine(ApplyMoveSpeedBoost(5f, 2f));
            Destroy(other.gameObject);
        }
        // Example: Apply a jump boost when picking up a power-up
        else if (other.CompareTag("JumpBoost"))
        {
            StartCoroutine(ApplyJumpBoost(5f, 1.5f));
            Destroy(other.gameObject);
        }
    }
}
