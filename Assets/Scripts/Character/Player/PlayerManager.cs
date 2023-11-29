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
}
