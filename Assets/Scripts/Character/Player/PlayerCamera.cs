using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;

    public PlayerManager player;

    public Camera cameraObject;

    [SerializeField] Transform cameraPivotTransform;

    // ������ܲ�������
    [Header("Camera Settings")]
    private float cameraSmoothSpeed = 1.0f; // ��ֵԽ������ƶ���Ŀ��λ�������ʱ��Խ��
    [SerializeField] float leftAndRightRotationSpeed = 180;
    [SerializeField] float upAndDownRotationSpeed = 160;
    [SerializeField] float minimumPivot = -20;  // ��ת����ɴ����͵�
    [SerializeField] float maximumPivot = 30;   // ��ת����ɴ����ߵ�
    [SerializeField] float cameraCollisionRadius = 0.2f;
    [SerializeField] LayerMask collideWithLayers;

    // ����ƶ�����
    [Header("Camera Values")]
    private Vector3 cameraVelocity;
    private Vector3 cameraObjectPosition;   // �����ײ�ƶ���Ŀ��λ��
    [SerializeField] float leftAndRightLookAngle;
    [SerializeField] float upAndDownLookAngle;
    [SerializeField] float cameraZPosition;   // �����ײ��ʼλ��
    [SerializeField] float targetCameraZPosition;    // �����ײĿ��λ��

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

        cameraZPosition = cameraObject.transform.localPosition.z;   // localPosition������ڸ�����λ��
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
        // TODO: ����Ŀ�����������ӽ�

        // �����ת
        // ����
        leftAndRightLookAngle += (PlayerInputManager.instance.cameraHorizontalInput * leftAndRightRotationSpeed) * Time.deltaTime;
        // ����
        upAndDownLookAngle -= (PlayerInputManager.instance.cameraVerticalInput * upAndDownRotationSpeed) * Time.deltaTime;
        // �����·�������ӽǹ̶���minimumPivot, maximumPivot֮��
        upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

        Vector3 cameraRotation = Vector3.zero;
        Quaternion targetRotation;
        // ������ת
        cameraRotation.y = leftAndRightLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        transform.rotation = targetRotation;

        // pivot(Up&Down)������ת
        cameraRotation = Vector3.zero;
        cameraRotation.x = upAndDownLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        cameraPivotTransform.localRotation = targetRotation;
    }

    private void HandleCollisions()
    {
        targetCameraZPosition = cameraZPosition;
        RaycastHit hit;
        Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;    // mainCamera��pivotCamera�����߷���
        direction.Normalize();
        // ������������֮���Ƿ����ϰ���
        // Physics.SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance= Mathf.Infinity, int layerMask)
        // ������Ͷ�����岢�����й����ж������ϸ��Ϣ
        if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZPosition), collideWithLayers))
        {
            // ��ȡ������ϰ���ľ���
            float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
            // ���������������
            targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
        }
        // ���Ŀ��λ��С����ײ�뾶��ʹ����ײ�뾶
        if (Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius)
        {
            targetCameraZPosition = -cameraCollisionRadius;
        }
        // ���ƽ���ƶ���Ŀ��λ��
        cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.2f);
        cameraObject.transform.localPosition = cameraObjectPosition;
    }
}
