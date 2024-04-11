using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class WeaponControllerEnemy : MonoBehaviour
{

    // check for enemies variables 
    [SerializeField] private LayerMask layerToHit;
    [SerializeField] private WeaponFire weaponFire;
    [SerializeField] private Transform parent;
    [SerializeField] private Transform player;
    [SerializeField] private float minimalDistends;

    private float weaponRadius;
    private float distanceFromPlayer;

    //colliders
    private Collider[] hitCollider = new Collider[1];
    private int numberOfColliders;
    private int prevNumberOfColliders;

    // focus variables 
    private bool switchFocusTarget = true;
    private Collider focusObject;
    private int intFocusOnObject = 0;

    // shoot variables 
    private float shootCoolDownTimer;
    private bool abilityToFire = false;

    //Reload variables 
    [SerializeField] private float outOfCombatCoolDown;
    private float outOfCombatTimer;
    private bool isOutOfCombat = false;
    private bool canReload = false;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SetWeaponRange(float range)
    {
        weaponRadius = range;
    }

    // See radius
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(parent.position, weaponRadius);
    }
    private void Update()
    {
        ReloadWeaponHolder();
        CalculateDistanceFromPlayer();

        if (!isOutOfCombat)
        {
            canReload = true;
            if (prevNumberOfColliders != numberOfColliders)
            {
                ClearArrayOnObjectExit();
                prevNumberOfColliders = numberOfColliders;
            }

            numberOfColliders = Physics.OverlapSphereNonAlloc(parent.position, weaponRadius, hitCollider, layerToHit);

            SortArrayByDistends();
            FocusObject();
            FireAtTarget();
        }
        ShootTimer();
    }


    private void ReloadWeaponHolder()
    {
        if (canReload && isOutOfCombat)
        {
            weaponFire.Reload();
            canReload = false;
        }
    }

    private void CalculateDistanceFromPlayer()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceFromPlayer < minimalDistends)
        {
            isOutOfCombat = false;
        }
        else
        {
            isOutOfCombat = true;
        }
    }

    private void ShootTimer()
    {
        if (!abilityToFire)
        {
            shootCoolDownTimer -= Time.deltaTime;
        }
        if (shootCoolDownTimer < 0)
        {
            abilityToFire = true;
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
        // if player is focus fire 
        if (focusObject != null)
        {
            if (abilityToFire && focusObject != null && !weaponFire.isReloading)
            {
                weaponFire.ActivateRocket(focusObject);
                FireCoolDown();
            }
        }
    }

    private void FireCoolDown()
    {
        if (abilityToFire)
        {
            abilityToFire = false;
            shootCoolDownTimer = weaponFire.getShotCoolDown;
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
        if (hitCollider[0] != null || focusObject == null)
        {
            switchFocusTarget = true;
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
}