using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButtonScript : MonoBehaviour
{
    // Start is called before the first frame update

    public string sceneName;

    void Start()
    {
        sceneName = "01 Main Window";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SwitchScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            Debug.Log($"Switching to scene: {sceneName}");
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is not assigned or empty!");
        }
    }
}

