
// # Unity
using UnityEngine;

public class EditorSetStageDropDown : MonoBehaviour
{
    public int stage { get; private set; }
    
    public void OnDropDownEvent(int index)
    {
        stage = index;
        GetComponent<EditorManager>().StageChange(stage);
        Debug.Log(stage);
    }
}
