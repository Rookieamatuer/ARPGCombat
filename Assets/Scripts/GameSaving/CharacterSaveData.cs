using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// ���л��ļ�����ָ��ÿ��������ļ�
public class CharacterSaveData
{
    [Header("Scene Index")]
    public int sceneIndex;

    [Header("Character name")]
    public string characterName = "test";

    [Header("Time playered")]
    public float secondsPlayed;

    // ֻ�ܱ���������͵�����
    [Header("World Coordinate")]
    public float xPosition;
    public float yPosition;
    public float zPosition;
}
