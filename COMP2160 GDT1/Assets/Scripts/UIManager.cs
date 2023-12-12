using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText; // Reference to the TextMeshProUGUI component for displaying the current score.
    [SerializeField] private TextMeshProUGUI highScoreText; // Reference to the TextMeshProUGUI component for displaying the high score.
    public float score = 0; // The player's current score.

    // Start is called before the first frame update
    void Start()
    {
        // Check if the references to scoreText components are null.
        if (currentScoreText == null || highScoreText == null)
        {
            enabled = false; // Disable the script if either TextMeshProUGUI component is null.
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {        
        // Update the text displaying the current score.
        currentScoreText.text = "Current Score: " + score.ToString();
        // Update the text displaying the high score.
        highScoreText.text = "High Score: " + score.ToString();
    }
}
