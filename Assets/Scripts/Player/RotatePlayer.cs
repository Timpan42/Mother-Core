using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotatePlayer : MonoBehaviour
{
    private float xTurnSpeed;
    private float yTurnSpeed;
    private Vector2 inputValue;

    public void UpdateRotation(float speed)
    {
        xTurnSpeed = speed;
        yTurnSpeed = speed;
    }

    public void Rotate(Vector2 input)
    {
        inputValue = input;
        ArrowControls();
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
}
