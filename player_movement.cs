using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public float speed = 5.0f;
    public float sensitivity = 2.0f;
    public float maxYAngle = 80.0f; // Batas atas rotasi kamera
    public float minYAngle = -80.0f; // Batas bawah rotasi kamera

    public Transform playerBody;
    public Transform cameraTransform;
    public Transform crosshair;

    private CharacterController characterController;
    private float rotationX = 0;
    private bool cursorLocked = true;

    private void Start()
    {
        Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !cursorLocked;

        characterController = GetComponent<CharacterController>();

        // Periksa apakah cameraTransform sudah diinisialisasi
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogError("Camera not found! Make sure the main camera is tagged as 'MainCamera'.");
        }

        // Menambahkan crosshair
        if (crosshair != null)
        {
            crosshair.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle status kursor dan keadaan kursor
            cursorLocked = !cursorLocked;
            Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !cursorLocked;
        }

        if (cursorLocked)
        {
            // Kontrol gerakan karakter hanya jika kursor terkunci
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 moveDirection = new Vector3(horizontal, 0.0f, vertical).normalized;
            Vector3 moveVector = transform.TransformDirection(moveDirection) * speed;

            // Kontrol rotasi kamera (look around)
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = -Input.GetAxis("Mouse Y") * sensitivity; // Negative untuk invert look up/down

            // Rotasi kamera vertikal dengan batasan
            rotationX += mouseY;
            rotationX = Mathf.Clamp(rotationX, minYAngle, maxYAngle);

            cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            playerBody.Rotate(Vector3.up * mouseX);

            // Terapkan gerakan pada CharacterController
            characterController.Move(moveVector * Time.deltaTime);

            // Memperbarui posisi crosshair
            if (crosshair != null)
            {
                crosshair.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            }
        }
    }
}
