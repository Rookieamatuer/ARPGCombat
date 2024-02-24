using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance;
    [Header("NETWORK JOIN")]
    [SerializeField] bool startGameAsClient;

    [HideInInspector] public PlayerUIHUDManager playerUIHUDManager;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

        playerUIHUDManager = GetComponentInChildren<PlayerUIHUDManager>();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(startGameAsClient)
        {
            startGameAsClient = false;
            // �����ȹر���Ϊhost�����ĵ���
            NetworkManager.Singleton.Shutdown();
            // ��client��ʽ������������
            NetworkManager.Singleton.StartClient();
        }
    }
}
