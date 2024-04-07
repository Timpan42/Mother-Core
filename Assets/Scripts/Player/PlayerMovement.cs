using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float backwardsSpeed;
    [SerializeField] private float longitudeSpeed;

    public void Thruster(Vector3 moveDirection)
    {
        if (moveDirection.z > 0)
        {
            transform.position += transform.forward * forwardSpeed * Time.deltaTime * moveDirection.z;
        }
        else
        {
            transform.position += transform.forward * backwardsSpeed * Time.deltaTime * moveDirection.z;
        }

        if (moveDirection.x > 0 || moveDirection.x < 0)
        {
            transform.position += transform.right * longitudeSpeed * Time.deltaTime * moveDirection.x;
        }
    }
}
