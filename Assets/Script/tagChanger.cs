using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tagChanger : MonoBehaviour
{
    public GameObject targetObject; // The GameObject whose tag we want to change

    void Start()
    {
        // Ensure targetObject is assigned
        if (targetObject == null)
        {
            Debug.LogError("targetObject is not assigned!");
        }
    }

    void Update()
    {
        // Directly access CamScriptForFinal's changeTag
        if (CamScriptForFinal.changeTag)
        {
            // Change the tag of the target object to "Message 1"
            targetObject.tag = "Message 1";
            Debug.Log($"Tag of {targetObject.name} changed to 'Message 1'");

            
        }
    }
}