using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotatePlayerToMouse : MonoBehaviour
{
    private  PlayerControls playerInputMap;
    private InputAction mousePosition;
    Vector2 mouseLocation;
    [SerializeField] float xTurnSpeed;
    [SerializeField] float yTurnSpeed;


    private void Awake() {
        playerInputMap = new PlayerControls();
    }

    private void OnEnable() {
        mousePosition = playerInputMap.PlayerMovement.Mouse;
        mousePosition.Enable();
    }
    private void Start() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update() {
        mouseLocation = mousePosition.ReadValue<Vector2>();
        RotatePlayer();
    }
    
    private void RotatePlayer(){
        float xRotation = 0;
        float yRotation = 0;
        if(mouseLocation.x > Screen.width * 0.80){
            yRotation =+  yTurnSpeed * Time.deltaTime;
        }
        if (mouseLocation.x < Screen.width * 0.20){
            yRotation =-  yTurnSpeed * Time.deltaTime;
        }
        if(mouseLocation.y > Screen.height * 0.80){
            xRotation =+  xTurnSpeed * Time.deltaTime;
        }
        if (mouseLocation.y < Screen.width * 0.20){
            xRotation =-  xTurnSpeed * Time.deltaTime;
        }
                
        transform.Rotate(new Vector3(xRotation, yRotation,0));
    }
}
