using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f; // Movement speed.
    [SerializeField] private GameObject hook; // Reference to the hook object.
    
    private Vector2 moveInput; // Input for player movement.
    private Rigidbody2D rb; // Player's Rigidbody2D component.
    private SpriteRenderer spriteRenderer; // Player's SpriteRenderer component.
    private bool isFacingRight = true; // Flag indicating if the player is facing right.
    private bool isHookDeployed = false; // Flag indicating if the hook is deployed.
    private GameObject currentHook; // Reference to the currently deployed hook.

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the player's Rigidbody2D component.
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the player's SpriteRenderer component.
    }

    // FixedUpdate is called at a fixed time interval
    void FixedUpdate()
    {
        if (!isHookDeployed)
        {
            // Handle player movement if the hook is not deployed.
            HandleMovement();
        }
        // Check if the hook is deployed and if it reached a certain height, destroy it.
        if (isHookDeployed && currentHook != null && currentHook.transform.position.y > 8.5)
        {
            Destroy(currentHook);
            isHookDeployed = false;
        }
    }

    // Handle player movement and collision with screen edges.
    void HandleMovement()
    {
        // Calculate the new position based on the player's current position, input, movement speed, and frame time.
        Vector2 newPosition = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;
        // Calculate half of the screen width.
        float halfScreenWidth = ScreenWidth() / 2f;
        // Clamp the new position within the screen boundaries.
        newPosition.x = Mathf.Clamp(newPosition.x, -halfScreenWidth, halfScreenWidth);
        // Check if the absolute difference between the new position and the current position is less than half the screen width.
        // This prevents the player from moving too far to the left or right in one frame.
        if (Mathf.Abs(newPosition.x - rb.position.x) < halfScreenWidth)
        {
            // Move the player to the new position using Rigidbody2D's MovePosition method.
            rb.MovePosition(newPosition);
        }
        // Flip the player sprite if moving right and not already facing right.
        if (moveInput.x > 0 && !isFacingRight)
        {
            FlipSprite();
        }
        // Flip the player sprite if moving left and not already facing left.
        else if (moveInput.x < 0 && isFacingRight)
        {
            FlipSprite();
        }
    }
    // Input callback for player movement.
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    // Update is called once per frame
    void Update()
    {
        // Deploy the hook when the space key or shift key is pressed.
        if (Keyboard.current.spaceKey.wasPressedThisFrame  && !isHookDeployed || Keyboard.current.shiftKey.wasPressedThisFrame && !isHookDeployed)
        {
            DeployHook();
        }
        // Retract the hook when the space key or shift key is pressed while the hook is deployed.
        else if (Keyboard.current.spaceKey.wasPressedThisFrame  && isHookDeployed || Keyboard.current.shiftKey.wasPressedThisFrame && isHookDeployed)
        {
            RetractHook();
        }
        // Check if there are no hooks in the scene to reset the deployment flag.
        if (GameObject.FindGameObjectWithTag("Hook") == null)
        {
            isHookDeployed = false;
        }
    }
    // Deploy the hook when called.
    void DeployHook()
    {
        // Check if there is already a hook in the scene.
        if (GameObject.FindGameObjectWithTag("Hook") == null)
        {
            // Instantiate the hook object at the player's position with an offset.
            currentHook = Instantiate(hook, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
            isHookDeployed = true; // Set the hook deployment flag.

            // Give the hook initial downward velocity.
            Rigidbody2D hookRb = currentHook.GetComponent<Rigidbody2D>();
            hookRb.velocity = Vector2.down * 10f;
        }
    }
    // Retract the hook by reversing its velocity when called.
    void RetractHook()
    {
        if (currentHook != null)
        {
            Rigidbody2D hookRb = currentHook.GetComponent<Rigidbody2D>();
            hookRb.velocity = -hookRb.velocity;
        }
    }
    // Flip the player sprite to change direction.
    void FlipSprite()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
    // Helper function to calculate the screen width in world units.
    float ScreenWidth()
    {
        return Camera.main.orthographicSize * 2f * Camera.main.aspect;
    }
}