using UnityEngine;

public class TargetScript : MonoBehaviour
{
    // add audio source
    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // OnCollisionEnter is called when the object collides with another object
    void OnCollisionEnter(Collision collision)
    {
        // If the object that collided with this object has the tag "Projectile"
        if (collision.gameObject.tag == "Projectile")
        {
            // Destroy the object that collided with this object
            Destroy(collision.gameObject);
            // Destroy this object
            Destroy(gameObject);

            if (audioSource != null)
            {
                // Play the audio source
                audioSource.Play();
            }
        }
    }
}
