using UnityEngine;

public class MouseLock : MonoBehaviour
{
    public bool paused = false;


    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
