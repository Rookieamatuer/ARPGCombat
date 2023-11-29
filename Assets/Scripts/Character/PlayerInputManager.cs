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
    private PlayerControls playerControls;

    [SerializeField] Vector2 movementInput;
    public float horizontalInput;
    public float verticalInput;
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

        // �л�����ʱʹ��
        SceneManager.activeSceneChanged += OnSceneChange;

        instance.enabled = false;
    }

    private void OnSceneChange(Scene oldScene, Scene newScene)
    {
        // ������Ϸ������������ҿ��ƣ���������
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
            // ��ȡ����������
            playerControls.PlayerMovements.Movements.performed += i => movementInput = i.ReadValue<Vector2>();
        }

        playerControls.Enable();
        
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

    private void OnApplicationFocus(bool focus)
    {
        // ��Ϸ��С�����޷������ƶ�������ͬʱ�ƶ��������ڵĽ�ɫ��
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
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        horizontalInput = movementInput.x; 
        verticalInput = movementInput.y;
        // Clamp01(float value) : ��value�޶���0-1֮�䣬����1����1С��0����0;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        if (moveAmount > 0 &&  moveAmount <= 0.5)
        {
            moveAmount = 0.5f;
        }
        else if (moveAmount > 0.5 && moveAmount <= 1.0)
        {
            moveAmount = 1.0f;
        }
    }
}
