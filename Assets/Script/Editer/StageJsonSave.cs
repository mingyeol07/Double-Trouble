// # Systems
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

// # Unity
using UnityEngine;
/// <summary>
/// Json저장을 돕는 클래스
/// </summary>
public class StageJsonSave : MonoBehaviour
{
    public void SaveData(StageData stageData)
    {
        string directoryPath = Application.dataPath + "/Resources/Data";
        string filePath = directoryPath + "/StageData.json";

        // 디렉터리가 존재하지 않으면 생성
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        Debug.Log("File path: " + filePath);
        string json = JsonUtility.ToJson(stageData, true);
        File.WriteAllText(filePath, json);
    }

    public StageData LoadData()
    {
        TextAsset test = Resources.Load<TextAsset>("Data/StageData");
        if (test != null)
        {
            string json = test.text;
            return JsonUtility.FromJson<StageData>(json);
        }
        else
        {
            Debug.Log("No save file found.");
            return new StageData(); // 빈 StageData 객체 반환
        }
    }
}
