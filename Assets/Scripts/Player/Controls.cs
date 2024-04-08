using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    private PlayerControls playerInputMap;
    private PlayerControls playerInputUI;

    //Inputs
    private InputAction cameraInput;
    private InputAction moveAction;
    private InputAction inputCorePower;
    private InputAction fireInput;
    private InputAction changeTargetInput;
    private InputAction combatModeInput;
    private InputAction reloadInput;

    //UI inputs 
    private InputAction upgradeInput;
    private InputAction escInput;


    //Scripts
    private InsideCore insideCore;
    private WeaponController weaponController;
    private PlayerMovement playerMovement;
    private RotatePlayer rotatePlayer;
    [SerializeField] private UIToggleManager uiToggleManager;

    //Variables 
    private Vector3 moveDirection;
    private Vector2 inputValue;


    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        insideCore = transform.GetComponentInChildren<InsideCore>();
        weaponController = transform.GetComponentInChildren<WeaponController>();
        playerMovement = transform.GetComponent<PlayerMovement>();
        rotatePlayer = transform.GetComponent<RotatePlayer>();
    }
    private void Awake()
    {
        playerInputMap = new PlayerControls();
        playerInputUI = new PlayerControls();

    }

    private void OnEnable()
    {
        cameraInput = playerInputMap.PlayerMovement.CameraMoveArrow;
        moveAction = playerInputMap.PlayerMovement.Move;
        inputCorePower = playerInputMap.PlayerMovement.CorePower;
        fireInput = playerInputMap.PlayerMovement.Fire;
        changeTargetInput = playerInputMap.PlayerMovement.ChangeTarget;
        combatModeInput = playerInputMap.PlayerMovement.CombatMode;
        reloadInput = playerInputMap.PlayerMovement.Reload;
        inputCorePower = playerInputMap.PlayerMovement.CorePower;

        upgradeInput = playerInputUI.PlayerUI.UpgradeWindow;
        escInput = playerInputUI.PlayerUI.EscWindow;

        cameraInput.Enable();
        moveAction.Enable();
        inputCorePower.Enable();
        fireInput.Enable();
        changeTargetInput.Enable();
        combatModeInput.Enable();
        reloadInput.Enable();
        inputCorePower.Enable();

        upgradeInput.Enable();
        escInput.Enable();
    }

    private void Update()
    {
        MovementInputs();
        RotateMovementInputs();
        CorePowerInputs();
        WeaponControllerInputs();
        UpgradeMenu();
    }

    private void RotateMovementInputs()
    {
        inputValue = cameraInput.ReadValue<Vector2>();
        rotatePlayer.Rotate(inputValue);
    }

    private void MovementInputs()
    {
        moveDirection = moveAction.ReadValue<Vector3>();
        playerMovement.Thruster(moveDirection);
    }

    private void CorePowerInputs()
    {
        if (inputCorePower.WasPerformedThisFrame())
        {
            insideCore.ActivateTheCore();
        }

    }

    private void WeaponControllerInputs()
    {
        if (reloadInput.WasPressedThisFrame())
        {
            weaponController.ReloadWeaponHolder();
        }
        if (combatModeInput.WasPerformedThisFrame())
        {
            weaponController.ActivateCombatMode();
        }
        if (fireInput.WasPerformedThisFrame())
        {
            weaponController.FireAtTarget();
        }
        if (changeTargetInput.WasPressedThisFrame())
        {
            weaponController.ChangeObject(changeTargetInput);
        }
    }

    private void UpgradeMenu()
    {
        if (upgradeInput.WasPerformedThisFrame())
        {
            uiToggleManager.ToggleUpgradeMenu();
        }
    }

    private void EscMenu()
    {
        if (escInput.WasPerformedThisFrame())
        {
            uiToggleManager.ToggleEscMenu();
        }
    }
}
