using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, InputActions.IPlayerActions
{
    public Vector2 MovementValue {get; private set;}
    public Vector2 MovementInput { get; private set; }
    public bool IsDownBreaking { get; private set; }
    InputActions inputActions;
    UIHandler UIHandler;
    PlayerEquipment playerEquipment;
    ResourcesManager resourcesManager;

    void Start()
    {
        UIHandler = FindObjectOfType<UIHandler>();
        inputActions = new InputActions();
        inputActions.Player.SetCallbacks(this);
        inputActions.Player.Enable();
        playerEquipment = GetComponent<PlayerEquipment>();
        resourcesManager = FindObjectOfType<ResourcesManager>();    
    }

    void Update()
    {

        //CHEATS!
#if UNITY_EDITOR


        if (Input.GetKeyDown(KeyCode.J))
        {
            resourcesManager.MoveMoney(10000);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            new Teleporter().UseConsumable(this, new Teleporter());
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            new Refueler().UseConsumable(this, new Refueler());
        }
#endif

    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        if (HealthManager.IsDead) return;
        if (Drill.IsDigging)
        {
            
            MovementInput = Vector2.zero;
            IsDownBreaking = false;            
            MovementValue = Vector2.zero;

            return;
        }
        
        Vector2 tempMovementValue = context.ReadValue<Vector2>();
        MovementInput = context.ReadValue<Vector2>();
        IsDownBreaking = tempMovementValue.y < 0;
        tempMovementValue.y = MathF.Max(tempMovementValue.y, 0);
        MovementValue = tempMovementValue;

    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Inventory.PrintInventory();
        UIHandler.ToggleInventoryUI();
    }

    public void OnConsumables(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if(playerEquipment.Consumables == null) return;

        foreach (ConsumableSO consumable in playerEquipment.Consumables)
        {
            if (consumable.Key == context.control.name)
            {
                consumable.UseConsumable(this, consumable);
                break;
            }
        }
    }
}
