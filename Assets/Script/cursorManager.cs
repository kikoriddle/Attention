using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D customCursor; // Drag your cursor texture here in the Inspector
    public Vector2 hotspot = Vector2.zero; // Optional: Adjust the cursor's click point

    void Start()
    {
        Cursor.visible = true; // Make the cursor visible
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor

        // Use a custom cursor if assigned
        if (customCursor != null)
        {
            Cursor.SetCursor(customCursor, hotspot, CursorMode.Auto);
        }

        
    }
}
