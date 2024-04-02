using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UITargetManager : MonoBehaviour
{
    [SerializeField] private WeaponController playerWeaponController;
    [SerializeField] private GameObject player;
    [SerializeField] private Image marker;
    [Range(0f, 1f)]
    [SerializeField] private float dotAllowedAmount;
    private Transform target;
    private bool isAlive = true;
    private Vector3 directionToTarget;
    private float minX, minY, maxX, maxY;

    private void Start()
    {
        StartCoroutine(TrackTarget());
    }

    private IEnumerator TrackTarget()
    {

        Vector2 targetPosition;

        while (isAlive)
        {
            if (playerWeaponController.getCombatMode)
            {
                if (playerWeaponController.GetFocusTarget() != null)
                {
                    target = playerWeaponController.GetFocusTarget();
                    targetPosition = Camera.main.WorldToScreenPoint(target.position);
                    SetOutMarker(targetPosition);
                }
                else
                {
                    marker.enabled = false;
                }
            }
            else if (marker.enabled)
            {
                marker.enabled = false;
            }

            if (!player.activeSelf || player == null)
            {
                isAlive = false;
            }
            yield return isAlive;
        }
    }


    private void SetOutMarker(Vector2 targetPosition)
    {
        directionToTarget = Vector3.Normalize(target.position - player.transform.position);
        marker.enabled = true;
        minX = marker.GetPixelAdjustedRect().width / 2;
        minY = marker.GetPixelAdjustedRect().height / 2;
        maxX = Screen.width - minX;
        maxY = Screen.height - minY;

        if (Vector3.Dot(player.transform.forward, directionToTarget) < -dotAllowedAmount)
        {
            marker.enabled = false;
        }
        else
        {
            marker.enabled = true;
        }

        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        marker.transform.position = targetPosition;
    }

}
