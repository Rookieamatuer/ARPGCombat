using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveFileDataWriter
{
    public string saveDataDirectoryPath = "";
    public string saveFileName = "";

    // 确认是否已经存在对应存档
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

    // 删除存档
    public void DeleteSavedFile()
    {
        File.Delete(Path.Combine(saveDataDirectoryPath, saveFileName));
    }

    // 在新建游戏时创建文件
    public void CreateNewCharacterSavedFile(CharacterSaveData characterSaveData)
    {
        // 设置本地文件保存路径
        string savePath = Path.Combine(saveDataDirectoryPath, saveFileName);

        try
        {
            // 创建文件写入的目标文件夹
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            Debug.Log("CREATING SAVE FILE, AT THE PATH: " + savePath);

            // 序列化游戏数据到json文件
            string dataToStore = JsonUtility.ToJson(characterSaveData, true);

            // 将文件写入系统
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

    // 加载游戏文件
    public CharacterSaveData LoadSaveFile()
    {
        CharacterSaveData characterSaveData = null;

        // 创建读取路径
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

                // 从json中反序列化数据
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
