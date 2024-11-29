using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsManage : MonoBehaviour
{
    public GameObject ins; // Corrected the casing for "GameObject"

    // Start is called before the first frame update
    void Start()
    {
        // Optional: Add null check for the GameObject
        if (ins == null)
        {
            Debug.LogError("Ins GameObject is not assigned in the Inspector!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (LogInManager.HasLoggedInBefore) // Access the static boolean
        {
            if (ins != null) // Ensure the GameObject is assigned
            {
                ins.tag = "04 INS";
            }
            else
            {
                Debug.LogError("Ins GameObject is null. Please assign it in the Inspector.");
            }
        }
    }
}
