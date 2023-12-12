using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField] private float hookSpeed = 10f; // Movement speed.
    
    private Vector2 originalPosition; // The original position where the hook was spawned.
    private bool returning = false; // Indicates whether the hook is returning to its original position.
    private Rigidbody2D rb; // Reference to the Rigidbody2D component.

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component.
        originalPosition = transform.position; // Store the initial position of the hook.
        rb.velocity = Vector2.down * hookSpeed; // Set the initial velocity to move the hook downward.
    }
    // Update is called once per frame
    void Update()
    {
        if (returning && Vector2.Distance(transform.position, originalPosition) < 0.1f)
        {
            // If the hook is returning and is close enough to the original position:
            rb.velocity = Vector2.zero; // Stop the hook's movement.
            transform.position = originalPosition; // Move the hook back to its original position.
            returning = false; // Set returning to false to indicate it's no longer returning.
            Destroy(gameObject); // Destroy the hook object.
        }
    }

    // OnTriggerEnter2D is called when a collider enters the Hook's trigger collider.
    void OnTriggerEnter2D(Collider2D other)
    {
        // If the hook collides with an object with the "Boundary" or "Fish" tag:
        if (other.CompareTag("Boundary") || other.CompareTag("Fish"))
        {
            rb.velocity = -rb.velocity; // Reverse the hook's vertical velocity to make it bounce.
            returning = true; // Set returning to true to indicate the hook is returning.
        }
    }
}