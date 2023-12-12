using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish1 : MonoBehaviour
{
    [SerializeField] private float minSpeedVal; // Minimum speed.
    [SerializeField] private float maxSpeedVal; // Maximum speed.
    
    private float speed; // Current speed.
    private float amplitude = 0.001f; // The amplitude of vertical movement.
    private bool movingRight = true; // Indicates whether the fish is currently moving to the right.
    private Rigidbody2D rb; // Reference to the Rigidbody2D component.
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component.
    private UIManager UIManager; // Reference to the UIManager script.
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component.
        UIManager = FindObjectOfType<UIManager>(); // Find the UIManager script in the scene.
        speed = Random.Range(minSpeedVal, maxSpeedVal); // Randomly set the initial speed.
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component.
        
        // Check the initial position
        if (transform.position.x < 0)
        {
            // If on left side, start moving right
            movingRight = true;
        }
        else
        {
            // If on the right side, start moving left and flip the sprite.
            movingRight = false;
            FlipSprite();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            // Move the fish to the right if movingRight is true.
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            // Calculate vertical movement based on sine wave.
            float verticalMovement = Mathf.Sin(Time.time * speed) * amplitude;

            // Apply vertical movement to the fish.
            transform.Translate(Vector3.up * verticalMovement);

            // Destroy the fish object when it leaves the right side of the window.
            if (transform.position.x > 20)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // Move the fish to the left if movingRight is false.
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            // Calculate vertical movement based on sine wave.
            float verticalMovement = Mathf.Sin(Time.time * speed) * amplitude;

            // Apply vertical movement to the fish.
            transform.Translate(Vector3.up * verticalMovement);

            // Destroy the fish object when it leaves the left side of the window.
            if (transform.position.x < -20)
            {
                Destroy(gameObject);
            }
        }
    }
    // Function to flip the sprite.
    void FlipSprite()
    {
        spriteRenderer.flipX = !movingRight;
    }

    // OnTriggerEnter2D is called when a collider enters the fish's trigger collider.
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the "Hook" tag.
        if (other.CompareTag("Hook"))
        {
            // If UIManager is not null, increase the score by 5.
            if (UIManager != null)
            {
                UIManager.score += 5;
            }
            // Destroy the fish object.
            Destroy(gameObject);
        }
    }
}

