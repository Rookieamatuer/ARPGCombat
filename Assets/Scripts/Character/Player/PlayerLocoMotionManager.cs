using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocoMotionManager : CharacterLocoMotionManager
{
    PlayerManager player;
    public float horizontalMovement;
    public float verticalMovement;
    public float moveAmount;

    private Vector3 moveDirection;
    private Vector3 targetRotationDirection;
    [SerializeField] float runningSpeed = 2f;
    [SerializeField] float walkingSpeed = 5f;
    [SerializeField] float ratationSpeed = 15f;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerManager>();
    }

    public void HandleAllMovement()
    {
        HandleGroundedMovement();
        HandleRoatation();
    }

    public void GetHorizontalAndVerticalInput()
    {
        horizontalMovement = PlayerInputManager.instance.horizontalInput;
        verticalMovement = PlayerInputManager.instance.verticalInput;
    }

    public void HandleGroundedMovement()
    {
        GetHorizontalAndVerticalInput();
        // 移动方向取决于相机朝向和输入
        moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
        moveDirection += PlayerCamera.instance.transform.right * horizontalMovement;
        moveDirection.Normalize();
        moveDirection.y = 0;

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

    public void HandleRoatation()
    {
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
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, ratationSpeed * Time.deltaTime);
        transform.rotation = targetRotation;
    }
}
