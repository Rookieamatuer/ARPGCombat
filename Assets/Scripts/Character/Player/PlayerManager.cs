using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
    [HideInInspector] public PlayerLocoMotionManager playerLocoMotionManager;
    [HideInInspector] public PlayerNetworkManager playerNetworkManager;
    [HideInInspector] public PlayerStatManager playerStatManager;

    protected override void Awake()
    {
        base.Awake();

        playerLocoMotionManager = GetComponent<PlayerLocoMotionManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerNetworkManager = GetComponent<PlayerNetworkManager>();
        playerStatManager = GetComponent<PlayerStatManager>();
    }

    protected override void Update()
    {
        base.Update();

        // 只控制自己的角色
        if (!IsOwner) return;

        playerLocoMotionManager.HandleAllMovement();

        // 回复耐力
        playerStatManager.RegenerateStamina();
    }

    protected override void LateUpdate()
    {
        if(!IsOwner) return;

        base.LateUpdate();

        PlayerCamera.instance.HandleAllCameraActions();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if(IsOwner)
        {
            PlayerCamera.instance.player = this;
            PlayerInputManager.instance.player = this;
            playerNetworkManager.cunrrentStamina.OnValueChanged += PlayerUIManager.instance.playerUIHUDManager.SetNewStaminaValue;
            playerNetworkManager.cunrrentStamina.OnValueChanged += playerStatManager.ResetStaminaRegenTimer;

            // 完成save game后移除
            playerNetworkManager.maxStamina.Value = playerStatManager.CalculateStaminaBasedOnEnduranceLevel(playerNetworkManager.endurance.Value);
            playerNetworkManager.cunrrentStamina.Value = playerStatManager.CalculateStaminaBasedOnEnduranceLevel(playerNetworkManager.endurance.Value);
            PlayerUIManager.instance.playerUIHUDManager.SetMaxStaminaValue(playerNetworkManager.maxStamina.Value);
        }
    }
}
