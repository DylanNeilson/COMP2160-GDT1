using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private Fish1 obstacle1; // The first type of fish to spawn.
    [SerializeField] private Fish2 obstacle2; // The second type of fish to spawn.
    [SerializeField] private float minStateTimerVal; // Minimum time between fish spawns.
    [SerializeField] private float maxStateTimerVal; // Maximum time between fish spawns.

    private float timer; // A timer used to control fish spawning.
    private float minYPosition = -5f; // The minimum Y position where fish can spawn.
    private float maxYPosition = 7f; // The maximum Y position where fish can spawn.

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            // Decrement the timer based on real-time.
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            // Call the Spawn method when the timer reaches or goes below 0.
            Spawn();
        }
    }

    void Spawn()
    {
        // Calculate a random Y position for Fish 1 within the specified range.
        float randomYPosition1 = Random.Range(minYPosition, maxYPosition);
        // Create a new instance of Fish1 at the calculated position.
        Fish1 newObstacle1 = Instantiate(obstacle1, new Vector3(transform.position.x, randomYPosition1, transform.position.z), Quaternion.identity);
        // Reset the timer with a random value between minStateTimerVal and maxStateTimerVal.
        timer = Random.Range(minStateTimerVal, maxStateTimerVal);
        // Calculate a random Y position for Fish 2 within the specified range.
        float randomYPosition2 = Random.Range(minYPosition, maxYPosition);
        // Create a new instance of Fish2 at the calculated position.
        Fish2 newObstacle2 = Instantiate(obstacle2, new Vector3(transform.position.x, randomYPosition2, transform.position.z), Quaternion.identity);
        // Reset the timer with a random value between minStateTimerVal and maxStateTimerVal.
        timer = Random.Range(minStateTimerVal, maxStateTimerVal);
    }
}
