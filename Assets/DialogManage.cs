using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManage : MonoBehaviour
{
    // References to the three GameObjects
    public GameObject objectA;
    public GameObject objectB;
    public GameObject objectC;
    public GameObject objectD;

    // Boolean variables to control which object is active
    public bool turnA = false;
    public bool turnB = false;
    public bool turnC = false;
    public bool turnD = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize all objects as inactive
        objectA.SetActive(false);
        objectB.SetActive(false);
        objectC.SetActive(false);
        objectD.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check each boolean and set the corresponding GameObject active or inactive
        if (FinalMessageScript.turnA)
        {
            objectA.SetActive(true);
        }
        else
        {
            objectA.SetActive(false);
        }

        if (FinalMessageScript.turnD)
        {
            objectB.SetActive(true);
        }
        else
        {
            objectB.SetActive(false);
        }

        if (FinalMessageScript.turnC)
        {
            objectC.SetActive(true);
        }
        else
        {
            objectC.SetActive(false);
        }
         if (FinalMessageScript.turnB)
        {
            objectD.SetActive(true);
        }
        else
        {
            objectD.SetActive(false);
        }
    }
}
