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
        // 同时只存在一个实例
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
        // 异步加载 (LoadSceneAsync需要传入要加载的场景编号（在buildsetting）)(Coroutine)
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);
        
        yield return null;
    }

    public int GetWorldSceneIndex()
    {
        return worldSceneIndex;
    }
}
