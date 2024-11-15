using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IconManager : MonoBehaviour
{
    // Prefab of the window
    public GameObject windowPrefab; 
    
    // Hover settings
    private SpriteRenderer spriteRenderer;
    public Color normalColor = Color.white;
    public Color hoverColor = new Color(0.8f, 0.8f, 0.8f, 1f);

    
    public string sceneName; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = normalColor;
        sceneName = "Ins"; 

        if (windowPrefab != null)
        {
            windowPrefab.SetActive(false); // Ensure window starts hidden
        }
    }

    // Mouse hover effects
    void OnMouseEnter()
    {
        spriteRenderer.color = hoverColor;
    }

    void OnMouseExit()
    {
        spriteRenderer.color = normalColor;
    }

    void OnMouseDown()
    {
        Debug.Log("Mouse clicked on icon");
        OpenWindow();
    }

    void OpenWindow()
    {
        if (windowPrefab != null)
        {
            Debug.Log("Toggling window visibility");
            windowPrefab.SetActive(!windowPrefab.activeSelf);

            if (windowPrefab.activeSelf)
            {
                LoadScene();
            }
        }
        else
        {
            Debug.LogError("Window prefab is not assigned!");
        }
    }

    private void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            Debug.Log("Loading scene: " + sceneName);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is empty or not assigned!");
        }
    }
}
