using UnityEngine;

public class ScriptForInsButton : MonoBehaviour
{
    // Arrays for different categories
    public GameObject love1;
    public GameObject love2;
    private GameObject[] love;

    public GameObject love11;
    public GameObject love22;
    private GameObject[] loveExtra; // Separate array for "love11" and "love22"

    public GameObject rc1;
    public GameObject rc2;
    public GameObject rc3;
    private GameObject[] rc;

    public GameObject eu1;
    public GameObject eu2;
    public GameObject eu3;
    public GameObject eu4;
    public GameObject eu5;
    private GameObject[] eu;

    public GameObject yk1;
    public GameObject yk2;
    public GameObject yk3;
    private GameObject[] yk;

    private int loveIndex = 0;    // Current index for love category
    private int loveExtraIndex = 0; // Current index for loveExtra category
    private int rcIndex = 0;      // Current index for rc category
    private int euIndex = 0;      // Current index for eu category
    private int ykIndex = 0;      // Current index for yk category

    void Start()
    {
        // Initialize the picture arrays
        love = new GameObject[] { love1, love2 };
        loveExtra = new GameObject[] { love11, love22 };
        rc = new GameObject[] { rc1, rc2, rc3 };
        eu = new GameObject[] { eu1, eu2, eu3, eu4, eu5 };
        yk = new GameObject[] { yk1, yk2, yk3 };

        // Activate the first picture for each category
        ActivatePicture(love, loveIndex);
        ActivatePicture(loveExtra, loveExtraIndex);
        ActivatePicture(rc, rcIndex);
        ActivatePicture(eu, euIndex);
        ActivatePicture(yk, ykIndex);
    }

    // LOVE Category Functions
    public void LoveSwitchToNext()
    {
        SwitchToNextPicture(love, ref loveIndex);
    }

    public void LoveSwitchToPrevious()
    {
        SwitchToPreviousPicture(love, ref loveIndex);
    }

    // LOVE EXTRA Category Functions
    public void LoveExtraSwitchToNext()
    {
        SwitchToNextPicture(loveExtra, ref loveExtraIndex);
    }

    public void LoveExtraSwitchToPrevious()
    {
        SwitchToPreviousPicture(loveExtra, ref loveExtraIndex);
    }

    // RC Category Functions
    public void RCSwitchToNext()
    {
        SwitchToNextPicture(rc, ref rcIndex);
    }

    public void RCSwitchToPrevious()
    {
        SwitchToPreviousPicture(rc, ref rcIndex);
    }

    // EU Category Functions
    public void EUSwitchToNext()
    {
        SwitchToNextPicture(eu, ref euIndex);
    }

    public void EUSwitchToPrevious()
    {
        SwitchToPreviousPicture(eu, ref euIndex);
    }

    // YK Category Functions
    public void YKSwitchToNext()
    {
        SwitchToNextPicture(yk, ref ykIndex);
    }

    public void YKSwitchToPrevious()
    {
        SwitchToPreviousPicture(yk, ref ykIndex);
    }

    // Generic Function to Switch to the Next Picture
    private void SwitchToNextPicture(GameObject[] pictures, ref int currentIndex)
    {
        if (pictures == null || pictures.Length == 0) return;

        // Deactivate the current picture
        pictures[currentIndex].SetActive(false);

        // Increment the index and wrap around if necessary
        currentIndex = (currentIndex + 1) % pictures.Length;

        // Activate the new picture
        pictures[currentIndex].SetActive(true);
    }

    // Generic Function to Switch to the Previous Picture
    private void SwitchToPreviousPicture(GameObject[] pictures, ref int currentIndex)
    {
        if (pictures == null || pictures.Length == 0) return;

        // Deactivate the current picture
        pictures[currentIndex].SetActive(false);

        // Decrement the index and wrap around if necessary
        currentIndex = (currentIndex - 1 + pictures.Length) % pictures.Length;

        // Activate the new picture
        pictures[currentIndex].SetActive(true);
    }

    // Generic Function to Activate a Picture
    private void ActivatePicture(GameObject[] pictures, int index)
    {
        if (pictures == null || pictures.Length == 0) return;

        // Deactivate all pictures
        foreach (GameObject picture in pictures)
        {
            if (picture != null) picture.SetActive(false);
        }

        // Activate the specified picture
        if (pictures[index] != null) pictures[index].SetActive(true);
    }
}
