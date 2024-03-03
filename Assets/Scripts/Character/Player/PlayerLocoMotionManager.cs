using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocoMotionManager : CharacterLocoMotionManager
{
    PlayerManager player;

    [HideInInspector] public float horizontalMovement;
    [HideInInspector] public float verticalMovement;
    [HideInInspector] public float moveAmount;

    [Header("Movement Settings")]
    private Vector3 moveDirection;
    private Vector3 targetRotationDirection;
    [SerializeField] float walkingSpeed = 1.5f;
    [SerializeField] float runningSpeed = 4.5f;
    [SerializeField] float sprintingSpeed = 9f;
    [SerializeField] float rotationSpeed = 15f;
    [SerializeField] int sprintingStaminaCost = 2;

    [Header("Dodge")]
    private Vector3 rollDirection;
    [SerializeField] float dodgeStaminaCost = 25;

    [Header("Jump")]
    [SerializeField] float jumpHeight = 4;
    [SerializeField] float jumpStaminaCost = 25;
    [SerializeField] float jumpForwardSpeed = 5;
    [SerializeField] float freeFallSpeed = 2;
    private Vector3 jumpDirection;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerManager>();
    }

    protected override void Update()
    {
        base.Update();
        if (player.IsOwner)
        {
            player.characterNetworkManager.animatorVerticalParameter.Value = verticalMovement;
            player.characterNetworkManager.animatorHorizontalParameter.Value = horizontalMovement;
            player.characterNetworkManager.networkMoveAmountParameter.Value = moveAmount;
        } else
        {
            verticalMovement = player.characterNetworkManager.animatorVerticalParameter.Value;
            horizontalMovement = player.characterNetworkManager.animatorHorizontalParameter.Value;
            moveAmount = player.characterNetworkManager.networkMoveAmountParameter.Value;

            player.playerAnimatorManager.UpdateAnimatorMovementParameter(0, moveAmount, player.playerNetworkManager.isSprinting.Value);
        }
    }

    public void HandleAllMovement()
    {
        HandleGroundedMovement();
        HandleRoatation();
        HandleJumpingMovement();
        HandleFreeFallMovement();
    }

    public void GetMovementValues()
    {
        horizontalMovement = PlayerInputManager.instance.horizontalInput;
        verticalMovement = PlayerInputManager.instance.verticalInput;
        moveAmount = PlayerInputManager.instance.moveAmount;
    }

    public void HandleGroundedMovement()
    {
        if (!player.canMove) return;
        GetMovementValues();
        // 移动方向取决于相机朝向和输入
        moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
        moveDirection += PlayerCamera.instance.transform.right * horizontalMovement;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (player.playerNetworkManager.isSprinting.Value)
        {
            player.characterController.Move(moveDirection * sprintingSpeed * Time.deltaTime);
        } 
        else
        {
            if (PlayerInputManager.instance.moveAmount > 0.5f)
            {
                // run
                player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
            }
            else if (PlayerInputManager.instance.moveAmount <= 0.5f)
            {
                // walk
                player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
            }
        }

        
    }

    public void HandleJumpingMovement()
    {
        if (player.isJumping)
        {
            player.characterController.Move(jumpDirection * jumpForwardSpeed * Time.deltaTime);
        }
    }

    public void HandleFreeFallMovement()
    {
        if (!player.isGrounded)
        {
            Vector3 freeFallDirection;

            freeFallDirection = PlayerCamera.instance.transform.forward * PlayerInputManager.instance.verticalInput;
            freeFallDirection += PlayerCamera.instance.transform.forward * PlayerInputManager.instance.horizontalInput;
            freeFallDirection.y = 0;

            player.characterController.Move(freeFallDirection * freeFallSpeed * Time.deltaTime);
        }
    }

    public void HandleRoatation()
    {
        if (!player.canRotate) return;
        targetRotationDirection = Vector3.zero;
        targetRotationDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
        targetRotationDirection += PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
        targetRotationDirection.Normalize();
        targetRotationDirection.y = 0;

        if (targetRotationDirection ==  Vector3.zero)
        {
            targetRotationDirection = transform.forward;
        }

        Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = targetRotation;
    }

    public void HandleSprinting()
    {
        if (player.isPerformingAction)
        {
            // 停止冲刺
            player.playerNetworkManager.isSprinting.Value = false;
        }
        // 耐力耗尽停止冲刺
        if (player.playerNetworkManager.cunrrentStamina.Value <= 0)
        {
            player.playerNetworkManager.isSprinting.Value = false;
            return;
        }
        // 移动中则开始冲刺
        if (moveAmount >= 0.5)
        {
            player.playerNetworkManager.isSprinting.Value = true;
        }else
        {
            // 处于静止则不冲刺
            player.playerNetworkManager.isSprinting.Value = false;
        }

        if (player.playerNetworkManager.isSprinting.Value)
        {
            player.playerNetworkManager.cunrrentStamina.Value -= sprintingStaminaCost * Time.deltaTime;
        }
    }

    public void AttemptToPerformDodge()
    {
        if (player.isPerformingAction) return;

        if (player.playerNetworkManager.cunrrentStamina.Value <= 0) return;

        if (PlayerInputManager.instance.moveAmount > 0)
        {
            rollDirection = PlayerCamera.instance.cameraObject.transform.forward * PlayerInputManager.instance.verticalInput;
            rollDirection += PlayerCamera.instance.cameraObject.transform.right * PlayerInputManager.instance.horizontalInput;
            rollDirection.y = 0;
            rollDirection.Normalize();

            Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
            player.transform.rotation = playerRotation;

            // 翻滚
            player.playerAnimatorManager.PlayerTargetActionAnimation("RollForward", true);
        } else
        {
            // 后撤
            // player.playerAnimatorManager.PlayerTargetActionAnimation("", true);

        }

        player.playerNetworkManager.cunrrentStamina.Value -= dodgeStaminaCost;
    }

    public void AttempToPerformJump()
    {
        if (player.isPerformingAction) return;

        if (player.playerNetworkManager.cunrrentStamina.Value <= 0) return;

        if (player.isJumping) return;

        if (!player.isGrounded) return;

        player.playerAnimatorManager.PlayerTargetActionAnimation("Jump_Up", false);

        // ApplyJumpingVelocity();

        player.isJumping = true;

        player.playerNetworkManager.cunrrentStamina.Value -= jumpStaminaCost;

        jumpDirection = PlayerCamera.instance.cameraObject.transform.forward * PlayerInputManager.instance.verticalInput;
        jumpDirection += PlayerCamera.instance.cameraObject.transform.forward * PlayerInputManager.instance.horizontalInput;

        if (jumpDirection != Vector3.zero)
        {
            if (player.playerNetworkManager.isSprinting.Value)
            {
                jumpDirection *= 1;
            }
            else if (PlayerInputManager.instance.moveAmount > 0.5)
            {
                jumpDirection *= 0.5f;
            }
            else if (PlayerInputManager.instance.moveAmount <= 0.5)
            {
                jumpDirection *= 0.25f;
            }
        } 
    }

    public void ApplyJumpingVelocity()
    {
        yVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityForce);
    }

}
