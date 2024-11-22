using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{

    public static int totalClickCount = 0;

    private bool isClicked = false; // Ensures the object is clicked only once

    void Start()
    {
        // Ensure the GameObject has a Collider
        if (GetComponent<Collider>() == null)
        {
            Debug.LogError($"GameObject {gameObject.name} does not have a Collider! Please add one.");
        }
    }

    void OnMouseDown()
    {
        if (!isClicked)
        {
            isClicked = true; // Mark as clicked
            totalClickCount++; // Increment the shared click count

            Debug.Log($"GameObject {gameObject.name} clicked! Total clicks: {totalClickCount}");

            // Turn off the GameObject
            gameObject.SetActive(false);
        }
    }
}


