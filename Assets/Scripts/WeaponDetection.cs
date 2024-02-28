using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class WeaponDetection : MonoBehaviour
{
    private PlayerControls playerInputMap;
    private InputAction fireInput;
    private InputAction changeTargetInput;
    private InputAction combatModeInput;

    private bool abilityToFire = false;
    private int ChangeTargetInputValue;
    private bool combatMode = false;

    [SerializeField] float radius;
    [SerializeField] LayerMask layerToHit;
    [SerializeField] Transform player;
    Collider[] hitCollider = new Collider[10];
    int numberOfColliders;

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

    private void Update()
    {
        if (combatModeInput.WasPerformedThisFrame())
        {
            combatMode = !combatMode;
            Debug.Log("combatMode active");
        }
        if (combatMode)
        {
            if (fireInput.WasPerformedThisFrame())
            {
                abilityToFire = true;
            }
            if (abilityToFire)
            {
                numberOfColliders = Physics.OverlapSphereNonAlloc(player.position, radius, hitCollider, layerToHit);
                for (int i = 0; i < numberOfColliders; i++)
                {
                    Debug.Log("Collider: " + hitCollider[i].name);
                }
                abilityToFire = false;
            }
        }

    }
}