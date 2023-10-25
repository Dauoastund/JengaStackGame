using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class BlockInfoPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI gradeLevelText;
    [SerializeField]
    private TextMeshProUGUI clusterText;
    [SerializeField]
    private TextMeshProUGUI standardIDText;
    [SerializeField]
    private Animation anim;

    private UIFollower uiFollower;

    private void Awake()
    {
        uiFollower = GetComponent<UIFollower>();
    }

    public void SetInfo(GradeData gradeData, Transform blockTransform)
    {
        gradeLevelText.text = "Grade level: " + gradeData.grade;
        clusterText.text = "Cluster: " + gradeData.cluster;
        standardIDText.text = "Standard ID: " + gradeData.standardid;
        uiFollower.targetObject = blockTransform.gameObject;
        anim.Stop();
        anim.Play("Panel_Open");
    }
}
