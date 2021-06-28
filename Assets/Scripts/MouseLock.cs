using System;
using UnityEngine;

public class MouseLock : MonoBehaviour
{
    [SerializeField] private bool cursorVisibility = true;
    [SerializeField] private CursorLockMode lockMode = CursorLockMode.Locked;


    private void Start()
    {
        Cursor.lockState = lockMode;
        Cursor.visible = cursorVisibility;
    }

    // Update is called once per frame
    public void SetVisibility(bool value)
    {
        cursorVisibility = value;
        Cursor.visible = cursorVisibility;
    }

    public void SetLockMode(CursorLockMode value)
    {
        lockMode = value;
        Cursor.lockState = lockMode;
    }
}


