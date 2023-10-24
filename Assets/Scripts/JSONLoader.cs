using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Class for loading the JSON data from the web server based on the URL.
/// This class is a singleton, so it can be accessed from anywhere in the project.
/// </summary>
public class JSONLoader : MonoBehaviour
{
    private string url = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";

    private GradeDataArray gradeDataArray;

    public static JSONLoader Instance;

    public delegate void OnDataLoadedDelegate();
    public OnDataLoadedDelegate OnDataLoaded;

    private void Awake()
    {
        Instance = this;
    }

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

    //Parse the JSON data into an array of GradeData objects
    void ProcessJSONData(string jsonString)
    {
        gradeDataArray = JsonUtility.FromJson<GradeDataArray>("{\"gradeDataArray\":" + jsonString + "}");

        foreach (GradeData gradeData in gradeDataArray.gradeDataArray)
        {
            Debug.Log("Subject: " + gradeData.subject + ", Grade: " + gradeData.grade);
        }

        OnDataLoaded?.Invoke();
    }

    //Return an array of GradeData objects based on the grade
    public GradeData[] GetGradeData(int grade)
    {
        ArrayList gradeDataList = new ArrayList();

        string gradeString = "-1";
        switch (grade)
        {
            case 6:
                gradeString = "6th Grade";
                break;

            case 7:
                gradeString = "7th Grade";
                break;

            case 8:
                gradeString = "8th Grade";
                break;
        }

        foreach (GradeData gradeData in gradeDataArray.gradeDataArray)
        {
            if (gradeData.grade == gradeString)
            {
                gradeDataList.Add(gradeData);
            }
        }

        return SortGradeData((GradeData[])gradeDataList.ToArray(typeof(GradeData)));
    }

    //Sort the gradeDataArray based on domain, cluster, and standard ID using LINQ
    private GradeData[] SortGradeData(GradeData[] gradeData)
    {
        gradeData = gradeData.OrderBy(gradeData => gradeData.domain).ToArray();

        gradeData = gradeData
                .OrderBy(gradeData => gradeData.domain)
                .ThenBy(gradeData => gradeData.cluster)
                .ThenBy(gradeData => gradeData.standardid)
                .ToArray();
        return gradeData;
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
