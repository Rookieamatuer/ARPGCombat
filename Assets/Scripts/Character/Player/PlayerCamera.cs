using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;

    public PlayerManager player;

    public Camera cameraObject;

    [SerializeField] Transform cameraPivotTransform;

    // 相机性能参数调整
    [Header("Camera Settings")]
    private float cameraSmoothSpeed = 1.0f; // 插值越大相机移动到目标位置所需的时间越长
    [SerializeField] float leftAndRightRotationSpeed = 180;
    [SerializeField] float upAndDownRotationSpeed = 160;
    [SerializeField] float minimumPivot = -20;  // 旋转相机可达的最低点
    [SerializeField] float maximumPivot = 30;   // 旋转相机可达的最高点
    [SerializeField] float cameraCollisionRadius = 0.2f;
    [SerializeField] LayerMask collideWithLayers;

    // 相机移动参数
    [Header("Camera Values")]
    private Vector3 cameraVelocity;
    private Vector3 cameraObjectPosition;   // 相机碰撞移动的目标位置
    [SerializeField] float leftAndRightLookAngle;
    [SerializeField] float upAndDownLookAngle;
    [SerializeField] float cameraZPosition;   // 相机碰撞初始位置
    [SerializeField] float targetCameraZPosition;    // 相机碰撞目标位置

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        cameraZPosition = cameraObject.transform.localPosition.z;   // localPosition是相对于父级的位置
    }

    public void HandleAllCameraActions()
    {
        if(player != null)
        {
            FollowTarget();
            HandleRotations();
            HandleCollisions();
        }
    }

    private void FollowTarget()
    {
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);

        transform.position = targetCameraPosition;
    }

    private void HandleRotations()
    {
        // TODO: 锁定目标后锁定相机视角

        // 相机旋转
        // 左右
        leftAndRightLookAngle += (PlayerInputManager.instance.cameraHorizontalInput * leftAndRightRotationSpeed) * Time.deltaTime;
        // 上下
        upAndDownLookAngle -= (PlayerInputManager.instance.cameraVerticalInput * upAndDownRotationSpeed) * Time.deltaTime;
        // 将上下方向相机视角固定在minimumPivot, maximumPivot之间
        upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

        Vector3 cameraRotation = Vector3.zero;
        Quaternion targetRotation;
        // 左右旋转
        cameraRotation.y = leftAndRightLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        transform.rotation = targetRotation;

        // pivot(Up&Down)上下旋转
        cameraRotation = Vector3.zero;
        cameraRotation.x = upAndDownLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        cameraPivotTransform.localRotation = targetRotation;
    }

    private void HandleCollisions()
    {
        targetCameraZPosition = cameraZPosition;
        RaycastHit hit;
        Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;    // mainCamera和pivotCamera的连线方向
        direction.Normalize();
        // 检查相机和人物之间是否有障碍物
        // Physics.SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance= Mathf.Infinity, int layerMask)
        // 沿射线投射球体并返回有关命中对象的详细信息
        if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZPosition), collideWithLayers))
        {
            // 获取人物和障碍物的距离
            float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
            // 重新设置相机距离
            targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
        }
        // 如果目标位置小于碰撞半径，使用碰撞半径
        if (Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius)
        {
            targetCameraZPosition = -cameraCollisionRadius;
        }
        // 相机平滑移动到目标位置
        cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.2f);
        cameraObject.transform.localPosition = cameraObjectPosition;
    }
}
