using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls playerInputMap;
    private InputAction moveAction;
    private Rigidbody playerRigidbody;
    private Vector3 moveDirection;

    [SerializeField] private float forwardSpeed;
    [SerializeField] private float backwardsSpeed;

    private void Awake()
    {
        playerInputMap = new PlayerControls();
    }

    private void OnEnable()
    {
        moveAction = playerInputMap.PlayerMovement.Move;
        moveAction.Enable();
    }

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveDirection = moveAction.ReadValue<Vector3>();
        Thruster();

    }
    private void Thruster()
    {
        if (moveDirection.z > 0)
        {
            transform.position += transform.forward * forwardSpeed * Time.deltaTime * moveDirection.z;
        }
        else
        {
            transform.position += transform.forward * backwardsSpeed * Time.deltaTime * moveDirection.z;
        }
    }
}
