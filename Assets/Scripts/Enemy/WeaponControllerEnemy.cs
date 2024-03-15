using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class WeaponControllerEnemy : MonoBehaviour
{

    // check for enemies variables 
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerToHit;
    [SerializeField] private WeaponFire weaponFire;
    [SerializeField] private Transform parent;
    [SerializeField] private Transform player;
    [SerializeField] private float minimalDistends;

    private float distanceFromPlayer;
    private Collider[] hitCollider = new Collider[10];
    private int numberOfColliders;
    private int prevNumberOfColliders;
    private bool combatMode = false;

    // focus variables 
    private bool switchFocusTarget = true;
    private Collider focusObject;
    private int intFocusOnObject = 0;

    // shoot variables 
    [SerializeField] private float shootCoolDown;
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


    // See radius
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(parent.position, radius);
    }
    private void Update()
    {
        ReloadWeaponHolder();


        // activate when player is (value) away
        distanceFromPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceFromPlayer < minimalDistends)
        {
            combatMode = true;
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
        else
        {
            combatMode = false;
        }
        ReloadTimer();
        ShootTimer();
    }


    private void ReloadTimer()
    {
        if (!isOutOfCombat)
        {
            outOfCombatTimer -= Time.deltaTime;
        }
        if (outOfCombatTimer < 0)
        {
            isOutOfCombat = true;
        }
    }
    private void ReloadWeaponHolder()
    {
        if (canReload && isOutOfCombat)
        {
            weaponFire.Reload();
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
            if (abilityToFire && focusObject != null)
            {
                weaponFire.ActivateRocket(focusObject);
                Debug.Log("FIRE!!!!");
                canReload = true;
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

    // can be used if the enemy also has enemies  
    private void FocusObject()
    {

        if (hitCollider[0])
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