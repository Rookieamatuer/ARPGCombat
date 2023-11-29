using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    PlayerLocoMotionManager PlayerLocoMotionManager;

    protected override void Awake()
    {
        base.Awake();

        PlayerLocoMotionManager = GetComponent<PlayerLocoMotionManager>();
    }

    protected override void Update()
    {
        base.Update();

        // 只控制自己的角色
        if (!IsOwner) return;

        PlayerLocoMotionManager.HandleAllMovement();
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
        }
    }
}
