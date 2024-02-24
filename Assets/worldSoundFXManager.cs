using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldSoundFXManager : MonoBehaviour
{
    public static worldSoundFXManager instance;

    [Header("ActionSounds")]
    public AudioClip rollSFX;

    private void Awake()
    {
        if (instance == null)
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
}
