using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    /**
     * TODO :
     * 1. Get input value
     * 2. Move character based on input value
     */

    public static PlayerInputManager instance;
    public PlayerManager player;
    private PlayerControls playerControls;

    [Header("CameraMovementInput")]
    [SerializeField] Vector2 cameraInput;
    public float cameraHorizontalInput;
    public float cameraVerticalInput;

    [Header("PlayerMovementInput")]
    [SerializeField] Vector2 movementInput;
    public float horizontalInput;
    public float verticalInput;

    [Header("PlayerActionInput")]
    [SerializeField] bool dodgeInput = false;
    [SerializeField] bool sprintInput = false;

    public float moveAmount;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        // 切换场景时使用
        SceneManager.activeSceneChanged += OnSceneChange;

        instance.enabled = false;
    }

    private void OnSceneChange(Scene oldScene, Scene newScene)
    {
        // 加载游戏世界则启动玩家控制，否则不启动
        if(newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
        {
            instance.enabled = true;
        }
        else
        {
            instance.enabled = false;
        }
    }

    public void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();
            // 获取控制器输入
            playerControls.PlayerMovements.Movements.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerCamera.CameraControls.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;
            playerControls.PlayerActions.Sprint.performed += i => sprintInput = true;
            playerControls.PlayerActions.Sprint.canceled += i => sprintInput = false;
        }

        playerControls.Enable();
        
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

    private void OnApplicationFocus(bool focus)
    {
        // 游戏最小化后无法控制移动（避免同时移动两个窗口的角色）
        if(enabled)
        {
            playerControls.Enable();
        }
        else
        {
            playerControls.Disable();
        }
    }

    private void Update()
    {
        HandleAllInputs();
    }

    private void HandleAllInputs()
    {
        HandleMovementInput();
        HandleCameraMovementInput();
        HandleDodgeInput();
        HandleSprintInput();
    }

    // 移动
    
    private void HandleMovementInput()
    {
        horizontalInput = movementInput.x; 
        verticalInput = movementInput.y;
        // Clamp01(float value) : 将value限定在0-1之间，大于1返回1小于0返回0;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        if (moveAmount > 0 &&  moveAmount <= 0.5)
        {
            moveAmount = 0.5f;
        }
        else if (moveAmount > 0.5 && moveAmount <= 1.0)
        {
            moveAmount = 1.0f;
        }

        if (player == null) return;

        // 非锁定状态
        player.playerAnimatorManager.UpdateAnimatorMovementParameter(0, moveAmount, player.playerNetworkManager.isSprinting.Value);
    }

    private void HandleCameraMovementInput()
    {
        cameraHorizontalInput = cameraInput.x;
        cameraVerticalInput = cameraInput.y;
    }

    // 动作

    private void HandleDodgeInput()
    {
        if (dodgeInput)
        {
            dodgeInput = false;
            // 显示菜单的时候直接返回

            // 执行翻滚
            player.playerLocoMotionManager.AttemptToPerformDodge();
        }
    }

    private void HandleSprintInput()
    {
        if (sprintInput)
        {

            // 执行冲刺
            player.playerLocoMotionManager.HandleSprinting();
        } else
        {
            player.playerNetworkManager.isSprinting.Value = false;
        }
    }
}
