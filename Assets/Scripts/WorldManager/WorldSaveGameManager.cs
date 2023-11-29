using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager : MonoBehaviour
{

    public static WorldSaveGameManager instance;

    [SerializeField] int worldSceneIndex = 1;

    private void Awake()
    {
        // ͬʱֻ����һ��ʵ��
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
    }

    public IEnumerator LoadNewGame()
    {
        Debug.Log("new game");
        // �첽���� (LoadSceneAsync��Ҫ����Ҫ���صĳ�����ţ���buildsetting��)(Coroutine)
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);
        
        yield return null;
    }

    public int GetWorldSceneIndex()
    {
        return worldSceneIndex;
    }
}
