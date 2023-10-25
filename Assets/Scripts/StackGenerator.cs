using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Used to generate a stack of blocks based on the grade selected in the inspector and the data loaded from the JSON file.
/// </summary>
public class StackGenerator : MonoBehaviour
{
    [SerializeField]
    private Block blockPrefab, blockWoodPrefab, blockGlassPrefab, blockStonePrefab;
    [SerializeField]
    private TextMeshPro gradeText; //Text to display the grade
    [SerializeField]
    public Grade grade; //Grade to generate the stack for

    private ArrayList blocks = new ArrayList();

    private void Start()
    {
        GameManager.Instance.AddStackGenerator(this,(int)grade-6);
    }

    //Called when the data is loaded from the JSON file
    private void OnDataLoaded()
    {
        GenerateStack(JSONLoader.Instance.GetGradeData((int)grade));
        SetGradeTextLabel();
    }

    private void SetGradeTextLabel()
    {
        string gradeTextLabel = "Grade";
        switch (grade)
        {
            case Grade.Sixth:
                gradeTextLabel = "6th Grade";
                break;
            case Grade.Seventh:
                gradeTextLabel = "7th Grade";
                break;
            case Grade.Eighth:
                gradeTextLabel = "8th Grade";
                break;
        }
        gradeText.text = gradeTextLabel;
    }

    //Generate a tower of blocks based on the grade selected in the inspector
    public void GenerateStack(GradeData[] gradeData)
    {
        blocks.Clear();

        for (int i = 0; i < gradeData.Length; i++)
        {
            Debug.Log("Subject: " + gradeData[i].subject + ", Grade: " + gradeData[i].grade + ", Domain: " + gradeData[i].domain
                 + ", cluster: " + gradeData[i].cluster + ", standard ID: " + gradeData[i].standardid);
            Block block = CreateBlock(i, gradeData[i].mastery);
            block.SetGradeData(gradeData[i]);
            blocks.Add(block);
        }
    }

    //Create a block at the specified index
    private Block CreateBlock(int index, int mastery)
    {
        Block block = blockPrefab;
        switch (mastery)
        {
            case 0:
                block = Instantiate(blockGlassPrefab);
                block.SetBlockType(BlockType.Glass);
                break;
            case 1:
                block = Instantiate(blockWoodPrefab);
                block.SetBlockType(BlockType.Wood);
                break;
            case 2:
                block = Instantiate(blockStonePrefab);
                block.SetBlockType(BlockType.Stone);
                break;
        }
        block.transform.parent = transform;
        block.transform.localPosition = GetBlockStackLocation(index);
        block.transform.localRotation = GetBlockStackRotation(index);

        return block;
    }

    //Determine the location of each block in the stack, starting from the bottom
    private Vector3 GetBlockStackLocation(int index)
    {
        int layerID = index / 3;

        float xLocation = 0f;
        float yLocation = 0f;
        float zLocation = 0f;

        if (layerID % 2 == 0)
        {
            xLocation = index % 3 - 1;
            xLocation *= 1.5f;
            yLocation = layerID * 0.75f;
            zLocation = 0f;
        }
        else
        {
            xLocation = 0f;
            yLocation = layerID * 0.75f;
            zLocation = index % 3 - 1;
            zLocation *= 1.5f;
        }

        Vector3 blockLocation = new Vector3(xLocation, yLocation, zLocation);
        return blockLocation;
    }

    //Determine the rotation of each block in the stack, starting from the bottom
    private Quaternion GetBlockStackRotation(int index)
    {
        int layerID = index / 3;

        Quaternion blockRotation = Quaternion.identity;

        if (layerID % 2 == 0)
        {
            blockRotation = Quaternion.Euler(0f, 90f, 0f);
        }

        return blockRotation;
    }

    private void OnEnable()
    {
        JSONLoader.Instance.OnDataLoaded += OnDataLoaded;
    }

    private void OnDisable()
    {
        JSONLoader.Instance.OnDataLoaded -= OnDataLoaded;
    }

    public void DestroyAllGlassBlocks()
    {
        foreach (Block block in blocks)
        {
            if (block.GetBlockType() == BlockType.Glass)
            {
                block.gameObject.SetActive(false);
            }
        }
    }
}

[System.Serializable]
public enum Grade
{
    Sixth = 6,
    Seventh = 7,
    Eighth = 8
}