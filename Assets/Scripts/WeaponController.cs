using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Video;


public class WeaponController : MonoBehaviour
{
    private PlayerControls playerInputMap;
    private InputAction fireInput;
    private InputAction changeTargetInput;
    private InputAction combatModeInput;

    private bool abilityToFire = false;
    private float changeTargetInputValue;
    private bool combatMode = false;

    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerToHit;
    [SerializeField] private Transform player;
    private Collider[] hitCollider = new Collider[10];
    private int numberOfColliders;
    private int prevNumberOfColliders;

    private bool switchFocusTarget = true;
    private Collider getFocusedObjectCollider = null;
    private int focusOnObject = 0;

    private void Awake()
    {
        playerInputMap = new PlayerControls();
    }

    private void OnEnable()
    {
        fireInput = playerInputMap.PlayerMovement.Fire;
        changeTargetInput = playerInputMap.PlayerMovement.ChangeTarget;
        combatModeInput = playerInputMap.PlayerMovement.CombatMode;
        fireInput.Enable();
        changeTargetInput.Enable();
        combatModeInput.Enable();
    }

    // See radius
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(player.position, radius);
    }
    private void Update()
    {

        ActivateCombatMode();

        if (combatMode)
        {
            if (prevNumberOfColliders != numberOfColliders)
            {
                ClearArrayOnObjectExit();
                prevNumberOfColliders = numberOfColliders;
            }

            numberOfColliders = Physics.OverlapSphereNonAlloc(player.position, radius, hitCollider, layerToHit);

            SortArrayByDistends();
            FocusObject();

            FireAtTarget();
            FireCoolDown();
        }
    }

    private void ClearArrayOnObjectExit()
    {
        for (int i = 0; i < prevNumberOfColliders; i++)
        {
            hitCollider[i] = null;
            Debug.Log("Clear");
        }
    }

    private void ActivateCombatMode()
    {
        if (combatModeInput.WasPerformedThisFrame())
        {
            combatMode = !combatMode;
            Debug.Log("combatMode active");
        }
    }

    private void FireAtTarget()
    {
        if (fireInput.WasPerformedThisFrame())
        {
            abilityToFire = true;
            
            // ask weapon script to attack 
            
            Debug.Log("FIRE!!!!");
        }
    }

    private void FireCoolDown()
    {
        if (abilityToFire)
        {
            // ask weapon script for cool down
        }
    }

    private void SortArrayByDistends()
    {
        if (numberOfColliders < 1)
        {
            return;
        }
        else
        {
            for (int i = 0; i < numberOfColliders; i++)
            {
                int closestPositionIndex = i;
                for (int j = i + 1; j < numberOfColliders; j++)
                {
                    if (CheckSmallerDistance(closestPositionIndex, j))
                    {
                        closestPositionIndex = j;
                    }
                    if (closestPositionIndex != i)
                    {
                        var temp = hitCollider[i];
                        hitCollider[i] = hitCollider[closestPositionIndex];
                        hitCollider[closestPositionIndex] = temp;
                    }

                }

            }

        }

    }

    private bool CheckSmallerDistance(int smallestIndex, int nextIndex)
    {
        float small = Vector3.Distance(player.position, hitCollider[smallestIndex].transform.position);
        float next = Vector3.Distance(player.position, hitCollider[nextIndex].transform.position);

        if (small < next)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void FocusObject()
    {

        if (changeTargetInput.WasPressedThisFrame())
        {
            changeTargetInputValue = changeTargetInput.ReadValue<float>();
            switchFocusTarget = true;
            changeObject();
        }

        if (switchFocusTarget)
        {
            getFocusedObjectCollider = hitCollider[focusOnObject];
            switchFocusTarget = false;
        }

        if (hitCollider[focusOnObject] != null)
        {
            if (!switchFocusTarget)
            {
                FindOriginalFocusTarget();
            }

            Debug.DrawLine(player.position, hitCollider[focusOnObject].transform.position, Color.red);
        }
        else
        {
            switchFocusTarget = true;
            focusOnObject = 0;
        }
    }

    private void changeObject()
    {

        // om spelaren focusera redan på ett object hitta dens index i array
        // bytt utt focusOnObject numer till dens index 

        // om spelaren trycker Q eller E ändra focus start från 0  

        if (changeTargetInputValue > 0)
        {
            focusOnObject++;

            if (focusOnObject > numberOfColliders - 1)
            {
                focusOnObject = 0;
            }

            changeTargetInputValue = 0;
        }
        else if (changeTargetInputValue < 0)
        {
            focusOnObject--;
            if (focusOnObject < 0)
            {
                focusOnObject = numberOfColliders - 1;
            }

            changeTargetInputValue = 0;
        }
    }

    private void FindOriginalFocusTarget()
    {
        for (int i = 0; i < numberOfColliders; i++)
        {
            if (getFocusedObjectCollider == hitCollider[i])
            {
                focusOnObject = i;
                break;
            }
        }
    }
}