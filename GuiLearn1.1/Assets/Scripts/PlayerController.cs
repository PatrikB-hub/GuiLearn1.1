using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    //mouse input
    //rotate camera up and down
    //rotate the character left and right
    //keyboard input
    //move the character

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.8f;

    private Vector3 velocity;

    public float mouseSensitivity = 500f;
    private float xRotation = 0f;

    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MouseLook();
        Move();
    }

    private void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void Move()
    {
        // gives x value from -1 to 1
        float x = Input.GetAxis("Horizontal");
        // gives z value from -1 to 1
        float z = Input.GetAxis("Vertical");

        //we want to move in this direction
        Vector3 move = (transform.right * x) + (transform.forward * z);

        velocity.y += gravity * Time.deltaTime;

        controller.Move((velocity + move) * speed * Time.deltaTime);
    }
}
