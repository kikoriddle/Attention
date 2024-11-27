using UnityEngine;

public class PersistentBatteryBar : MonoBehaviour
{
    private static PersistentBatteryBar instance;

    void Awake()
    {
        // Check if an instance already exists
        if (instance == null)
        {
            instance = this; // Assign the current instance
            DontDestroyOnLoad(gameObject); // Make this GameObject persistent
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameObjects
        }
    }
}
