using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private float _mouseX;
    private float _mouseY;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private CharacterController controller;

    private void Update()
    {
        //prob not gonna use the Y direction for player.
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        transform.eulerAngles += new Vector3(0, _mouseX * rotateSpeed, 0);
    }
}
