using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform cam;

    private void Start()
    {
        Camera playerCam = Camera.main;
        if (playerCam != null) cam = playerCam.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
