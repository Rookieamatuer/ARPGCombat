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

        // �浵01
        if (characterSlot == CharacterSlots.CharaterSlot_01)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // ����д浵
            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot01.characterName;
            } // �޴浵�򲻿���
            else
            {
                gameObject.SetActive(false);
            }
        }

        // �浵02
        else if (characterSlot == CharacterSlots.CharaterSlot_02)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // ����д浵
            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot02.characterName;
            } // �޴浵�򲻿���
            else
            {
                gameObject.SetActive(false);
            }
        }

        // �浵03
        else if (characterSlot == CharacterSlots.CharaterSlot_03)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // ����д浵
            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot03.characterName;
            } // �޴浵�򲻿���
            else
            {
                gameObject.SetActive(false);
            }
        }

        // �浵04
        else if (characterSlot == CharacterSlots.CharaterSlot_04)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // ����д浵
            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot04.characterName;
            } // �޴浵�򲻿���
            else
            {
                gameObject.SetActive(false);
            }
        }

        // �浵05
        else if (characterSlot == CharacterSlots.CharaterSlot_05)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // ����д浵
            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot05.characterName;
            } // �޴浵�򲻿���
            else
            {
                gameObject.SetActive(false);
            }
        }

        // �浵06
        else if (characterSlot == CharacterSlots.CharaterSlot_06)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // ����д浵
            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot06.characterName;
            } // �޴浵�򲻿���
            else
            {
                gameObject.SetActive(false);
            }
        }

        // �浵07
        else if (characterSlot == CharacterSlots.CharaterSlot_07)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // ����д浵
            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot07.characterName;
            } // �޴浵�򲻿���
            else
            {
                gameObject.SetActive(false);
            }
        }

        // �浵08
        else if (characterSlot == CharacterSlots.CharaterSlot_08)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // ����д浵
            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot08.characterName;
            } // �޴浵�򲻿���
            else
            {
                gameObject.SetActive(false);
            }
        }

        // �浵09
        else if (characterSlot == CharacterSlots.CharaterSlot_09)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // ����д浵
            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot09.characterName;
            } // �޴浵�򲻿���
            else
            {
                gameObject.SetActive(false);
            }
        }

        // �浵10
        else if (characterSlot == CharacterSlots.CharaterSlot_10)
        {
            saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            // ����д浵
            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot10.characterName;
            } // �޴浵�򲻿���
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
            // �ڵ����ȴ�һ��ʱ�䣬���û���ٴε�������϶�Ϊ�����¼�
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
