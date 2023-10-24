using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class JSONLoader : MonoBehaviour
{
    private string url = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";

    private GradeDataArray gradeDataArray;

    void Start()
    {
        StartCoroutine(FetchData());
    }

    IEnumerator FetchData()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Web Request Error: " + request.error);
        }
        else
        {
            ProcessJSONData(request.downloadHandler.text);
        }
    }

    void ProcessJSONData(string jsonString)
    {
        gradeDataArray = JsonUtility.FromJson<GradeDataArray>("{\"gradeDataArray\":" + jsonString + "}");

        foreach (GradeData gradeData in gradeDataArray.gradeDataArray)
        {
            Debug.Log("Subject: " + gradeData.subject + ", Grade: " + gradeData.grade);
        }
    }
}


[System.Serializable]
public class GradeData
{
    public int id;
    public string subject;
    public string grade;
    public int mastery;
    public string domainid;
    public string domain;
    public string cluster;
    public string standardid;
    public string standardDescription;
}


[System.Serializable]
public class GradeDataArray
{
    public GradeData[] gradeDataArray;
}
