using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Character_Save_Slot : MonoBehaviour
{
    SaveFileDataWriter saveFileWriter;

    [Header("Game slot")]
    public CharacterSlots characterSlot;

    [Header("Character Info")]
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI timePlayed;

    private bool isDoubleClick = false;

    private void OnEnable()
    {
        LoadSaveSlot();
    }

    private void LoadSaveSlot()
    {
        saveFileWriter = new SaveFileDataWriter();
        saveFileWriter.saveDataDirectoryPath = Application.persistentDataPath;

        // 存档01
        if (characterSlot == CharacterSlots.CharaterSlot_01)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // 如果有存档
            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot01.characterName;
            } // 无存档则不可用
            else
            {
                gameObject.SetActive(false);
            }
        }

        // 存档02
        else if (characterSlot == CharacterSlots.CharaterSlot_02)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // 如果有存档
            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot02.characterName;
            } // 无存档则不可用
            else
            {
                gameObject.SetActive(false);
            }
        }

        // 存档03
        else if (characterSlot == CharacterSlots.CharaterSlot_03)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // 如果有存档
            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot03.characterName;
            } // 无存档则不可用
            else
            {
                gameObject.SetActive(false);
            }
        }

        // 存档04
        else if (characterSlot == CharacterSlots.CharaterSlot_04)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // 如果有存档
            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot04.characterName;
            } // 无存档则不可用
            else
            {
                gameObject.SetActive(false);
            }
        }

        // 存档05
        else if (characterSlot == CharacterSlots.CharaterSlot_05)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // 如果有存档
            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot05.characterName;
            } // 无存档则不可用
            else
            {
                gameObject.SetActive(false);
            }
        }

        // 存档06
        else if (characterSlot == CharacterSlots.CharaterSlot_06)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // 如果有存档
            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot06.characterName;
            } // 无存档则不可用
            else
            {
                gameObject.SetActive(false);
            }
        }

        // 存档07
        else if (characterSlot == CharacterSlots.CharaterSlot_07)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // 如果有存档
            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot07.characterName;
            } // 无存档则不可用
            else
            {
                gameObject.SetActive(false);
            }
        }

        // 存档08
        else if (characterSlot == CharacterSlots.CharaterSlot_08)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // 如果有存档
            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot08.characterName;
            } // 无存档则不可用
            else
            {
                gameObject.SetActive(false);
            }
        }

        // 存档09
        else if (characterSlot == CharacterSlots.CharaterSlot_09)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // 如果有存档
            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot09.characterName;
            } // 无存档则不可用
            else
            {
                gameObject.SetActive(false);
            }
        }

        // 存档10
        else if (characterSlot == CharacterSlots.CharaterSlot_10)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // 如果有存档
            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot10.characterName;
            } // 无存档则不可用
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void LoadGameFromCharacterSlot()
    {
        if (!isDoubleClick)
        {
            isDoubleClick = true;
            // 在点击后等待一段时间，如果没有再次点击，则认定为单击事件
            Invoke("SingleClick", 0.5f);
            return;
        }
        isDoubleClick = false;
        WorldSaveGameManager.instance.currentCharacterSlotBeingUsed = characterSlot;
        WorldSaveGameManager.instance.LoadGame();
    }

    private void SingleClick()
    {
        isDoubleClick = false;
    }

    public void SelectCurrentSlot()
    {
        TitleScreenManager.Instance.SelectCharacterSlot(characterSlot);
    }
}
