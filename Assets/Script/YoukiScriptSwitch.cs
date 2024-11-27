using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoukiScriptSwitch : MonoBehaviour
{
    
    public GameObject picA1; // First picture
    public GameObject picA2; // Second picture
    public GameObject picA3; // Third picture

    private GameObject[] pictures; // Array to store the pictures
    private int currentIndex = 0; // Tracks the current picture index

    void Start()
    {
        // Initialize the pictures array
        pictures = new GameObject[] { picA1, picA2, picA3 };

        // Ensure only the first picture is active at the start
        ActivatePicture(0);
    }

    public void SwitchToNextPicture()
    {
        // Deactivate the current picture
        pictures[currentIndex].SetActive(false);

        // Increment the index and wrap around if necessary
        currentIndex = (currentIndex + 1) % pictures.Length;

        // Activate the new picture
        pictures[currentIndex].SetActive(true);
    }

    public void SwitchToPreviousPicture()
    {
        // Deactivate the current picture
        pictures[currentIndex].SetActive(false);

        // Decrement the index and wrap around if necessary
        currentIndex = (currentIndex - 1 + pictures.Length) % pictures.Length;

        // Activate the new picture
        pictures[currentIndex].SetActive(true);
    }

    private void ActivatePicture(int index)
    {
        // Deactivate all pictures
        foreach (GameObject picture in pictures)
        {
            if (picture != null) picture.SetActive(false);
        }

        // Activate the specified picture
        if (pictures[index] != null) pictures[index].SetActive(true);
    }
}

