using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveFileDataWriter
{
    public string saveDataDirectoryPath = "";
    public string saveFileName = "";

    // ȷ���Ƿ��Ѿ����ڶ�Ӧ�浵
    public bool CheckToSeeIfFileExists()
    {
        if (File.Exists(Path.Combine(saveDataDirectoryPath, saveFileName)))
        {
            return true;
        } else
        {
            return false;
        }
    }

    // ɾ���浵
    public void DeleteSavedFile()
    {
        File.Delete(Path.Combine(saveDataDirectoryPath, saveFileName));
    }

    // ���½���Ϸʱ�����ļ�
    public void CreateNewCharacterSavedFile(CharacterSaveData characterSaveData)
    {
        // ���ñ����ļ�����·��
        string savePath = Path.Combine(saveDataDirectoryPath, saveFileName);

        try
        {
            // �����ļ�д���Ŀ���ļ���
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            Debug.Log("CREATING SAVE FILE, AT THE PATH: " + savePath);

            // ���л���Ϸ���ݵ�json�ļ�
            string dataToStore = JsonUtility.ToJson(characterSaveData, true);

            // ���ļ�д��ϵͳ
            using (FileStream stream = new FileStream(savePath, FileMode.Create))
            {
                using (StreamWriter fileWriter = new StreamWriter(stream))
                {
                    fileWriter.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.Log("ERROR OCCURED WHILE TRYING TO SAVE CHARACTER DATA, GAME NOT SAVE" + savePath + "\n" + e);
        }
    }

    // ������Ϸ�ļ�
    public CharacterSaveData LoadSaveFile()
    {
        CharacterSaveData characterSaveData = null;

        // ������ȡ·��
        string loadPath = Path.Combine(saveDataDirectoryPath, saveFileName);

        if (File.Exists(loadPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(loadPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // ��json�з����л�����
                characterSaveData = JsonUtility.FromJson<CharacterSaveData>(dataToLoad);
            } 
            catch(Exception e)
            {
                Debug.LogError(e);
            }
        }
        return characterSaveData;
    }
}
