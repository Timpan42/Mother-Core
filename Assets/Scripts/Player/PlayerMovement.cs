using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float forwardSpeed;
    private float backwardsSpeed;
    private float longitudeSpeed;

    public void UpdateSpeed(float speed)
    {
        forwardSpeed = speed;
        backwardsSpeed = speed / 2;
        longitudeSpeed = speed / 2;
    }

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
