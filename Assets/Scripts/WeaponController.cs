using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class WeaponController : MonoBehaviour
{
    // input variables 
    private PlayerControls playerInputMap;
    private InputAction fireInput;
    private InputAction changeTargetInput;
    private InputAction combatModeInput;
    private InputAction reloadInput;
    private bool abilityToFire = false;
    private float changeTargetInputValue;
    private bool combatMode = false;
    public bool getCombatMode { get => combatMode; }

    // check for enemies variables 
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerToHit;
    [SerializeField] private Transform parent;
    [SerializeField] private WeaponFire weaponFire;
    private Collider[] hitCollider = new Collider[10];
    private int numberOfColliders;
    private int prevNumberOfColliders;

    // focus variables 
    private bool switchFocusTarget = true;
    private Collider focusObject;
    private int intFocusOnObject = 0;

    // shoot variables 
    [SerializeField] private float shootCoolDown;
    private float shootCoolDownTimer;

    private void Awake()
    {
        playerInputMap = new PlayerControls();
    }

    private void OnEnable()
    {
        fireInput = playerInputMap.PlayerMovement.Fire;
        changeTargetInput = playerInputMap.PlayerMovement.ChangeTarget;
        combatModeInput = playerInputMap.PlayerMovement.CombatMode;
        reloadInput = playerInputMap.PlayerMovement.Reload;
        fireInput.Enable();
        changeTargetInput.Enable();
        combatModeInput.Enable();
        reloadInput.Enable();
    }

    // See radius
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(parent.position, radius);
    }
    private void Update()
    {
        ReloadWeaponHolder();

        ActivateCombatMode();

        if (combatMode)
        {
            if (prevNumberOfColliders != numberOfColliders)
            {
                ClearArrayOnObjectExit();
                prevNumberOfColliders = numberOfColliders;
            }

            numberOfColliders = Physics.OverlapSphereNonAlloc(parent.position, radius, hitCollider, layerToHit);

            SortArrayByDistends();
            FocusObject();

            FireAtTarget();
        }
        if (!abilityToFire)
        {
            shootCoolDownTimer -= Time.deltaTime;
        }
        if (shootCoolDownTimer < 0)
        {
            abilityToFire = true;
        }

    }

    private void ReloadWeaponHolder()
    {
        if (reloadInput.WasPressedThisFrame())
        {
            weaponFire.Reload();
        }
    }

    private void ActivateCombatMode()
    {
        if (combatModeInput.WasPerformedThisFrame())
        {
            combatMode = !combatMode;
            if (combatMode)
            {
                Debug.Log("combatMode active");
            }
            else
            {
                Debug.Log("combatMode deactivated");
            }
        }
    }

    private void ClearArrayOnObjectExit()
    {
        for (int i = 0; i < prevNumberOfColliders; i++)
        {
            hitCollider[i] = null;
        }
    }

    private void FireAtTarget()
    {
        if (fireInput.WasPerformedThisFrame())
        {
            if (abilityToFire && focusObject != null && !weaponFire.isReloading)
            {
                weaponFire.ActivateRocket(focusObject);
                FireCoolDown();
            }
            else
            {
                Debug.Log("Cant fire at target");
            }
        }
    }

    private void FireCoolDown()
    {
        if (abilityToFire)
        {
            abilityToFire = false;
            shootCoolDownTimer = shootCoolDown;
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
        float small = Vector3.Distance(parent.position, hitCollider[smallestIndex].transform.position);
        float next = Vector3.Distance(parent.position, hitCollider[nextIndex].transform.position);

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
            focusObject = hitCollider[intFocusOnObject];
            switchFocusTarget = false;
        }

        if (hitCollider[intFocusOnObject] != null)
        {
            if (!switchFocusTarget)
            {
                FindOriginalFocusTarget();
            }

            Debug.DrawLine(parent.position, hitCollider[intFocusOnObject].transform.position, Color.red);
        }
        else
        {
            switchFocusTarget = true;
            intFocusOnObject = 0;
        }
    }

    private void changeObject()
    {
        if (changeTargetInputValue > 0)
        {
            intFocusOnObject++;

            if (intFocusOnObject > numberOfColliders - 1)
            {
                intFocusOnObject = 0;
            }

            changeTargetInputValue = 0;
        }
        else if (changeTargetInputValue < 0)
        {
            intFocusOnObject--;
            if (intFocusOnObject < 0)
            {
                intFocusOnObject = numberOfColliders - 1;
            }

            changeTargetInputValue = 0;
        }
    }

    private void FindOriginalFocusTarget()
    {
        for (int i = 0; i < numberOfColliders; i++)
        {
            if (focusObject == hitCollider[i])
            {
                intFocusOnObject = i;
                break;
            }
        }
    }

    public Transform GetFocusTarget()
    {
        if (hitCollider[intFocusOnObject] != null)
        {
            return hitCollider[intFocusOnObject].transform;
        }
        else
        {
            return null;
        }
    }

}