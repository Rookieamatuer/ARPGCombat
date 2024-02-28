using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// 序列化文件用于指向每个保存的文件
public class CharacterSaveData
{
    [Header("Scene Index")]
    public int sceneIndex;

    [Header("Character name")]
    public string characterName = "test";

    [Header("Time playered")]
    public float secondsPlayed;

    // 只能保存基本类型的数据
    [Header("World Coordinate")]
    public float xPosition;
    public float yPosition;
    public float zPosition;
}
