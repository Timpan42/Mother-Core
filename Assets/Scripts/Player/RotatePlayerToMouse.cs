using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotatePlayerToMouse : MonoBehaviour
{
    private PlayerControls playerInputMap;
    private InputAction cameraInput;
    private Vector2 inputValue;
    [SerializeField] private bool mouseControlsActive = false;
    [SerializeField] private float xTurnSpeed;
    [SerializeField] private float yTurnSpeed;


    private void Awake()
    {
        playerInputMap = new PlayerControls();
    }

    private void OnEnable()
    {
        if (mouseControlsActive)
        {
            cameraInput = playerInputMap.PlayerMovement.CameraMoveMouse;
        }
        else
        {
            cameraInput = playerInputMap.PlayerMovement.CameraMoveArrow;
        }
        cameraInput.Enable();
    }
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        inputValue = cameraInput.ReadValue<Vector2>();
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        if (mouseControlsActive)
        {
            MouseControls();
        }
        else
        {
            ArrowControls();
        }
    }

    private void MouseControls()
    {
        float xRotation = 0;
        float yRotation = 0;
        if (inputValue.x > Screen.width * 0.80)
        {
            yRotation += yTurnSpeed * Time.deltaTime;
        }
        if (inputValue.x < Screen.width * 0.20)
        {
            yRotation -= yTurnSpeed * Time.deltaTime;
        }
        if (inputValue.y > Screen.height * 0.80)
        {
            xRotation += xTurnSpeed * Time.deltaTime;
        }
        if (inputValue.y < Screen.width * 0.20)
        {
            xRotation -= xTurnSpeed * Time.deltaTime;
        }

        transform.Rotate(new Vector3(xRotation, yRotation, 0));
    }
    private void ArrowControls()
    {
        float xRotation = 0;
        float yRotation = 0;
        if (inputValue.x != 0)
        {
            yRotation += yTurnSpeed * inputValue.x * Time.deltaTime;
        }
        if (inputValue.y != 0)
        {
            xRotation += xTurnSpeed * inputValue.y * Time.deltaTime;
        }
        transform.Rotate(new Vector3(xRotation, yRotation, 0));
    }
}
