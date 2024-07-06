// # Systems
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

// # Unity
using UnityEngine;
/// <summary>
/// Json������ ���� Ŭ����
/// </summary>
public class StageJsonSave : MonoBehaviour
{
    public void SaveData(StageData stageData, int stageIndex)
    {
        string directoryPath = Application.dataPath + "/Resources/Data";
        string filePath = directoryPath + "/StageData" + stageIndex + ".json";

        // ���͸��� �������� ������ ����
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        Debug.Log("File path: " + filePath);
        string json = JsonUtility.ToJson(stageData, true);
        File.WriteAllText(filePath, json);
    }

    public StageData LoadData(int stageIndex)
    {
        TextAsset test = Resources.Load<TextAsset>("Data/StageData" + stageIndex);
        if (test != null)
        {
            string json = test.text;
            return JsonUtility.FromJson<StageData>(json);
        }
        else
        {
            Debug.Log("No save file found.");
            return new StageData(); // �� StageData ��ü ��ȯ
        }
    }
}
