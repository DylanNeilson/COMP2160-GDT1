using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f; // Enemy movement speed.
    private int direction = 1; // 1 = right, -1 = left.

    void Update()
    {
        // Calculate the new position for the enemy based on its current position, speed, direction, and frame time.
        Vector3 newPosition = transform.position + Vector3.right * speed * direction * Time.deltaTime;

        // Check if the enemy has reached the edge of the screen.
        if (newPosition.x < -ScreenWidth() / 2f || newPosition.x > ScreenWidth() / 2f)
        {
            // Reverse the direction, if the enemy has gone too far left or right.
            direction *= -1;
        }
        // Update the enemy's position.
        transform.position = newPosition;
    }
    // Helper function to calculate the screen width.
    float ScreenWidth()
    {
        return Camera.main.orthographicSize * 2f * Camera.main.aspect;
    }
}