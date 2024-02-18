using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private  PlayerControls playerInputMap;
    private InputAction moveAction;
    private Rigidbody playerRigidbody;
    private Vector3 moveDirection;

    [SerializeField] private float forwardSpeed;
    [SerializeField] private float backwardsSpeed;

    private void Awake() {
        playerInputMap = new PlayerControls();
    }

    private void OnEnable() {
        moveAction = playerInputMap.PlayerMovement.Move;
        moveAction.Enable();
    }

    private void Start() {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
       moveDirection = moveAction.ReadValue<Vector3>();
        if(moveDirection.z > 0){
            useForwardThruster();
        }
        else if(moveDirection.z < 0){
            useBackwardsThruster();
        }
        
        if(moveDirection.x != 0){
            useSideThrusters();
        }
        
    }

    private void useForwardThruster(){
        playerRigidbody.AddForce(transform.forward * moveDirection.z * forwardSpeed, ForceMode.Acceleration);
    }
    
    private void useBackwardsThruster(){
        playerRigidbody.AddForce(transform.forward * moveDirection.z * backwardsSpeed, ForceMode.Acceleration);
    }

    private void useSideThrusters(){
        playerRigidbody.AddForce(transform.right * moveDirection.x, ForceMode.Acceleration);
    }
}
