using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Hide and lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public float moveSpeed = 50f;
    public float verticalSpeed = 20f;

    Vector2 rotation = Vector2.zero;
    public float speed = 3;

    void Update()
    {
        // Camera movement
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalMovement, 0f, verticalMovement).normalized;
        Vector3 moveAmount = moveDirection * moveSpeed * Time.deltaTime;

        // Flying up and down
        float verticalInput = 0f;
        if (Input.GetKey(KeyCode.Space))
        {
            verticalInput = 1f; // Fly up
        }
        else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            verticalInput = -1f; // Fly down
        }

        Vector3 verticalMoveAmount = Vector3.up * verticalInput * verticalSpeed * Time.deltaTime;

        // Combine horizontal/vertical movement and apply to the camera
        transform.Translate(moveAmount + verticalMoveAmount);

        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        transform.eulerAngles = (Vector2)rotation * speed;
    }
}
